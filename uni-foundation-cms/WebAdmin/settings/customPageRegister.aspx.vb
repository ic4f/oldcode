Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Namespace settings

    Public Class customPageRegister
        Inherits ba.AddBasePage

        Protected tbxPageTitle As TextBox
        Protected valUniqueTitle As CustomValidator
        Protected tbxContentTitle As TextBox
        Protected tbxFileName As TextBox
        Protected tbxBody As System.Web.UI.HtmlControls.HtmlTextArea
        Protected tbxRedirect As TextBox
        Protected cbxDisplayContent As CheckBox
        Protected cbxCanEditBody As CheckBox
        Protected cbxCanChangeMenu As CheckBox
        Protected tbxComment As TextBox
        Protected hidMenu As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected hidModules As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected hidTags As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected hidLabels As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected ltrParentError As Literal
        Protected cbxDisplayPublishedDate As CheckBox
        Protected cbxDisplayPrintable As CheckBox
        Protected cbxDisplayBookmarking As CheckBox

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            btnSave.Attributes.Add("OnClick", "getmenu();getmodules();gettags();getlabels();")
        End Sub

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "custom page"
            End Get
        End Property

        Protected Overrides ReadOnly Property RedirectPage() As String
            Get
                Return "custompagelist.aspx"
            End Get
        End Property

        Protected Overrides Sub ResetFormFields()
            tbxPageTitle.Text = ""
            tbxContentTitle.Text = ""
            tbxFileName.Text = ""
            tbxBody.Value = ""
            tbxRedirect.Text = ""
            cbxDisplayContent.Checked = False
            cbxCanEditBody.Checked = False
            cbxCanEditBody.Checked = False
            tbxComment.Text = ""
            hidMenu.Value = ""
            hidModules.Value = ""
            hidTags.Value = ""
            hidLabels.Value = ""
            cbxDisplayPublishedDate.Checked = False
            cbxDisplayPrintable.Checked = False
            cbxDisplayBookmarking.Checked = False
        End Sub

        Protected Overrides Function SaveItem() As Integer
            If Page.IsValid Then

                Dim menuid As Integer = Convert.ToInt32(hidMenu.Value)
                If menuid = -1 Then
                    ltrParentError.Visible = True
                    Return 0
                Else
                    ltrParentError.Visible = False
                End If

                Dim canDelete As Boolean = False
                Dim canEditBody As Boolean = cbxCanEditBody.Checked
                Dim canEditAdminComment As Boolean = False
                Dim canChangeMenu As Boolean = cbxCanChangeMenu.Checked
                Dim redirect As String = tbxRedirect.Text.Trim
                Dim pageTitle As String = tbxPageTitle.Text.Trim
                Dim contentTitle As String = tbxContentTitle.Text.Trim
                Dim displayContent As Boolean = cbxDisplayContent.Checked
                Dim displayDate As Boolean = cbxDisplayPublishedDate.Checked
                Dim displayBookmarking As Boolean = cbxDisplayBookmarking.Checked
                Dim displayPrintable As Boolean = cbxDisplayPrintable.Checked
                Dim displayDiscussion As Boolean = False

                Dim cleaner As New ba.HtmlCleaner(tbxBody.Value.Trim())
                Dim body As String = cleaner.CleanHtml()

                Dim fileName As String = tbxFileName.Text.Trim
                If Not fileName.EndsWith(".aspx") Then
                    fileName += ".aspx"
                End If
                Dim partialUrl As String = ba.UrlHelper.BuildCustomPagePartialUrl(fileName)

                Dim admincomment As String = tbxComment.Text.Trim

                Dim newPageId As Integer = New dt.PageData().Create(
                  menuid, ba.PageCategoryCode.StandardPage, canDelete, canEditBody, canEditAdminComment, canChangeMenu,
                  redirect, pageTitle, contentTitle, body, displayContent, displayDate, displayBookmarking, displayPrintable,
                  False, False, partialUrl, admincomment, Identity.UserId)

                If newPageId > 0 Then
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

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 101
            End Get
        End Property

    End Class

End Namespace