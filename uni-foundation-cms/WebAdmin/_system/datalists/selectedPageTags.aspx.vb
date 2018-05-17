Imports dt = Foundation.Data
Imports c = Core

Public Class selectedPageTags
    Inherits System.Web.UI.Page

    Protected cblTags As c.DataCheckboxList

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            BindData()
        End If
        MyBase.OnLoad(e)
    End Sub

    Private Sub BindData()
        cblTags.DataSource = New dt.TagData().GetAllTagsByPage(GetId())
        cblTags.DataTextField = "Text"
        cblTags.DataValueField = "Id"
        cblTags.DataCheckField = "selected"
        cblTags.DataBind()
    End Sub

    Private Function GetId() As Integer
        Return Convert.ToInt32(Request("Id"))
    End Function

End Class