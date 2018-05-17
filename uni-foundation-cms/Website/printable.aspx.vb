Imports bm = Foundation.BusinessMain
Imports dt = Foundation.Data

Public Class printable
    Inherits System.Web.UI.Page

    Protected ltrBodyTitle As Literal
    Protected ltrBody As Literal
    Protected ltrUrl As Literal

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Private Sub BindData()
        Dim myPage As dt.Page
        If Request("id") = "" Then
            myPage = New dt.Page(bm.UrlHelper.HOMEPAGE_ID)
        Else
            Try
                myPage = New dt.Page(Convert.ToInt32(Request("id")))
            Catch ex As Exception
                Response.Redirect(bm.ConfigurationHelper.Page_NotFound)
            End Try
        End If

        ltrBodyTitle.Text = myPage.ContentTitle
        ltrBody.Text = "<p>" + Server.HtmlDecode(myPage.Body)
        ltrUrl.Text = bm.ConfigurationHelper.AbsoluteUrlRoot + myPage.Url()
    End Sub

End Class
