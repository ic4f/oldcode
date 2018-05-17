Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Namespace webpages

    Public Class news
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
            If HasPermission(ba.PermissionCode.WebPages_News_View) Or
            HasPermission(ba.PermissionCode.WebPages_News_ViewModify) Or
            HasPermission(ba.PermissionCode.WebPages_News_ViewModifyPublish) Then
                pnlView.Visible = True
            End If
            If HasPermission(ba.PermissionCode.WebPages_News_ViewModify) Or
            HasPermission(ba.PermissionCode.WebPages_News_ViewModifyPublish) Then
                pnlModify.Visible = True
            End If
        End Sub

        Private Sub CheckPageCategoryMenu()
            If Not New dt.PageCategoryData().HasMenu(ba.PageCategoryCode.News) Then
                pnlView.Visible = False
                pnlModify.Visible = False
                pnlPageGroupMenu.Visible = True
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 9
            End Get
        End Property

    End Class

End Namespace