Imports bm = Foundation.BusinessMain
Imports dt = Foundation.Data

Public Class collegeList
    Inherits BasePage

    Protected WithEvents rptColleges As Repeater

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub BindData()
        rptColleges.DataSource = New dt.CollegePageData().GetPublishedCollegePages()
        rptColleges.DataBind()
    End Sub

    Private Sub rptColleges_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptColleges.ItemDataBound
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim lnkCollege As HyperLink = e.Item.FindControl("lnkCollege")
            lnkCollege.Text = DataBinder.Eval(e.Item.DataItem, "contenttitle").ToString()
            lnkCollege.NavigateUrl = DataBinder.Eval(e.Item.DataItem, "url").ToString()
        End If
    End Sub

End Class
