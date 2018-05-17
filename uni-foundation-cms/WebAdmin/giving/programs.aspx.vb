Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Namespace giving

    Public Class programs
        Inherits ba.BasePage

        Protected pnlView As Panel
        Protected pnlModify As Panel
        Protected pnlPageGroupMenu As Panel

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                DisplayContent()
                CheckPageCategoryMenu()
            End If
        End Sub

        Private Sub DisplayContent()
            pnlView.Visible = False
            pnlModify.Visible = False
            If HasPermission(ba.PermissionCode.Giving_Programs_View) Or
            HasPermission(ba.PermissionCode.Giving_Programs_ViewModify) Or
            HasPermission(ba.PermissionCode.Giving_Programs_ViewModifyPublish) Then
                pnlView.Visible = True
            End If
            If HasPermission(ba.PermissionCode.Giving_Programs_ViewModify) Or
            HasPermission(ba.PermissionCode.Giving_Programs_ViewModifyPublish) Then
                pnlModify.Visible = True
            End If
        End Sub

        Private Sub CheckPageCategoryMenu()
            If Not (New dt.PageCategoryData().HasMenu(ba.PageCategoryCode.College) _
                      And New dt.PageCategoryData().HasMenu(ba.PageCategoryCode.Department) _
                      And New dt.PageCategoryData().HasMenu(ba.PageCategoryCode.Program)) Then
                pnlView.Visible = False
                pnlModify.Visible = False
                pnlPageGroupMenu.Visible = True
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 30
            End Get
        End Property

    End Class

End Namespace