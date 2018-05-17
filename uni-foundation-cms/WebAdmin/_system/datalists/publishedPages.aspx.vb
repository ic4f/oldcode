Imports System.Text
Imports ba = Foundation.BusinessAdmin
Imports cr = Core
Imports dt = Foundation.Data

Public Class publishedPages
    Inherits System.Web.UI.Page

    Protected ddlLabel As DropDownList
    Protected ddlType As DropDownList
    Protected ltrTreeDisplay As Literal
    Protected hidMenu As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnSearch As Button
    Protected WithEvents btnReset As Button
    Protected radPages As RadioButtonList
    Protected WithEvents btnClear As Button

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            btnSearch.Attributes.Add("OnClick", "getMenuId();")
            BindLists()
            BindMenu()
            BindData(True)
            If Request("PageId") <> "" Then
                btnClear.Visible = True
            End If
        End If
        MyBase.OnLoad(e)
    End Sub

    Private Function getMenuId() As Integer
        Return Convert.ToInt32(hidMenu.Value)
    End Function

    Private Sub BindMenu()
        Dim dt As DataTable = New dt.StagingMenuData().GetOrdered().Tables(0)
        Dim pmt As New ba.PublicMenuTree(ba.PublicMenuTree.ConvertData(dt), getMenuId(), "../../")
        ltrTreeDisplay.Text = pmt.GetMenuForCmsMenuSelection()
    End Sub

    Private Sub BindLists()
        ddlLabel.DataSource = New dt.ContentLabelData().GetList()
        ddlLabel.DataTextField = "text"
        ddlLabel.DataValueField = "id"
        ddlLabel.DataBind()
        ddlLabel.Items.Insert(0, New ListItem("-- select all", "0"))

        ddlType.DataSource = New dt.PageCategoryData().GetList()
        ddlType.DataTextField = "text"
        ddlType.DataValueField = "id"
        ddlType.DataBind()
        ddlType.Items.Insert(0, New ListItem("-- select all", "0"))
    End Sub

    Private Sub BindData(ByVal showSelection As Boolean)
        Dim pages As DataTable

        Dim menuid As Integer = getMenuId()

        If (ddlType.SelectedIndex > 0 Or ddlLabel.SelectedIndex > 0 Or menuid > 0) Then
            pages = New dt.PageData().GetPublishedPagesListWithQuery(makeQuery(menuid))
        Else
            pages = New dt.PageData().GetPublishedPagesList()
        End If

        radPages.DataSource = pages
        radPages.DataTextField = "description"
        radPages.DataValueField = "id"
        radPages.DataBind()

        If showSelection Then
            If Request("PageId") <> "" Then
                Dim pageId As Integer = Convert.ToInt32(Request("PageId"))
                For Each dr As DataRow In pages.Rows
                    If pageId = Convert.ToInt32(dr(0)) Then
                        radPages.SelectedValue = pageId
                    End If
                Next
            End If
        End If
    End Sub

    Private Function makeQuery(ByVal menuid As Integer) As String
        Dim sb As New StringBuilder

        sb.Append(" and ")

        If (ddlType.SelectedIndex > 0) Then
            sb.AppendFormat(" p.pagecategoryid = {0} and ", ddlType.SelectedValue)
        End If

        If (ddlLabel.SelectedIndex > 0) Then
            sb.AppendFormat(" pll.contentlabelid = {0} and ", ddlLabel.SelectedValue)
        End If

        If (menuid > 0) Then
            sb.AppendFormat(" m.id = {0} and ", menuid)
        End If

        Dim temp As String = sb.ToString
        temp = temp.Substring(0, temp.Length - 4)
        Return temp
    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindMenu()
        BindData(True)
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        hidMenu.Value = "-1"
        BindLists()
        BindMenu()
        BindData(True)
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        BindData(False)
    End Sub
End Class