Imports ba = Foundation.BusinessAdmin
Imports c = Core
Imports dt = Foundation.Data

Namespace settings

    Public Class roleList
        Inherits ba.BasePage

        Protected WithEvents dgrRoles As c.MySortGrid
        Protected pnlGrid As Panel

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            dgrRoles.BindingMethod = AddressOf BindData
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            Dim roles As DataTable = New dt.RoleData().GetRecords(dgrRoles.SortExpression)
            If roles.Rows.Count > 0 Then
                pnlGrid.Visible = True
                dgrRoles.Prefix = LinkPrefix
                dgrRoles.DataSource = roles
                dgrRoles.DataBind()
            Else
                pnlGrid.Visible = False
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 65
            End Get
        End Property

        Private Sub dgrRoles_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrRoles.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then

                Dim lnkPerms As HyperLink = e.Item.Cells(4).Controls(0)
                Dim lnkUsers As HyperLink = e.Item.Cells(5).Controls(0)
                Dim lnkEdit As HyperLink = e.Item.Cells(6).Controls(0)
                Dim deleteLink As LinkButton = e.Item.Cells(7).Controls(0)
                lnkPerms.CssClass = "gridlink"
                lnkUsers.CssClass = "gridlink"
                lnkEdit.CssClass = "gridlink"
                deleteLink.CssClass = "gridlinkalert"

                Dim role As String = DataBinder.Eval(e.Item.DataItem, "Name").ToString().Replace("'", "")
                If (role = dt.RoleData.Administrator_Role Or Not HasPermission(ba.PermissionCode.SystemSettings_Roles_ViewModify)) Then
                    lnkPerms.Visible = False
                    lnkUsers.Visible = False
                    lnkEdit.Visible = False
                    deleteLink.Visible = False
                Else
                    deleteLink.Attributes("onclick") = "javascript:return confirm('Are you sure you want to delete the " + role + " role?')"
                End If
            End If
        End Sub

        Private Sub dgrRoles_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgrRoles.ItemCommand
            If e.CommandName = "Delete" Then
                Dim dtRole As New dt.RoleData
                dtRole.Delete(dgrRoles.DataKeys(e.Item.ItemIndex))
                BindData()
            End If
        End Sub
    End Class

End Namespace
