Imports bm = Foundation.BusinessMain
Imports dt = Foundation.Data

Public Class newsList
    Inherits BasePage

    Protected WithEvents rptNews As Repeater
    Private currYear As Integer
    Private currMonth As Integer

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub BindData()
        rptNews.DataSource = New dt.NewsPageData().GetNewsForPublicNewsList()
        rptNews.DataBind()
    End Sub

    Private Sub rptNews_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptNews.ItemDataBound
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then

            Dim pageId As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "id"))
            Dim title As String = DataBinder.Eval(e.Item.DataItem, "contenttitle").ToString()
            Dim summary As String = DataBinder.Eval(e.Item.DataItem, "summary").ToString()
            Dim url As String = DataBinder.Eval(e.Item.DataItem, "url").ToString()
            Dim publisheddate As DateTime = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "displayedpublished"))
            Dim hasImage As Boolean = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "hasimage"))
            Dim isHighlighted As Boolean = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "ishighlighted"))
            Dim year As Integer = publisheddate.Year
            Dim month As Integer = publisheddate.Month
            Dim newsId As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "newsId"))

            Dim lblYear As Label = e.Item.FindControl("lblYear")
            Dim lblMonth As Label = e.Item.FindControl("lblMonth")
            Dim lblDate As Label = e.Item.FindControl("lblDate")
            Dim lnkTitle As HyperLink = e.Item.FindControl("lnkTitle")
            Dim ltrImage As Literal = e.Item.FindControl("ltrImage")

            lblDate.Text = String.Format("{0:MMMM d, yyyy}", publisheddate)
            lnkTitle.Text = title
            lnkTitle.NavigateUrl = url

            If currYear <> year Then
                lblYear.Text = "<p style='border-bottom: solid 1px #cacaca;'><span class='news-list-year'>" + year.ToString() + "</span></p>"
                currYear = year
            Else
                lblYear.Text = ""
            End If

            If currMonth <> month Then
                lblMonth.Text = "<p><span class='news-list-month'>" + getMonth(month) + "</span></p>"
                currMonth = month
            Else
                lblYear.Text = ""
            End If

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

    Private Function getMonth(ByVal m As Integer) As String
        Select Case m
            Case 1
                Return "January"
            Case 2
                Return "February"
            Case 3
                Return "March"
            Case 4
                Return "April"
            Case 5
                Return "May"
            Case 6
                Return "June"
            Case 7
                Return "July"
            Case 8
                Return "August"
            Case 9
                Return "September"
            Case 10
                Return "October"
            Case 11
                Return "November"
            Case 12
                Return "December"
            Case Else
                Return ""
        End Select
    End Function

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        currYear = 0
        currMonth = 0
        MyBase.OnInit(e)
    End Sub

End Class
