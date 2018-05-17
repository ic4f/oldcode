Imports System.Text
Imports ba = Foundation.BusinessAdmin
Imports cr = Core
Imports dt = Foundation.Data

Namespace webpages

    Public Class allPages
        Inherits ba.BasePage

        Protected ddlType As DropDownList
        Protected ddlLabel As DropDownList
        Protected ddlTag As DropDownList
        Protected ddlModule As DropDownList
        Protected ddlStatus As DropDownList
        Protected tbxKeyword As TextBox
        Protected ltrTreeDisplay As Literal
        Protected hidMenu As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents btnSearch As Button
        Protected WithEvents btnReset As Button
        Protected ltrCount As Literal
        Protected WithEvents dgrPages As cr.MySortGrid
        Protected pnlGrid As Panel

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            dgrPages.BindingMethod = AddressOf BindData
            If Not Page.IsPostBack Then
                btnSearch.Attributes.Add("OnClick", "getMenuId();")
                dgrPages.SortColumnIndex = 6
                dgrPages.SortOrder = " desc"
                BindLists()
                BindLabelId()
                BindTagId()
                BindModuleId()
                BindMenu()
                BindData()
            End If
        End Sub

        Private Sub BindLabelId()
            If Request("LabelId") <> "" Then
                ddlLabel.SelectedValue = Request("LabelId")
            End If
        End Sub

        Private Sub BindTagId()
            If Request("TagId") <> "" Then
                ddlTag.SelectedValue = Request("TagId")
            End If
        End Sub

        Private Sub BindModuleId()
            If Request("ModuleId") <> "" Then
                ddlModule.SelectedValue = Request("ModuleId")
            End If
        End Sub

        Private Function getMenuId() As Integer
            Return Convert.ToInt32(hidMenu.Value)
        End Function

        Private Sub BindMenu()
            Dim dt As DataTable = New dt.StagingMenuData().GetOrdered().Tables(0)
            Dim pmt As New ba.PublicMenuTree(ba.PublicMenuTree.ConvertData(dt), getMenuId(), "../")
            ltrTreeDisplay.Text = pmt.GetMenuForCmsMenuSelection()
        End Sub

        Private Sub BindLists()
            ddlType.DataSource = New dt.PageCategoryData().GetList()
            ddlType.DataTextField = "text"
            ddlType.DataValueField = "id"
            ddlType.DataBind()
            ddlType.Items.Insert(0, New ListItem("-- select all", "0"))

            ddlLabel.Items.Insert(0, New ListItem("-- select all", "0"))
            ddlLabel.DataSource = New dt.ContentLabelData().GetList()
            ddlLabel.DataTextField = "text"
            ddlLabel.DataValueField = "id"
            ddlLabel.DataBind()
            ddlLabel.Items.Insert(0, New ListItem("-- select all", "0"))

            ddlModule.Items.Insert(0, New ListItem("-- select all", "0"))
            ddlModule.DataSource = New dt.ModuleData().GetList()
            ddlModule.DataTextField = "text"
            ddlModule.DataValueField = "id"
            ddlModule.DataBind()
            ddlModule.Items.Insert(0, New ListItem("-- select all", "0"))

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
            Dim menuid As Integer = getMenuId()

            If (ddlModule.SelectedIndex > 0 Or ddlType.SelectedIndex > 0 Or ddlStatus.SelectedIndex > 0 Or ddlLabel.SelectedIndex > 0 Or ddlTag.SelectedIndex > 0 Or menuid > 0 Or keywords <> "") Then
                pages = New dt.PageData().GetAllPagesWithQuery(makeQuery(menuid, keywords))
            Else
                pages = New dt.PageData().GetAllPages(dgrPages.SortExpression)
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

        Private Function makeQuery(ByVal menuid As Integer, ByVal keywords As String) As String
            Dim sb As New StringBuilder

            If (ddlStatus.SelectedValue = "1") Then
                sb.Append(" and p.ispublished = 1 ")
            ElseIf (ddlStatus.SelectedValue = "2") Then
                sb.Append(" and p.ispublished = 0 ")
            End If

            If (ddlModule.SelectedIndex > 0) Then
                sb.AppendFormat(" and pml.moduleid = {0} ", ddlModule.SelectedValue)
            End If

            If (ddlType.SelectedIndex > 0) Then
                sb.AppendFormat(" and p.pagecategoryid = {0} ", ddlType.SelectedValue)
            End If

            If (ddlLabel.SelectedIndex > 0) Then
                sb.AppendFormat(" and pll.contentlabelid = {0} ", ddlLabel.SelectedValue)
            End If

            If (ddlTag.SelectedIndex > 0) Then
                sb.AppendFormat(" and ptl.tagid = {0} ", ddlTag.SelectedValue)
            End If

            If (menuid > 0) Then
                sb.AppendFormat(" and m.id = {0} ", menuid)
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
            BindMenu()
            BindData()
        End Sub

        Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
            hidMenu.Value = "-1"
            BindLists()
            BindMenu()
            BindData()
        End Sub

        Private Sub dgrPages_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrPages.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then

                Dim lnkInfo As HyperLink = e.Item.Cells(8).Controls(0)
                Dim lnkEdit As HyperLink = e.Item.Cells(9).Controls(0)
                lnkInfo.CssClass = "gridlink"
                lnkEdit.CssClass = "gridlink"

                Dim ispublished As Boolean = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "ispublished"))
                If ispublished Then
                    e.Item.Cells(4).Text = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "publisheddate")).ToShortDateString
                Else
                    e.Item.Cells(4).Text = "-"
                End If

                Dim id As Integer = dgrPages.DataKeys(e.Item.ItemIndex)
                Dim catId As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "pagecategoryId"))
                If catId = ba.PageCategoryCode.College Then
                    lnkEdit.NavigateUrl = "../giving/collegeEdit.aspx?Id=" + id.ToString
                ElseIf catId = ba.PageCategoryCode.Department Then
                    lnkEdit.NavigateUrl = "../giving/departmentEdit.aspx?Id=" + id.ToString
                ElseIf catId = ba.PageCategoryCode.Program Then
                    lnkEdit.NavigateUrl = "../giving/programEdit.aspx?Id=" + id.ToString
                ElseIf catId = ba.PageCategoryCode.DonorStory Then
                    lnkEdit.NavigateUrl = "dstoryEdit.aspx?Id=" + id.ToString
                ElseIf catId = ba.PageCategoryCode.News Then
                    lnkEdit.NavigateUrl = "newsEdit.aspx?Id=" + id.ToString
                Else
                    lnkEdit.NavigateUrl = "pageEdit.aspx?Id=" + id.ToString
                End If

                Dim url As String = DataBinder.Eval(e.Item.DataItem, "url")
                e.Item.Cells(10).Text = "<a href='" + url + "&draft=1' class='gridlink' target='_blank'>preview</a>"
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 106
            End Get
        End Property

    End Class

End Namespace