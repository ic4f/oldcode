Imports System.IO
Imports System.Text
Imports ba = Foundation.BusinessAdmin
Imports cr = Core
Imports dt = Foundation.Data

Namespace contentlibrary

    Public Class moduleList
        Inherits ba.BasePage

        Protected WithEvents dgrModules As cr.MyBaseGrid
        Protected pnlGrid As Panel
        Protected ddlLabel As DropDownList
        Protected WithEvents btnSearch As Button
        Protected ltrCount As Literal

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            dgrModules.BindingMethod = AddressOf BindData
            If Not Page.IsPostBack Then
                BindLists()
                BindLabelId()
                BindData()
            End If
        End Sub

        Private Sub BindLists()
            ddlLabel.DataSource = New dt.ContentLabelData().GetList()
            ddlLabel.DataTextField = "text"
            ddlLabel.DataValueField = "id"
            ddlLabel.DataBind()
            ddlLabel.Items.Insert(0, New ListItem("-- select all", "0"))
        End Sub

        Private Sub BindLabelId()
            If Request("LabelId") <> "" Then
                ddlLabel.SelectedValue = Request("LabelId")
            End If
        End Sub

        Private Sub BindData()
            Dim dt As DataTable
            If (ddlLabel.SelectedIndex > 0) Then
                dt = New dt.ModuleData().GetModulesByLabelId(Convert.ToInt32(ddlLabel.SelectedValue))
            Else
                dt = New dt.ModuleData().GetModules()
            End If

            ltrCount.Text = dt.Rows.Count.ToString

            If (dt.Rows.Count > 0) Then
                pnlGrid.Visible = True
                dgrModules.Prefix = LinkPrefix
                dgrModules.DataSource = dt
                dgrModules.DataBind()
            Else
                pnlGrid.Visible = False
            End If
        End Sub

        Private Sub dgrLabels_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgrModules.ItemCommand
            If e.CommandName = "Delete" Then
                Dim moduleId As Integer = dgrModules.DataKeys(e.Item.ItemIndex)
                Dim m As New dt.Module(moduleId)
                If m.ImageExtension <> "" Then
                    File.Delete(Server.MapPath(ba.UrlHelper.GetModuleImageUrl(moduleId.ToString, m.ImageExtension)))
                End If
                Dim dm As New dt.ModuleData
                dm.Delete(moduleId)

            ElseIf e.CommandName = "Up" Then
                Dim moduleId As Integer = dgrModules.DataKeys(e.Item.ItemIndex)
                Dim dt As DataTable = New dt.ModuleData().GetRecords()
                Dim ids(dt.Rows.Count) As Integer
                Dim temp As Integer
                For pos As Integer = 0 To dt.Rows.Count - 1
                    ids(pos) = dt.Rows(pos)(0)
                    If (dt.Rows(pos)(0) = moduleId And pos > 0) Then
                        temp = ids(pos - 1)
                        ids(pos - 1) = moduleId
                        ids(pos) = temp
                    End If
                Next
                Dim md As New dt.ModuleData
                Dim rank As Integer = 0
                For Each currid As Integer In ids
                    md.UpdateRank(currid, rank, Identity.UserId)
                    rank = rank + 1
                Next

            ElseIf e.CommandName = "Down" Then
                Dim moduleId As Integer = dgrModules.DataKeys(e.Item.ItemIndex)
                Dim dt As DataTable = New dt.ModuleData().GetRecords()
                Dim ids(dt.Rows.Count) As Integer
                For pos As Integer = 0 To dt.Rows.Count - 1
                    ids(pos) = dt.Rows(pos)(0)
                    If (dt.Rows(pos)(0) = moduleId And pos < dt.Rows.Count - 1) Then
                        ids(pos) = dt.Rows(pos + 1)(0)
                        ids(pos + 1) = moduleId
                        pos = pos + 2
                    End If
                Next
                Dim md As New dt.ModuleData
                Dim rank As Integer = 0
                For Each currid As Integer In ids
                    md.UpdateRank(currid, rank, Identity.UserId)
                    rank = rank + 1
                Next
            End If
            BindData()
        End Sub

        Private Sub dgrLabels_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrModules.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then

                Dim lnkPages As HyperLink = e.Item.Cells(4).Controls(0)
                Dim lnkEdit As HyperLink = e.Item.Cells(8).Controls(0)
                Dim lnkPreview As HyperLink = e.Item.Cells(9).Controls(0)
                Dim deleteLink As LinkButton = e.Item.Cells(10).Controls(0)
                lnkPages.CssClass = "gridlink"
                lnkEdit.CssClass = "gridlink"
                lnkPreview.CssClass = "gridlink"
                deleteLink.CssClass = "gridlinkalert"

                Dim isrequired As Boolean = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "isrequired"))
                If isrequired Then
                    lnkPages.Text = "all pages"
                    lnkPages.Enabled = False
                End If

                Dim pagecount As Integer = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "pagecount"))
                If pagecount = 0 And Not isrequired Then
                    lnkPages.Enabled = False
                End If

                Dim isarchived As Boolean = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "isarchived"))
                If isarchived Then
                    e.Item.Cells(3).Text = "<span style='color:maroon;'>archived</span>"
                Else
                    e.Item.Cells(3).Text = "<span style='color:green;'>current</span>"
                End If

                Dim labelText As String = e.Item.Cells(1).Text.ToUpper
                deleteLink.Attributes("onclick") = "javascript:return confirm('Are you sure you want to delete module " + labelText + " ?')"
            End If
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            BindData()
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 44
            End Get
        End Property

    End Class

End Namespace