Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Namespace settings

    Public Class customPageEdit
        Inherits ba.PublishBasePage

        Protected tbxPageTitle As TextBox
        Protected valUniqueTitleError As CustomValidator
        Protected tbxContentTitle As TextBox
        Protected tbxBody As System.Web.UI.HtmlControls.HtmlTextArea
        Protected tbxRedirect As TextBox
        Protected cbxDisplayContent As CheckBox
        Protected cbxCanEditBody As CheckBox
        Protected cbxCanChangeMenu As CheckBox
        Protected cbxDisplayPublishedDate As CheckBox
        Protected cbxDisplayPrintable As CheckBox
        Protected cbxDisplayBookmarking As CheckBox
        Protected tbxComment As TextBox
        Protected hidMenu As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected hidModules As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected hidTags As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected hidLabels As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected ltrParentError As Literal
        Protected ltrMenuFrame As Literal
        Protected ltrModulesFrame As Literal
        Protected ltrTagsFrame As Literal
        Protected ltrLabelsFrame As Literal

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            btnSave.Attributes.Add("OnClick", "getmenu();getmodules();gettags();getlabels();")
            btnPublish.Attributes.Add("OnClick", "getmenu();getmodules();gettags();getlabels();")

            Dim pageId As Integer = GetId()
            Dim mypage As New dt.Page(pageId)

            tbxPageTitle.Text = mypage.PageTitle
            tbxContentTitle.Text = mypage.ContentTitle
            tbxBody.Value = mypage.BodyDraft
            tbxRedirect.Text = mypage.RedirectLink
            cbxDisplayContent.Checked = mypage.DisplayContent
            cbxCanEditBody.Checked = mypage.CanEditBody
            cbxCanChangeMenu.Checked = mypage.CanChangeMenu
            cbxDisplayPublishedDate.Checked = mypage.DisplayPublished
            cbxDisplayPrintable.Checked = mypage.DisplayPrintable
            cbxDisplayBookmarking.Checked = mypage.DisplayBookmarking
            tbxComment.Text = mypage.AdminComment

            Dim dtMenu As New dt.StagingMenu(mypage.MenuId)
            If dtMenu.PageId = pageId Then
                ltrMenuFrame.Text = "<div style='border:1px #cacaca solid;background-color:#ffffff;padding:10px;'>" +
                  "You cannot change the menu because this page is set as the default page for the <b>" + dtMenu.Text + "</b> menu.</div>"
            Else
                ltrMenuFrame.Text = "<iframe id=""iframeMenu"" name=""iframeMenu"" src=""../_system/datalists/menu.aspx?ToEditId=" + mypage.MenuId.ToString() + """ class=""sidebaroptionframe""></iframe>"
            End If

            ltrModulesFrame.Text = "<iframe id=""iframeModules"" name=""iframeModules"" src=""../_system/datalists/selectedPageModules.aspx?Id=" + pageId.ToString() + """ class=""sidebaroptionframe""></iframe>"
            ltrTagsFrame.Text = "<iframe id=""iframeTags"" name=""iframeTags"" src=""../_system/datalists/selectedPageTags.aspx?Id=" + pageId.ToString() + """ class=""sidebaroptionframe""></iframe>"
            ltrLabelsFrame.Text = "<iframe id=""iframeLabels"" name=""iframeLabels"" src=""../_system/datalists/selectedPageLabels.aspx?Id=" + pageId.ToString() + """ class=""sidebaroptionframe""></iframe>"
        End Sub

        Protected Overrides Function SaveItem() As Integer
            If Page.IsValid Then

                Dim pageId As Integer = GetId()
                Dim mypage As New dt.Page(pageId)

                Dim dtMenu As New dt.StagingMenu(mypage.MenuId)
                If dtMenu.PageId <> pageId Then
                    Dim menuid As Integer = Convert.ToInt32(hidMenu.Value)
                    If menuid = -1 Then
                        ltrParentError.Visible = True
                        Return 0
                    Else
                        ltrParentError.Visible = False
                        mypage.MenuId = menuid
                    End If
                End If

                Dim canEditBody As Boolean = cbxCanEditBody.Checked
                Dim canChangeMenu As Boolean = cbxCanChangeMenu.Checked
                Dim redirect As String = tbxRedirect.Text.Trim
                Dim pageTitle As String = tbxPageTitle.Text.Trim
                Dim contentTitle As String = tbxContentTitle.Text.Trim
                Dim displayContent As Boolean = cbxDisplayContent.Checked
                Dim displayDate As Boolean = cbxDisplayPublishedDate.Checked
                Dim displayBookmarking As Boolean = cbxDisplayBookmarking.Checked
                Dim displayPrintable As Boolean = cbxDisplayPrintable.Checked

                Dim admincomment As String = tbxComment.Text.Trim

                mypage.CanEditBody = canEditBody
                mypage.CanChangeMenu = canChangeMenu
                mypage.RedirectLink = redirect
                mypage.PageTitle = pageTitle
                mypage.ContentTitle = contentTitle
                mypage.DisplayContent = displayContent
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
        End Sub

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "custom page"
            End Get
        End Property

        Protected Overrides ReadOnly Property RedirectPage() As String
            Get
                Return "customPageList.aspx"
            End Get
        End Property

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 103
            End Get
        End Property

    End Class

End Namespace