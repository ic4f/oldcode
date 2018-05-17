Imports ba = Foundation.BusinessAdmin

Namespace contentlibrary

  Public Class _default
    Inherits ba.BasePage

    Protected pnlHeaderImages As Panel
    Protected pnlQuotes As Panel
    Protected pnlModules As Panel
    Protected pnlFiles As Panel
    Protected pnlImages As Panel

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
      If Not Page.IsPostBack Then
        DisplayContent()
      End If
    End Sub

    Private Sub DisplayContent()
      pnlHeaderImages.Visible = False
      pnlQuotes.Visible = False
      pnlModules.Visible = False
      pnlFiles.Visible = False
      pnlImages.Visible = False

      If HasPermission(ba.PermissionCode.Library_HeaderImages_View) Or HasPermission(ba.PermissionCode.Library_HeaderImages_ViewModify) Then
        pnlHeaderImages.Visible = True
      End If
      If HasPermission(ba.PermissionCode.Library_Quotes_View) Or HasPermission(ba.PermissionCode.Library_Quotes_ViewModify) Then
        pnlQuotes.Visible = True
      End If
      If HasPermission(ba.PermissionCode.Library_Modules_View) Or HasPermission(ba.PermissionCode.Library_Modules_ViewModify) Then
        pnlModules.Visible = True
      End If
      If HasPermission(ba.PermissionCode.Library_Files_View) Or HasPermission(ba.PermissionCode.Library_Files_ViewModify) Then
        pnlFiles.Visible = True
        pnlImages.Visible = True
      End If
    End Sub

    Protected Overrides ReadOnly Property PageId() As Integer
      Get
        Return 34
      End Get
    End Property

  End Class

End Namespace