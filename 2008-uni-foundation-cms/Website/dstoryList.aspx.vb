Imports bm = Foundation.BusinessMain
Imports dt = Foundation.Data

Public Class dstoryList
    Inherits BasePage

    Protected WithEvents rptDStories As Repeater

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub BindData()
        rptDStories.DataSource = New dt.DStoryPageData().GetDStoriesForPublicDStoryList()
        rptDStories.DataBind()
    End Sub

    Private Sub rptDStories_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDStories.ItemDataBound
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then

            Dim ltrImage As Literal = e.Item.FindControl("ltrImage")
            Dim ltrTitle As Literal = e.Item.FindControl("ltrTitle")
            Dim ltrSummary As Literal = e.Item.FindControl("ltrSummary")


            Dim pageId As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "id"))
            Dim url As String = DataBinder.Eval(e.Item.DataItem, "url").ToString()
            Dim publisheddate As DateTime = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "publisheddate"))
            Dim hasImage As Boolean = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "hasimage"))
            Dim dstoryId As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "dstoryId"))

            ltrTitle.Text = "<a class='dstory-list-title-link' href='" + url + "'>" + DataBinder.Eval(e.Item.DataItem, "contenttitle").ToString() + "</a>"
            ltrSummary.Text = "<a class='dstory-list-summary-link' href='" + url + "'>" + DataBinder.Eval(e.Item.DataItem, "summary").ToString() + " [...read more]</a>"

            If hasImage Then
                ltrImage.Text = "<a href='" + url + "'><img align='left' src='" + bm.UrlHelper.GetDStoryImageUrl(dstoryId) + "' style='border:1px #414141 solid;margin-right:10px;margin-bottom:3px;'></a>"
            End If

        End If
    End Sub

End Class
