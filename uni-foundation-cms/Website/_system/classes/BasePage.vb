Imports System.Text
Imports bm = Foundation.BusinessMain
Imports dt = Foundation.Data

Public MustInherit Class BasePage
    Inherits System.Web.UI.Page

    Protected ucLayoutHelper1 As LayoutHelper1
    Protected ucLayoutHelper2 As LayoutHelper2
    Protected htmlTitle As HtmlGenericControl
    Private myPage As dt.Page

    Protected ltrTags As Literal
    Protected ltrPrintable As Literal
    Protected ltrRevised As Literal
    Protected ltrBookmarks As Literal

    Protected Overrides Sub OnInit(ByVal e As EventArgs)
        AddHandler Me.Load, AddressOf Page_Load
        AddHandler Me.Error, AddressOf Page_Error
        MyBase.OnInit(e)
    End Sub

    Protected ReadOnly Property IsDraft() As Boolean
        Get
            Return (Request("draft") = "1")
        End Get
    End Property

    Protected ReadOnly Property PageRecord() As dt.Page
        Get
            Return myPage
        End Get
    End Property

    Private Function IsHomepage() As Boolean
        Dim path = Page.Request.Path.ToLower
        Return path.IndexOf("default.aspx") > -1
    End Function

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            If Request("id") = "" And IsHomepage() Then
                myPage = New dt.Page(bm.UrlHelper.HOMEPAGE_ID)
            Else
                Try
                    myPage = New dt.Page(bm.UrlHelper.GetPageId(Request("id")))
                Catch ex As Exception
                    Response.Redirect(bm.ConfigurationHelper.Page_NotFound)
                End Try
            End If
            ucLayoutHelper1.LoadContent(PageRecord, htmlTitle, IsDraft)
            ucLayoutHelper2.LoadContent(myPage.Id)
            If myPage.RedirectLink <> "" Then
                Response.Redirect(myPage.RedirectLink)
            End If
            If myPage.DisplayPrintable Then
                BuildPrintable(myPage.Id)
            End If
            If myPage.DisplayPublished Then
                ltrRevised.Text = "Revised: " + myPage.Published.ToShortDateString
            End If
            If myPage.DisplayBookmarking Then
                BuildBookmarks(myPage)
            End If
            BuildTags(myPage.Id)
        End If
        MyBase.OnLoad(e)
    End Sub

    Private Sub BuildTags(ByVal mypageid As Integer)
        Dim sb As New StringBuilder
        Dim dtTags As DataTable = New dt.TagData().GetTagsByPage(mypageid)
        If (dtTags.Rows.Count > 0) Then
            sb.Append("<p style='padding-top:5px;font-size:10px;border-top:1px solid #e4e4e4;'>Categories: ")
            For Each dr As DataRow In dtTags.Rows
                sb.AppendFormat("<a href='{0}{1}&catId={2}'>{3}</a>, ",
                  bm.UrlHelper.BuildCustomPagePartialUrl("categories.aspx"), "46", dr(0), dr(1))
            Next
            sb.Remove(sb.Length - 2, 2)
            sb.Append("</p>")
        End If
        ltrTags.Text = sb.ToString
    End Sub

    Private Sub BuildPrintable(ByVal myPageId As Integer)
        Dim sb As New StringBuilder
        sb.AppendFormat("<a href='#' style='text-decoration:none;color:#444444;' onclick=""open('printable.aspx?id={0}', '', 'menubar=1, toolbar=1, width=800, height=600, resizable, scrollbars');"">",
          myPage.Id.ToString)
        sb.Append("<img src='_system/images/layout/print.gif' border='0'>Printable version</a>")
        ltrPrintable.Text = sb.ToString
    End Sub

    Private Sub BuildBookmarks(ByVal myPage As dt.Page)
        Dim absoluteUrl As String = bm.ConfigurationHelper.AbsoluteUrlRoot + myPage.Url
        Dim sb As New StringBuilder
        sb.Append("<div class='page-bookmarks'>Bookmark:")
        sb.Append("<table align='center' cellpadding='5' style='font-size:10px;'><tr><td>")

        sb.AppendFormat("<a style='text-decoration:none;' title='Post this story to Delicious' href='http://del.icio.us/post?url={0}&amp;title={1}'>",
          absoluteUrl, myPage.ContentTitle)
        sb.Append("<img src='_system/images/layout/delicious.gif' border='0' style='margin-right:5px;'>Delicious</a></td><td>")

        sb.AppendFormat("<a style='text-decoration:none;' title='Post this story to Digg' href='http://digg.com/submit?url={0}&amp;title={1}'>",
          absoluteUrl, myPage.ContentTitle)
        sb.Append("<img src='_system/images/layout/digg.gif' border='0' style='margin-right:5px;'>Digg</a></td><td>")

        sb.AppendFormat("<a style='text-decoration:none;' title='Post this story to Facebook' href='http://www.facebook.com/sharer.php?u={0}'>",
          absoluteUrl)
        sb.Append("<img src='_system/images/layout/facebook.gif' border='0' style='margin-right:5px;'>Facebook</a></td><td>")

        sb.AppendFormat("<a style='text-decoration:none;' title='Post this story to reddit' href='http://reddit.com/submit?url={0}&amp;title={1}'>",
          absoluteUrl, myPage.ContentTitle)
        sb.Append("<img src='_system/images/layout/reddit.gif' border='0' style='margin-right:5px;'>reddit</a></td><td></table></div>")

        ltrBookmarks.Text = sb.ToString
    End Sub

    Protected Sub Page_Error(ByVal sender As Object, ByVal e As EventArgs)
        bm.ErrorHandler.HandleError(HttpContext.Current.Server.GetLastError())
    End Sub

    Protected MustOverride Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

End Class
