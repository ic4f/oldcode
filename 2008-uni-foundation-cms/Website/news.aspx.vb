Imports dt = Foundation.Data

Public Class news
    Inherits BasePage

    Protected ltrNewsDate As Literal
    Protected ltrNewsTitle As Literal
    Protected ltrNewsBody As Literal

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Private Sub BindData()
        Dim dtn As New dt.NewsPageData
        Dim dtNews As New dt.NewsPage(dtn.GetIdByPageId(PageRecord.Id))
        ltrNewsDate.Text = dtNews.DisplayedPublishedDate.ToShortDateString()
        ltrNewsTitle.Text = PageRecord.ContentTitle
        If IsDraft Then
            ltrNewsBody.Text = "<p>" + Server.HtmlDecode(PageRecord.BodyDraft)
        Else
            ltrNewsBody.Text = "<p>" + Server.HtmlDecode(PageRecord.Body)
        End If
    End Sub

End Class
