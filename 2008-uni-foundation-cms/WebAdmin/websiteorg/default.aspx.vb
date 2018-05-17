Imports ba = Foundation.BusinessAdmin

Namespace websiteorg

    Public Class _default
        Inherits ba.BasePage

        Protected pnlMenus As Panel
        Protected pnlPageGroups As Panel

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                DisplayContent()
            End If
        End Sub

        Private Sub DisplayContent()
            pnlMenus.Visible = False
            pnlPageGroups.Visible = False

            If HasPermission(ba.PermissionCode.WebsiteOrg_Menus_ViewModify) Or HasPermission(ba.PermissionCode.WebsiteOrg_Menus_ViewModifyPublish) Then
                pnlMenus.Visible = True
            End If

            If HasPermission(ba.PermissionCode.WebsiteOrg_PageGroups_View) Then
                pnlPageGroups.Visible = True
            End If

        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 96
            End Get
        End Property

    End Class

End Namespace