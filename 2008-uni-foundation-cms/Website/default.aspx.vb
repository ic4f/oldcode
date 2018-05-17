Imports bm = Foundation.BusinessMain
Imports dt = Foundation.Data

Public Class _default
    Inherits BasePage

    Protected pnlFeature As Panel
    Protected ltrFeature As Literal

    Protected pnl100 As Panel
    Protected pnl5050 As Panel
    Protected pnlDStories As Panel
    Protected pnlPrograms As Panel

    Protected WithEvents rptNews100 As Repeater
    Protected WithEvents rptNews5050 As Repeater
    Protected WithEvents rptDStories As Repeater
    Protected WithEvents rptPrograms As Repeater

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Private Sub BindData()
        BindFeature()
        BindPanels()
    End Sub

    Private Sub BindFeature()
        Dim feature As String = PageRecord.Body
        If feature = "" Then
            pnlFeature.Visible = False
        Else
            ltrFeature.Text = Server.HtmlDecode(feature)
        End If
    End Sub

    Private Sub BindPanels()
        Dim dtNews As DataTable = New dt.NewsPageData().GetNewsForHomepage()
        Dim dtPrograms As DataTable = New dt.ProgramPageData().GetProgramsForHomepage()
        Dim dtDStories As DataTable = New dt.DStoryPageData().GetDStoriesForHomepage()
        If Not (dtNews.Rows.Count = 0 And dtPrograms.Rows.Count = 0 And dtDStories.Rows.Count = 0) Then
            If dtPrograms.Rows.Count = 0 And dtDStories.Rows.Count = 0 Then
                pnl100.Visible = True
                pnl5050.Visible = False
                BindOneColumnLayout(dtNews)
            Else
                pnl100.Visible = False
                pnl5050.Visible = True
                BindTwoColumnLayout(dtNews, dtPrograms, dtDStories)
            End If
        End If
    End Sub

    Private Sub BindOneColumnLayout(ByVal dtNews As DataTable)
        rptNews100.DataSource = dtNews
        rptNews100.DataBind()
    End Sub

    Private Sub BindTwoColumnLayout(ByVal dtNews As DataTable, ByVal dtPrograms As DataTable, ByVal dtDStories As DataTable)

        rptNews5050.DataSource = dtNews
        rptNews5050.DataBind()

        If dtDStories.Rows.Count > 0 Then
            pnlDStories.Visible = True
            rptDStories.DataSource = dtDStories
            rptDStories.DataBind()
        Else
            pnlDStories.Visible = False
        End If
        If dtPrograms.Rows.Count > 0 Then
            pnlPrograms.Visible = True
            rptPrograms.DataSource = dtPrograms
            rptPrograms.DataBind()
        Else
            pnlPrograms.Visible = False
        End If
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

    Private Sub rptPrograms_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptPrograms.ItemDataBound
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim lnkProgram As HyperLink = e.Item.FindControl("lnkProgram")
            lnkProgram.Text = DataBinder.Eval(e.Item.DataItem, "contenttitle").ToString()
            lnkProgram.NavigateUrl = DataBinder.Eval(e.Item.DataItem, "url").ToString()
        End If
    End Sub

    Private Sub rptNews100_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptNews100.ItemDataBound
        NewsItemDataBound(sender, e)
    End Sub

    Private Sub rptNews5050_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptNews5050.ItemDataBound
        NewsItemDataBound(sender, e)
    End Sub

    Private Sub NewsItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then

            Dim pageId As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "id"))
            Dim title As String = DataBinder.Eval(e.Item.DataItem, "contenttitle").ToString()
            Dim summary As String = DataBinder.Eval(e.Item.DataItem, "summary").ToString()
            Dim url As String = DataBinder.Eval(e.Item.DataItem, "url").ToString()
            Dim publisheddate As DateTime = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "displayedpublished"))
            Dim hasImage As Boolean = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "hasimage"))
            Dim isHighlighted As Boolean = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "ishighlighted"))
            Dim newsId As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "newsId"))

            Dim lblDate As Label = e.Item.FindControl("lblDate")
            Dim lnkTitle As HyperLink = e.Item.FindControl("lnkTitle")
            Dim ltrImage As Literal = e.Item.FindControl("ltrImage")

            lblDate.Text = String.Format("{0:MMMM d, yyyy}", publisheddate)
            lnkTitle.Text = title
            lnkTitle.NavigateUrl = url

            If hasImage Then
                ltrImage.Text = "<a href='" + url + "'><img src='" + bm.UrlHelper.GetNewsImageUrl(newsId) + "' style='border:1px #414141 solid;'></a>"
            End If

            If isHighlighted Then
                Dim ltrHighlightDivOpen As Literal = e.Item.FindControl("ltrHighlightDivOpen")
                Dim ltrHighlightDivClose As Literal = e.Item.FindControl("ltrHighlightDivClose")
                ltrHighlightDivOpen.Text = "<div class='news-list-div-highlight'>"
                ltrHighlightDivClose.Text = "</div>"
            End If

        End If

    End Sub

End Class
