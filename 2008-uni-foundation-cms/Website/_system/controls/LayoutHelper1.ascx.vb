Imports System.IO
Imports dt = Foundation.Data
Imports bm = Foundation.BusinessMain

Public Class LayoutHelper1
    Inherits System.Web.UI.UserControl

    Protected ltrHeaderImageLeft As Literal
    Protected ltrHeaderImageRight As Literal
    Protected ltrBody As Literal
    Protected ltrBodyTitle As Literal
    Protected ltrMainMenu As Literal
    Protected ltrContextMenu As Literal
    Protected tbxSearch As TextBox
    Protected WithEvents btnSearch As Button
    Protected ltrQuote As Literal
    Protected ltrQuoteSig As Literal

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Friend Sub LoadContent()
        makeHeaderLeftImage()
        makeHeaderRightImage()
        makeQuote()
        Dim dtMenu As DataTable = New dt.MenuData().GetOrdered
        makeMainMenu(-1, dtMenu)
    End Sub

    Friend Sub LoadContent(ByVal myPage As dt.Page, ByVal htmlTitle As HtmlGenericControl, ByVal isDraft As Boolean)
        makeHeaderLeftImage(myPage.MenuId)
        makeHeaderRightImage()
        makeQuote()
        makeMenus(myPage)
        htmlTitle.InnerText = myPage.PageTitle
        If myPage.DisplayContent Then
            makePage(myPage, isDraft)
        End If
    End Sub

    Private Sub makeMenus(ByVal myPage As dt.Page)
        Dim dtMenu As DataTable = New dt.MenuData().GetOrdered
        Try
            makeMainMenu(myPage.MenuId, dtMenu)
            If Not myPage.Id = bm.UrlHelper.HOMEPAGE_ID Then
                makeContextMenu(myPage.ContentTitle, myPage.MenuId, dtMenu)
            End If
        Catch ex As Exception
            makeMainMenu(-1, dtMenu)
        End Try
    End Sub

    Private Sub makePage(ByVal myPage As dt.Page, ByVal isDraft As Boolean)
        ltrBodyTitle.Text = myPage.ContentTitle
        If isDraft Then
            ltrBody.Text = "<p>" + Server.HtmlDecode(myPage.BodyDraft)
        Else
            ltrBody.Text = "<p>" + Server.HtmlDecode(myPage.Body)
        End If
    End Sub

    Private Sub makeHeaderLeftImage()
        Dim imagePath As String = bm.UrlHelper.GetDefaultHeaderImageUrlLeft()
        ltrHeaderImageLeft.Text = "<img src='" + imagePath + "'>"
    End Sub

    Private Sub makeHeaderLeftImage(ByVal menuId As Integer)

        Dim himageid As Integer = New dt.MenuData().GetHeaderImageId(menuId)
        Dim imagePath As String
        If himageid > 0 Then
            imagePath = bm.UrlHelper.GetHeaderImageUrlLeft(himageid)
            If Not File.Exists(Server.MapPath(imagePath)) Then
                imagePath = bm.UrlHelper.GetDefaultHeaderImageUrlLeft()
            End If
        Else
            imagePath = bm.UrlHelper.GetDefaultHeaderImageUrlLeft()
        End If
        ltrHeaderImageLeft.Text = "<img src='" + imagePath + "'>"
    End Sub

    Private Sub makeHeaderRightImage()
        Dim dt As DataTable = New dt.HeaderImageData().GetImagesForDisplay(bm.HeaderImageLocationCode.Right)
        Dim rnd As New Random
        Dim index As Integer = rnd.Next(dt.Rows.Count)

        Dim imagePath As String
        If dt.Rows.Count > 0 Then
            imagePath = bm.UrlHelper.GetHeaderImageUrlRight(dt.Rows(index)(0).ToString())
            If Not File.Exists(Server.MapPath(imagePath)) Then
                imagePath = bm.UrlHelper.GetDefaultHeaderImageUrlRight()
            End If
        Else
            imagePath = bm.UrlHelper.GetDefaultHeaderImageUrlRight()
        End If
        ltrHeaderImageRight.Text = "<img src='" + imagePath + "'>"
    End Sub

    Private Sub makeQuote()
        Dim dt As DataTable = New dt.QuoteData().GetQuotesForDisplay()
        If dt.Rows.Count > 0 Then
            Dim rnd As New Random
            Dim index As Integer = rnd.Next(dt.Rows.Count)
            ltrQuote.Text = """" + dt.Rows(index)(0).ToString() + """"
            ltrQuoteSig.Text = dt.Rows(index)(1).ToString()
        End If
    End Sub

    Private Sub makeMainMenu(ByVal menuId As Integer, ByVal dtMenu As DataTable)
        Dim pmt As New bm.PublicMenuTree(bm.PublicMenuTree.ConvertData(dtMenu), menuId, "")
        ltrMainMenu.Text = ltrMainMenu.Text + pmt.GetMainMenuForDisplay()
    End Sub

    Private Sub makeContextMenu(ByVal pageTitle As String, ByVal menuId As Integer, ByVal dtMenu As DataTable)
        Dim mh As New bm.MenuHelper
        ltrContextMenu.Text = mh.GetContextMenuHtml(pageTitle, menuId, dtMenu)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim h As New bm.WebHelper
        Dim text As String = h.FilterUserInput(tbxSearch.Text.Trim, True)
        Response.Redirect("http://www.google.com/search?sa=Search&domains=www.uni-foundation.org&sitesearch=www.uni-foundation.org&q=" + text)
    End Sub

End Class
