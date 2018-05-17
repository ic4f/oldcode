Imports ba = Foundation.BusinessAdmin

Namespace settings

    Public Class _default
        Inherits ba.BasePage

        Protected pnlUsers As Panel
        Protected pnlRoles As Panel
        Protected pnlPages As Panel

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                DisplayContent()
            End If
        End Sub

        Private Sub DisplayContent()
            pnlUsers.Visible = False
            pnlRoles.Visible = False
            pnlPages.Visible = False
            If HasPermission(ba.PermissionCode.SystemSettings_Users_View) Or HasPermission(ba.PermissionCode.SystemSettings_Users_ViewModify) Then
                pnlUsers.Visible = True
            End If
            If HasPermission(ba.PermissionCode.SystemSettings_Roles_View) Or HasPermission(ba.PermissionCode.SystemSettings_Roles_ViewModify) Then
                pnlRoles.Visible = True
            End If
            If HasPermission(ba.PermissionCode.SystemSettings_CustomPages_ViewModify) Then
                pnlPages.Visible = True
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 57
            End Get
        End Property

    End Class

End Namespace
