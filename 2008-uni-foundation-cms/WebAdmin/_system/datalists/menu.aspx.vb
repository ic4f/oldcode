Imports dt = Foundation.Data
Imports ba = Foundation.BusinessAdmin
Imports c = Core

Public Class menu
    Inherits System.Web.UI.Page

    Protected rblMenu As RadioButtonList
    Protected ltrTreeDisplay As Literal
    Protected lnkLevelUp1 As HyperLink
    Protected lnkLevelUp2 As HyperLink

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            BindMenu()
        End If
        MyBase.OnLoad(e)
    End Sub

    Private Sub BindMenu()
        Dim toeditmenuid As Integer = GetToEditMenuId()
        Dim dt As DataTable = New dt.StagingMenuData().GetOrdered().Tables(0)
        Dim pmt As New ba.PublicMenuTree(ba.PublicMenuTree.ConvertData(dt), toeditmenuid, "../../")
        ltrTreeDisplay.Text = pmt.GetMenuForCmsMenuSelection()
    End Sub

    Private Function GetToEditMenuId() As Integer
        If Request("ToEditId") = "" Then
            Return c.Tree.ROOT_ID 'which means no menu is selected, since a page cannot belong to the root menu
        Else
            Return Convert.ToInt32(Request("ToEditId"))
        End If
    End Function

End Class