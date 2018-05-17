Imports dt = Foundation.Data
Imports c = Core

Public Class modules
    Inherits System.Web.UI.Page

    Protected cblModules As CheckBoxList

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            BindData()
        End If
        MyBase.OnLoad(e)
    End Sub

    Private Sub BindData()
        cblModules.DataSource = New dt.ModuleData().GetModulesForPageAssingment()
        cblModules.DataTextField = "AdminTitle"
        cblModules.DataValueField = "Id"
        cblModules.DataBind()
    End Sub

End Class