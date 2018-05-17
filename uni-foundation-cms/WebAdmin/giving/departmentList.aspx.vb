Imports System.Text
Imports ba = Foundation.BusinessAdmin
Imports cr = Core
Imports dt = Foundation.Data

Namespace giving

    Public Class departmentList
        Inherits ba.BasePage

        Protected ddlCollege As DropDownList
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
                dgrPages.SortColumnIndex = 1
                dgrPages.SortOrder = " asc"
                BindLists()
                BindData()
            End If
        End Sub

        Private Sub BindLists()
            ddlCollege.DataSource = New dt.CollegePageData().GetList()
            ddlCollege.DataTextField = "college"
            ddlCollege.DataValueField = "id"
            ddlCollege.DataBind()
            ddlCollege.Items.Insert(0, New ListItem("-- select all", "0"))

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
        End Sub

        Private Sub BindData()
            Dim pages As DataTable

            Dim keywords As String = (New ba.WebHelper).FilterUserInput(tbxKeyword.Text.Trim, True)

            If (ddlCollege.SelectedIndex > 0 Or ddlStatus.SelectedIndex > 0 Or ddlLabel.SelectedIndex > 0 Or ddlTag.SelectedIndex > 0 Or keywords <> "") Then
                pages = New dt.DepartmentPageData().GetDepartmentPagesWithQuery(makeQuery(keywords))
            Else
                pages = New dt.DepartmentPageData().GetDepartmentPages(dgrPages.SortExpression)
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

            If (ddlCollege.SelectedIndex > 0) Then
                sb.AppendFormat(" and d.collegepageid = {0} ", ddlCollege.SelectedValue)
            End If

            If (ddlStatus.SelectedValue = "1") Then
                sb.Append(" and p.ispublished = 1 ")
            ElseIf (ddlStatus.SelectedValue = "2") Then
                sb.Append(" and p.ispublished = 0 ")
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
                        sbKeywords.AppendFormat(" (p.pagetitle like '%{0}%' or p.contenttitle like '%{0}%' or p.body like '%{0}%' or p.bodydraft like '%{0}%' or p.admincomment like '%{0}%') and ", term)
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

                Dim lnkInfo As HyperLink = e.Item.Cells(7).Controls(0)
                Dim lnkEdit As HyperLink = e.Item.Cells(8).Controls(0)
                lnkInfo.CssClass = "gridlink"
                lnkEdit.CssClass = "gridlink"

                Dim ispublished As Boolean = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "ispublished"))
                If ispublished Then
                    e.Item.Cells(3).Text = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "publisheddate")).ToShortDateString
                Else
                    e.Item.Cells(3).Text = "-"
                End If

                Dim url As String = DataBinder.Eval(e.Item.DataItem, "url")
                e.Item.Cells(9).Text = "<a href='" + url + "&draft=1' class='gridlink' target='_blank'>preview</a>"
            End If
        End Sub

        Private Sub CheckPageCategoryMenu()
            If Not (New dt.PageCategoryData().HasMenu(ba.PageCategoryCode.College) _
                      And New dt.PageCategoryData().HasMenu(ba.PageCategoryCode.Department) _
                      And New dt.PageCategoryData().HasMenu(ba.PageCategoryCode.Program)) Then
                Response.Redirect("departments.aspx")
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 28
            End Get
        End Property

    End Class

End Namespace