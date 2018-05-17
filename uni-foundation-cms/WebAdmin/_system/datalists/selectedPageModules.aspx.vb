Imports dt = Foundation.Data
Imports c = Core

Public Class selectedPageModules
    Inherits System.Web.UI.Page

    Protected cblModules As c.DataCheckboxList

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            BindData()
        End If
        MyBase.OnLoad(e)
    End Sub

    Private Sub BindData()
        cblModules.DataSource = New dt.ModuleData().GetModulesForPageAssingmentByPage(GetId())
        cblModules.DataTextField = "AdminTitle"
        cblModules.DataValueField = "Id"
        cblModules.DataCheckField = "selected"
        cblModules.DataBind()
    End Sub

    Private Function GetId() As Integer
        Return Convert.ToInt32(Request("Id"))
    End Function

End Class