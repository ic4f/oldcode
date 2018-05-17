Imports ba = Foundation.BusinessAdmin

Namespace contentorg

    Public Class _default
        Inherits ba.BasePage

        Protected pnlFileTypes As Panel
        Protected pnlLabels As Panel
        Protected pnlTags As Panel

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                DisplayContent()
            End If
        End Sub

        Private Sub DisplayContent()
            pnlFileTypes.Visible = False
            pnlLabels.Visible = False
            pnlTags.Visible = False

            If HasPermission(ba.PermissionCode.Library_Files_View) Or HasPermission(ba.PermissionCode.ContentOrg_FileTypes_ViewModify) Then
                pnlFileTypes.Visible = True
            End If
            If HasPermission(ba.PermissionCode.ContentOrg_Labels_View) Or HasPermission(ba.PermissionCode.ContentOrg_Labels_ViewModify) Then
                pnlLabels.Visible = True
            End If
            If HasPermission(ba.PermissionCode.ContentOrg_Tags_View) Or HasPermission(ba.PermissionCode.ContentOrg_Tags_ViewModify) Then
                pnlTags.Visible = True
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 95
            End Get
        End Property

    End Class

End Namespace