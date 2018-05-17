Imports System.Text
Imports ba = Foundation.BusinessAdmin
Imports cr = Core
Imports dt = Foundation.Data

Namespace webpages

    Public Class newsList
        Inherits ba.BasePage

        Protected ddlHomepage As DropDownList
        Protected ddlHighlighted As DropDownList
        Protected ddlLabel As DropDownList
        Protected ddlTag As DropDownList
        Protected ddlStatus As DropDownList
        Protected tbxKeyword As TextBox
        Protected WithEvents btnSearch As Button
        Protected WithEvents btnReset As Button
        Protected ltrCount As Literal
        Protected WithEvents dgrPages As cr.MySortGrid
        Protected pnlGrid As Panel

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            CheckPageCategoryMenu()
            dgrPages.BindingMethod = AddressOf BindData
            If Not Page.IsPostBack Then
                dgrPages.SortColumnIndex = 4
                dgrPages.SortOrder = " desc"
                BindLists()
                BindData()
            End If
        End Sub

        Private Sub BindLists()
            ddlLabel.DataSource = New dt.ContentLabelData().GetList()
            ddlLabel.DataTextField = "text"
            ddlLabel.DataValueField = "id"
            ddlLabel.DataBind()
            ddlLabel.Items.Insert(0, New ListItem("-- select all", "0"))

            ddlTag.DataSource = New dt.TagData().GetList()
            ddlTag.DataTextField = "text"
            ddlTag.DataValueField = "id"
            ddlTag.DataBind()
            ddlTag.Items.Insert(0, New ListItem("-- select all", "0"))

            ddlStatus.Items.Clear()
            ddlStatus.Items.Add(New ListItem("-- select all", "0"))
            ddlStatus.Items.Add(New ListItem("published", "1"))
            ddlStatus.Items.Add(New ListItem("drafts", "2"))
            ddlStatus.SelectedIndex = 0

            ddlHomepage.Items.Clear()
            ddlHomepage.Items.Add(New ListItem("-- select all", "0"))
            ddlHomepage.Items.Add(New ListItem("displayed", "1"))
            ddlHomepage.Items.Add(New ListItem("not displayed", "2"))
            ddlHomepage.SelectedIndex = 0

            ddlHighlighted.Items.Clear()
            ddlHighlighted.Items.Add(New ListItem("-- select all", "0"))
            ddlHighlighted.Items.Add(New ListItem("highlighted", "1"))
            ddlHighlighted.Items.Add(New ListItem("not highlighted", "2"))
            ddlHighlighted.SelectedIndex = 0
        End Sub

        Private Sub BindData()
            Dim pages As DataTable

            Dim keywords As String = (New ba.WebHelper).FilterUserInput(tbxKeyword.Text.Trim, True)

            If (ddlHomepage.SelectedIndex > 0 Or ddlHighlighted.SelectedIndex > 0 Or ddlStatus.SelectedIndex > 0 _
              Or ddlLabel.SelectedIndex > 0 Or ddlTag.SelectedIndex > 0 Or keywords <> "") Then
                pages = New dt.NewsPageData().GetNewsPagesWithQuery(makeQuery(keywords))
            Else
                pages = New dt.NewsPageData().GetNewsPages(dgrPages.SortExpression)
            End If

            ltrCount.Text = pages.Rows.Count.ToString

            If pages.Rows.Count > 0 Then
                pnlGrid.Visible = True
                dgrPages.Prefix = LinkPrefix
                dgrPages.DataSource = pages
                dgrPages.DataBind()
            Else
                pnlGrid.Visible = False
            End If
        End Sub

        Private Function makeQuery(ByVal keywords As String) As String
            Dim sb As New StringBuilder

            If (ddlStatus.SelectedValue = "1") Then
                sb.Append(" and p.ispublished = 1 ")
            ElseIf (ddlStatus.SelectedValue = "2") Then
                sb.Append(" and p.ispublished = 0 ")
            End If

            If (ddlHomepage.SelectedValue = "1") Then
                sb.Append(" and n.isonhomepage = 1 ")
            ElseIf (ddlHomepage.SelectedValue = "2") Then
                sb.Append(" and n.isonhomepage = 0 ")
            End If

            If (ddlHighlighted.SelectedValue = "1") Then
                sb.Append(" and n.ishighlighted = 1 ")
            ElseIf (ddlHighlighted.SelectedValue = "2") Then
                sb.Append(" and n.ishighlighted = 0 ")
            End If

            If (ddlLabel.SelectedIndex > 0) Then
                sb.AppendFormat(" and pll.contentlabelid = {0} ", ddlLabel.SelectedValue)
            End If

            If (ddlTag.SelectedIndex > 0) Then
                sb.AppendFormat(" and ptl.tagid = {0} ", ddlTag.SelectedValue)
            End If

            If (keywords <> "") Then
                Dim arr As String() = keywords.Split()
                If arr.Length > 0 Then
                    Dim sbKeywords As New StringBuilder
                    For Each term As String In arr
                        sbKeywords.AppendFormat(" (p.pagetitle like '%{0}%' or p.contenttitle like '%{0}%' or p.body like '%{0}%' or p.bodydraft like '%{0}%' or p.admincomment like '%{0}%' or n.summary like '%{0}%') and ", term)
                    Next
                    Dim result As String = sbKeywords.ToString
                    sb.AppendFormat(" and {0} ", result.Substring(0, result.Length - 4))
                End If
            End If

            sb.AppendFormat(" order by {0} ", dgrPages.SortExpression)

            Dim temp As String = sb.ToString()
            Return " where " + temp.Substring(4)
        End Function

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            BindData()
        End Sub

        Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
            BindLists()
            tbxKeyword.Text = ""
            BindData()
        End Sub

        Private Sub dgrPages_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrPages.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then

                Dim lnkInfo As HyperLink = e.Item.Cells(6).Controls(0)
                Dim lnkEdit As HyperLink = e.Item.Cells(7).Controls(0)
                lnkInfo.CssClass = "gridlink"
                lnkEdit.CssClass = "gridlink"

                Dim ispublished As Boolean = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "ispublished"))
                If ispublished Then
                    e.Item.Cells(2).Text = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "publisheddate")).ToShortDateString
                Else
                    e.Item.Cells(2).Text = "-"
                End If

                Dim url As String = DataBinder.Eval(e.Item.DataItem, "url")
                e.Item.Cells(8).Text = "<a href='" + url + "&draft=1' class='gridlink' target='_blank'>preview</a>"
            End If
        End Sub

        Private Sub CheckPageCategoryMenu()
            If Not New dt.PageCategoryData().HasMenu(ba.PageCategoryCode.News) Then
                Response.Redirect("news.aspx")
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 11
            End Get
        End Property

    End Class

End Namespace