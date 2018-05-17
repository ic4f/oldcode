Imports System.Text
Imports System.IO
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Namespace contentlibrary

    Public Class moduleEdit
        Inherits ba.EditBasePage

        Protected tbxAdminTitle As TextBox
        Protected tbxFile As HtmlInputFile
        Protected cbxRemove As CheckBox
        Protected valFileType As CustomValidator
        Protected tbxTitle As TextBox
        Protected tbxBody As TextBox
        Protected tbxLink As TextBox
        Protected hidPage As HtmlControls.HtmlInputHidden
        Protected hidLabels As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected radStatus As RadioButtonList
        Protected valUniqueTitle As CustomValidator
        Protected ltrLinkError As Literal
        Protected ltrContentError As Literal
        Protected ltrTypes As Literal
        Protected ltrWidth As Literal
        Protected ltrHeight As Literal
        Protected ltrFrame As Literal
        Protected ltrLabelsFrame As Literal

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            btnSave.Attributes.Add("OnClick", "getPage();getLabels();")

            Dim moduleId As Integer = GetId()
            Dim m As New dt.Module(moduleId)

            ltrFrame.Text = "<iframe id=""iframePage"" name=""iframePage"" src=""../_system/datalists/publishedPages.aspx?PageId=" + m.PageId.ToString + """ class=""sidebaroptionframe""  style=""width:100%; height:730px;""></iframe>"
            ltrLabelsFrame.Text = "<iframe id=""iframeLabels"" name=""iframeLabels"" src=""../_system/datalists/selectedModuleLabels.aspx?Id=" + moduleId.ToString() + """ class=""sidebaroptionframe"" style=""height:115px;""></iframe>"
            ltrTypes.Text = "You may select the following file types: " + getImageFileTypes()
            ltrWidth.Text = ba.ConfigurationHelper.ModuleWidth.ToString
            ltrHeight.Text = ba.ConfigurationHelper.MaxModuleHeight.ToString
            cbxRemove.Checked = False

            tbxAdminTitle.Text = m.AdminTitle
            tbxTitle.Text = m.Title
            tbxBody.Text = m.Body
            tbxLink.Text = m.ExternalLink

            radStatus.Items.Clear()
            radStatus.Items.Add(New ListItem("displayed on all pages", "0"))
            radStatus.Items.Add(New ListItem("assinged to each page individually", "1"))
            radStatus.Items.Add(New ListItem("archived", "2"))

            If m.IsRequired Then
                radStatus.SelectedIndex = 0
            ElseIf m.IsArchived Then
                radStatus.SelectedIndex = 2
            Else
                radStatus.SelectedIndex = 1
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

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "module"
            End Get
        End Property

        Protected Overrides ReadOnly Property RedirectPage() As String
            Get
                Return "moduleList.aspx"
            End Get
        End Property

        Protected Overrides Sub ResetFormFields()
            BindData()
            cbxRemove.Checked = False
        End Sub

        Protected Overrides Function SaveItem() As Integer
            If Page.IsValid Then

                Dim helper As New ba.WebHelper
                Dim admintitle As String = helper.FilterUserInput(tbxAdminTitle.Text, True)
                Dim title As String = helper.FilterUserInput(tbxTitle.Text, True)
                Dim body As String = helper.FilterUserInput(tbxBody.Text, True)
                Dim link As String = helper.FilterUserInput(tbxLink.Text, True)
                Dim isrequired As Boolean = (radStatus.SelectedIndex = 0)
                Dim isarchived As Boolean = (radStatus.SelectedIndex = 2)

                Dim m As New dt.Module(GetId())

                If (m.ImageExtension = "" And tbxFile.PostedFile.ContentLength = 0 And title = "" And body = "") _
                Or (m.ImageExtension <> "" And cbxRemove.Checked And tbxFile.PostedFile.ContentLength = 0 And title = "" And body = "") Then
                    ltrContentError.Visible = True
                    Return 0
                End If

                If tbxFile.PostedFile.ContentLength > 0 Then
                    Dim fInfo As FileInfo = New FileInfo(tbxFile.PostedFile.FileName)
                    If (Not ba.ImageHelper.IsImage(fInfo.Extension)) Then
                        valFileType.IsValid = False
                        Return 0
                    End If
                End If

                Dim pageId As Integer = Convert.ToInt32(hidPage.Value)
                If (pageId = -1 And link = "") Or (pageId > 0 And link <> "") Then
                    ltrLinkError.Visible = True
                    Return 0
                Else
                    ltrLinkError.Visible = False
                End If

                m.AdminTitle = admintitle
                m.Title = title
                m.Body = body
                m.IsRequired = isrequired
                m.IsArchived = isarchived
                m.ModifiedBy = Identity.UserId

                If link = "" Then
                    m.ExternalLink = ""
                    m.PageId = pageId
                Else
                    m.ExternalLink = link
                    m.PageId = -1
                End If

                If cbxRemove.Checked Then
                    m.ImageExtension = ""
                End If

                If tbxFile.PostedFile.ContentLength > 0 Then
                    Dim fInfo As FileInfo = New FileInfo(tbxFile.PostedFile.FileName)
                    m.ImageExtension = fInfo.Extension
                End If

                If isarchived Then
                    Dim dtpm As New dt.PageModuleData
                    dtpm.DeleteByModule(GetId())
                End If

                Dim result As Integer = m.Update()

                If result > 0 Then
                    updateModuleLabels(m.Id)
                    Dim path As String = Server.MapPath(ba.UrlHelper.GetModuleImageUrl(m.Id, m.ImageExtension))
                    If cbxRemove.Checked Then
                        File.Delete(path)
                    End If
                    If tbxFile.PostedFile.ContentLength > 0 Then
                        File.Delete(path)
                        Dim fInfo As FileInfo = New FileInfo(tbxFile.PostedFile.FileName)
                        Dim ih As New ba.ImageHelper
                        ih.MakeImage(tbxFile.PostedFile.InputStream, path, ba.ConfigurationHelper.ModuleWidth, ba.ConfigurationHelper.MaxModuleHeight)
                    End If
                    Return 1
                Else
                    valUniqueTitle.IsValid = False
                    Return 0
                End If

            Else
                Return 0
            End If
        End Function

        Private Sub updateModuleLabels(ByVal moduleId As Integer)
            Dim mld As New dt.ModuleContentLabelData
            mld.DeleteByModule(moduleId)
            Dim dtLabels As DataTable = New dt.ContentLabelData().GetList
            Dim labels As String() = hidLabels.Value.Split()
            For i As Integer = 0 To labels.Length - 1
                If (labels(i) = "true") Then
                    mld.AddLink(moduleId, Convert.ToInt32(dtLabels.Rows(i)(0)))
                End If
            Next
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 45
            End Get
        End Property

    End Class

End Namespace