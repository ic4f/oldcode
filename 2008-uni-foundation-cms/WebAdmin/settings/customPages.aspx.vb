Imports ba = Foundation.BusinessAdmin

Namespace settings

    Public Class customPages
        Inherits ba.BasePage

        Protected pnlModify As Panel

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                DisplayContent()
            End If
        End Sub

        Private Sub DisplayContent()
            pnlModify.Visible = False
            If HasPermission(ba.PermissionCode.SystemSettings_CustomPages_ViewModify) Then
                pnlModify.Visible = True
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 100
            End Get
        End Property

    End Class

End Namespace