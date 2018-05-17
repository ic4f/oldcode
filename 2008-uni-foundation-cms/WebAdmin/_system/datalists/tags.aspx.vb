Imports dt = Foundation.Data
Imports c = Core

Public Class tags
    Inherits System.Web.UI.Page

    Protected cblTags As CheckBoxList

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            BindData()
        End If
        MyBase.OnLoad(e)
    End Sub

    Private Sub BindData()
        cblTags.DataSource = New dt.TagData().GetList()
        cblTags.DataTextField = "Text"
        cblTags.DataValueField = "Id"
        cblTags.DataBind()
    End Sub

End Class