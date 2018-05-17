Imports ba = Foundation.BusinessAdmin
Imports cr = Core
Imports dt = Foundation.Data

Namespace contentorg

    Public Class labels
        Inherits ba.BasePage

        Protected pnlView As Panel
        Protected pnlModify As Panel

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                DisplayContent()
            End If
        End Sub

        Private Sub DisplayContent()
            pnlView.Visible = False
            pnlModify.Visible = False
            If HasPermission(ba.PermissionCode.ContentOrg_Labels_View) Or HasPermission(ba.PermissionCode.ContentOrg_Labels_ViewModify) Then
                pnlView.Visible = True
            End If
            If HasPermission(ba.PermissionCode.ContentOrg_Labels_ViewModify) Then
                pnlModify.Visible = True
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 53
            End Get
        End Property

    End Class

End Namespace