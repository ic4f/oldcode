Imports System.Text
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data


Namespace webpages

    Public Class dstoryAdd
        Inherits ba.AddBasePage

        Protected tbxFile As HtmlInputFile
        Protected tbxPageTitle As TextBox
        Protected valUniqueTitle As CustomValidator
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
        Protected ltrTypes As Literal
        Protected ltrWidth As Literal
        Protected ltrHeight As Literal
        Protected cbxFeatured As CheckBox

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            CheckPageCategoryMenu()
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            btnSave.Attributes.Add("OnClick", "getmodules();gettags();getlabels();")
            ltrTypes.Text = "You may select the following file types: " + getImageFileTypes()
            ltrWidth.Text = ba.ConfigurationHelper.NewsImageWidth.ToString()
            ltrHeight.Text = ba.ConfigurationHelper.NewsImageHeight.ToString
        End Sub

        Private Function getImageFileTypes() As String
            Dim sb As New StringBuilder
            Dim dt As DataTable = New dt.FileTypeData().GetImageTypeList()
            For Each dr As DataRow In dt.Rows
                sb.AppendFormat("{0} ", dr(0))
            Next
            Return sb.ToString
        End Function

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "donor story page"
            End Get
        End Property

        Protected Overrides ReadOnly Property RedirectPage() As String
            Get
                Return "dstorylist.aspx"
            End Get
        End Property

        Protected Overrides Sub ResetFormFields()
            tbxPageTitle.Text = ""
            tbxContentTitle.Text = ""
            tbxNames.Text = ""
            tbxSummary.Text = ""
            tbxBody.Value = ""
            tbxRedirect.Text = ""
            cbxDisplayPublishedDate.Checked = False
            cbxDisplayPrintable.Checked = False
            cbxDisplayBookmarking.Checked = False
            tbxComment.Text = ""
            hidModules.Value = ""
            hidTags.Value = ""
            hidLabels.Value = ""
            cbxFeatured.Checked = False
        End Sub

        Protected Overrides Function SaveItem() As Integer
            If Page.IsValid Then

                Dim pageCat As Integer = ba.PageCategoryCode.DonorStory

                Dim dtCat As New dt.PageCategory(pageCat)
                Dim menuid As Integer = dtCat.MenuId

                Dim pageTitle As String = tbxPageTitle.Text.Trim
                Dim contentTitle As String = tbxContentTitle.Text.Trim
                Dim redirect As String = tbxRedirect.Text.Trim
                Dim displayDate As Boolean = cbxDisplayPublishedDate.Checked
                Dim displayBookmarking As Boolean = cbxDisplayBookmarking.Checked
                Dim displayPrintable As Boolean = cbxDisplayPrintable.Checked
                Dim admincomment As String = tbxComment.Text.Trim
                Dim summary As String = tbxSummary.Text.Trim
                Dim names As String = tbxNames.Text.Trim

                Dim cleaner As New ba.HtmlCleaner(tbxBody.Value.Trim())
                Dim body As String = cleaner.CleanHtml()

                Dim partialUrl As String = ba.UrlHelper.BuildPagePartialUrlByPageCategory(pageCat)

                Dim newPageId As Integer = New dt.PageData().Create(
                  menuid, pageCat, True, True, True, False, redirect, pageTitle, contentTitle, body,
                  True, displayDate, displayBookmarking, displayPrintable, False, False, partialUrl, admincomment, Identity.UserId)

                If newPageId > 0 Then

                    Dim hasImage As Boolean = False
                    If tbxFile.PostedFile.ContentLength > 0 Then
                        hasImage = True
                    End If
                    Dim dstoryId As Integer = New dt.DStoryPageData().Create(newPageId, hasImage, cbxFeatured.Checked, names, summary)
                    If tbxFile.PostedFile.ContentLength > 0 Then
                        ProcessImage(dstoryId)
                    End If

                    updatePageModules(newPageId)
                    updatePageTags(newPageId)
                    updatePageLabels(newPageId)
                    Dim la As New ba.ContextLinkAnalyzer
                    la.UpdateContextualLinks(newPageId)
                    Return 1
                Else
                    valUniqueTitle.IsValid = False
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

        Private Sub CheckPageCategoryMenu()
            If Not New dt.PageCategoryData().HasMenu(ba.PageCategoryCode.DonorStory) Then
                Response.Redirect("dstories.aspx")
            End If

        End Sub
        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 18
            End Get
        End Property

    End Class

End Namespace