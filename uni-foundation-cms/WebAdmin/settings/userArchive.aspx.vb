Imports ba = Foundation.BusinessAdmin
Imports c = Core
Imports dt = Foundation.Data

Namespace settings

    Public Class userArchive
        Inherits ba.BasePage

        Protected WithEvents dgrUsers As c.MySortGrid
        Protected pnlGrid As Panel

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            dgrUsers.BindingMethod = AddressOf BindData
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            Dim users As DataTable = New dt.CmsUserData().GetArchivedUsers(dgrUsers.SortExpression)
            If users.Rows.Count > 0 Then
                pnlGrid.Visible = True
                dgrUsers.Prefix = LinkPrefix
                dgrUsers.DataSource = users
                dgrUsers.DataBind()
            Else
                pnlGrid.Visible = False
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 109
            End Get
        End Property

        Private Sub dgrUsers_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrUsers.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then

                Dim lnkActivate As LinkButton = e.Item.Cells(5).Controls(0)
                lnkActivate.CssClass = "gridlinkalert"

                Dim name As String = DataBinder.Eval(e.Item.DataItem, "DisplayedName").ToString().Replace("'", "")
                If (Not HasPermission(ba.PermissionCode.SystemSettings_Users_ViewModify)) Then
                    lnkActivate.Visible = False
                Else
                    lnkActivate.Attributes("onclick") = "javascript:return confirm('Are you sure you want to activate " + name + " account?')"
                End If
            End If
        End Sub

        Private Sub dgrUsers_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgrUsers.ItemCommand
            If e.CommandName = "Activate" Then
                Dim ud As New dt.CmsUserData
                ud.Activate(dgrUsers.DataKeys(e.Item.ItemIndex))
                Response.Redirect("userList.aspx")
            End If
        End Sub

    End Class

End Namespace
