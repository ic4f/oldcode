Imports dt = Foundation.Data
Imports c = Core

Public Class selectedFileLabels
    Inherits System.Web.UI.Page

    Protected cblLabels As c.DataCheckboxList

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            BindData()
        End If
        MyBase.OnLoad(e)
    End Sub

    Private Sub BindData()
        cblLabels.DataSource = New dt.ContentLabelData().GetAllLabelsByFile(GetId())
        cblLabels.DataTextField = "Text"
        cblLabels.DataValueField = "Id"
        cblLabels.DataCheckField = "selected"
        cblLabels.DataBind()
    End Sub

    Private Function GetId() As Integer
        Return Convert.ToInt32(Request("FileId"))
    End Function

End Class