Imports bm = Foundation.BusinessMain
Imports dt = Foundation.Data

Public Class categories
    Inherits BasePage

    Protected WithEvents rptCats As Repeater
    Protected ltrTitle As Literal

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub BindData()
        rptCats.DataSource = New dt.PageData().GetAllPagesByTag(Convert.ToInt32(Request("catid")))
        rptCats.DataBind()
        Dim tag As New dt.Tag(Convert.ToInt32(Request("catid")))
        ltrTitle.Text = "Pages Listed Under """ + tag.Text + """"
    End Sub

    Private Sub rptCats_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptCats.ItemDataBound
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim lnkPage As HyperLink = e.Item.FindControl("lnkPage")
            lnkPage.Text = DataBinder.Eval(e.Item.DataItem, "contenttitle").ToString()
            lnkPage.NavigateUrl = DataBinder.Eval(e.Item.DataItem, "url").ToString()
        End If
    End Sub

End Class
