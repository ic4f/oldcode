Imports dt = Foundation.Data
Imports c = Core

Public Class labels
    Inherits System.Web.UI.Page

    Protected cblLabels As CheckBoxList

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            BindData()
        End If
        MyBase.OnLoad(e)
    End Sub

    Private Sub BindData()
        cblLabels.DataSource = New dt.ContentLabelData().GetList()
        cblLabels.DataTextField = "Text"
        cblLabels.DataValueField = "Id"
        cblLabels.DataBind()
    End Sub

End Class