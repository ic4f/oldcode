'Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Public Class LayoutHelper
  Inherits System.Web.UI.UserControl

  Protected ltrMenu As Literal

  Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
  End Sub

  'Public Sub LoadMenu(ByVal pageId As Integer)
  '  Dim currMenuId = -1
  '  Dim dtMenu As DataTable = New dt.StagingMenuData().GetOrdered().Tables(0)
  '  Dim menuTree As New bs.MenuTree(dtMenu, currMenuId)
  '  ltrMenu.Text = menuTree.GetStandardPageHtml()
  'End Sub

End Class
