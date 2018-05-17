Imports System.Text
Imports System.IO
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Namespace webpages

    Public Class dstoryEdit
        Inherits ba.PublishBasePage

        Protected tbxFile As HtmlInputFile
        Protected tbxPageTitle As TextBox
        Protected valUniqueTitleError As CustomValidator
        Protected tbxContentTitle As TextBox
        Protected tbxNames As TextBox
        Protected tbxSummary As TextBox
        Protected tbxBody As System.Web.UI.HtmlControls.HtmlTextArea
        Protected tbxRedirect As TextBox
        Protected cbxDisplayPublishedDate As CheckBox
        Protected cbxDisplayPrintable As CheckBox
        Protected cbxDisplayBookmarking As CheckBox
        Protected tbxComment As TextBox
        Protected hidModules As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected hidTags As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected hidLabels As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected ltrModulesFrame As Literal
        Protected ltrTagsFrame As Literal
        Protected ltrLabelsFrame As Literal
        Protected ltrTypes As Literal
        Protected ltrWidth As Literal
        Protected ltrHeight As Literal
        Protected cbxFeatured As CheckBox
        Protected cbxRemove As CheckBox
        Protected ltrImage As Literal

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            CheckPageCategoryMenu()
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Function getImageFileTypes() As String
            Dim sb As New StringBuilder
            Dim dt As DataTable = New dt.FileTypeData().GetImageTypeList()
            For Each dr As DataRow In dt.Rows
                sb.AppendFormat("{0} ", dr(0))
            Next
            Return sb.ToString
        End Function

        Private Sub BindData()
            btnSave.Attributes.Add("OnClick", "getmodules();gettags();getlabels();")
            btnPublish.Attributes.Add("OnClick", "getmodules();gettags();getlabels();")
            ltrTypes.Text = "You may select the following file types: " + getImageFileTypes()
            ltrWidth.Text = ba.ConfigurationHelper.NewsImageWidth.ToString()
            ltrHeight.Text = ba.ConfigurationHelper.NewsImageHeight.ToString

            Dim pageId As Integer = GetId()
            Dim mypage As New dt.Page(pageId)
            Dim mydstory As New dt.DStoryPage(New dt.DStoryPageData().GetIdByPageId(pageId))

            tbxPageTitle.Text = mypage.PageTitle
            tbxContentTitle.Text = mypage.ContentTitle
            tbxBody.Value = mypage.BodyDraft
            tbxRedirect.Text = mypage.RedirectLink
            cbxDisplayPublishedDate.Checked = mypage.DisplayPublished
            cbxDisplayPrintable.Checked = mypage.DisplayPrintable
            cbxDisplayBookmarking.Checked = mypage.DisplayBookmarking
            tbxComment.Text = mypage.AdminComment

            tbxSummary.Text = mydstory.Summary
            tbxNames.Text = mydstory.DonorDisplayedName
            cbxFeatured.Checked = mydstory.IsFeatured

            If mydstory.HasImage Then
                ltrImage.Text = "<br><br><img src='" + ba.UrlHelper.GetDStoryImageUrl(mydstory.Id) + "' border='1'>"
            End If

            ltrModulesFrame.Text = "<iframe id=""iframeModules"" name=""iframeModules"" src=""../_system/datalists/selectedPageModules.aspx?Id=" + pageId.ToString() + """ class=""sidebaroptionframe""></iframe>"
            ltrTagsFrame.Text = "<iframe id=""iframeTags"" name=""iframeTags"" src=""../_system/datalists/selectedPageTags.aspx?Id=" + pageId.ToString() + """ class=""sidebaroptionframe""></iframe>"
            ltrLabelsFrame.Text = "<iframe id=""iframeLabels"" name=""iframeLabels"" src=""../_system/datalists/selectedPageLabels.aspx?Id=" + pageId.ToString() + """ class=""sidebaroptionframe""></iframe>"
        End Sub

        Protected Overrides Function SaveItem() As Integer
            If Page.IsValid Then

                Dim pageId As Integer = GetId()
                Dim mypage As New dt.Page(pageId)

                Dim pageTitle As String = tbxPageTitle.Text.Trim
                Dim contentTitle As String = tbxContentTitle.Text.Trim
                Dim redirect As String = tbxRedirect.Text.Trim
                Dim displayDate As Boolean = cbxDisplayPublishedDate.Checked
                Dim displayBookmarking As Boolean = cbxDisplayBookmarking.Checked
                Dim displayPrintable As Boolean = cbxDisplayPrintable.Checked
                Dim admincomment As String = tbxComment.Text.Trim

                mypage.PageTitle = pageTitle
                mypage.ContentTitle = contentTitle
                mypage.RedirectLink = redirect
                mypage.DisplayPublished = displayDate
                mypage.DisplayBookmarking = displayBookmarking
                mypage.DisplayPrintable = displayPrintable
                mypage.AdminComment = admincomment
                mypage.ModifiedBy = Identity.UserId

                If mypage.CanEditBody Then
                    Dim cleaner As New ba.HtmlCleaner(tbxBody.Value.Trim())
                    mypage.BodyDraft = cleaner.CleanHtml()
                End If

                Dim result As Integer = mypage.Update()
                If result > 0 Then
                    Dim mydstory As New dt.DStoryPage(New dt.DStoryPageData().GetIdByPageId(pageId))
                    mydstory.IsFeatured = cbxFeatured.Checked
                    mydstory.DonorDisplayedName = tbxNames.Text.Trim
                    mydstory.Summary = tbxSummary.Text.Trim

                    Dim path As String = Server.MapPath(ba.UrlHelper.GetDStoryImageUrl(mydstory.Id))
                    If tbxFile.PostedFile.ContentLength > 0 Then
                        mydstory.HasImage = True
                        ProcessImage(mydstory.Id)
                    ElseIf cbxRemove.Checked Then
                        mydstory.HasImage = False
                        File.Delete(path)
                    End If

                    mydstory.Update()

                    updatePageModules(pageId)
                    updatePageTags(pageId)
                    updatePageLabels(pageId)
                    Dim la As New ba.ContextLinkAnalyzer
                    la.UpdateContextualLinks(pageId)
                    Return 1
                Else
                    valUniqueTitleError.IsValid = False
                    Return 0
                End If
            Else
                Return 0
            End If
        End Function

        Private Sub ProcessImage(ByVal newsId As Integer)
            Dim ih As New ba.ImageHelper
            ih.MakeImage(
             tbxFile.PostedFile.InputStream, Server.MapPath(ba.UrlHelper.GetDStoryImageUrl(newsId)),
             ba.ConfigurationHelper.DStoryImageWidth,
             ba.ConfigurationHelper.DStoryImageHeight)
        End Sub

        Private Sub updatePageModules(ByVal newPageId As Integer)
            Dim dtpm As New dt.PageModuleData
            dtpm.DeleteByPage(newPageId)
            Dim dtModules As DataTable = New dt.ModuleData().GetModulesForPageAssingment
            Dim modules As String() = hidModules.Value.Split()
            For i As Integer = 0 To modules.Length - 1
                If (modules(i) = "true") Then
                    dtpm.AddLink(newPageId, Convert.ToInt32(dtModules.Rows(i)(0)))
                End If
            Next
        End Sub

        Private Sub updatePageTags(ByVal newPageId As Integer)
            Dim dtpt As New dt.PageTagData
            dtpt.DeleteByPage(newPageId)
            Dim dtTags As DataTable = New dt.TagData().GetList
            Dim tags As String() = hidTags.Value.Split()
            For i As Integer = 0 To tags.Length - 1
                If (tags(i) = "true") Then
                    dtpt.AddLink(newPageId, Convert.ToInt32(dtTags.Rows(i)(0)))
                End If
            Next
        End Sub

        Private Sub updatePageLabels(ByVal newPageId As Integer)
            Dim dtpl As New dt.PageContentLabelData
            dtpl.DeleteByPage(newPageId)
            Dim dtLabels As DataTable = New dt.ContentLabelData().GetList
            Dim labels As String() = hidLabels.Value.Split()
            For i As Integer = 0 To labels.Length - 1
                If (labels(i) = "true") Then
                    dtpl.AddLink(newPageId, Convert.ToInt32(dtLabels.Rows(i)(0)))
                End If
            Next
        End Sub

        Protected Overrides Sub ResetFormFields()
            BindData()
            cbxRemove.Checked = False
        End Sub

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "donor story page"
            End Get
        End Property

        Protected Overrides ReadOnly Property RedirectPage() As String
            Get
                Return "dstoryList.aspx"
            End Get
        End Property

        Private Sub CheckPageCategoryMenu()
            If Not New dt.PageCategoryData().HasMenu(ba.PageCategoryCode.DonorStory) Then
                Response.Redirect("dstories.aspx")
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 20
            End Get
        End Property

    End Class

End Namespace