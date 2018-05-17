Imports System.Text
Imports System.IO
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Namespace contentlibrary

    Public Class moduleAdd
        Inherits ba.AddBasePage

        Protected tbxAdminTitle As TextBox
        Protected tbxFile As HtmlInputFile
        Protected valFileType As CustomValidator
        Protected tbxTitle As TextBox
        Protected tbxBody As TextBox
        Protected tbxLink As TextBox
        Protected hidPage As HtmlControls.HtmlInputHidden
        Protected hidLabels As HtmlControls.HtmlInputHidden
        Protected radStatus As RadioButtonList
        Protected valUniqueTitle As CustomValidator
        Protected ltrLinkError As Literal
        Protected ltrContentError As Literal
        Protected ltrTypes As Literal
        Protected ltrWidth As Literal
        Protected ltrHeight As Literal

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            btnSave.Attributes.Add("OnClick", "getPage();getLabels();")
            ltrTypes.Text = "You may select the following file types: " + getImageFileTypes()
            ltrWidth.Text = ba.ConfigurationHelper.ModuleWidth.ToString
            ltrHeight.Text = ba.ConfigurationHelper.MaxModuleHeight.ToString

            radStatus.Items.Add(New ListItem("displayed on all pages", "0"))
            radStatus.Items.Add(New ListItem("assinged to each page individually", "1"))
            radStatus.Items.Add(New ListItem("archived", "2"))
            radStatus.SelectedIndex = 0
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
            tbxAdminTitle.Text = ""
            tbxTitle.Text = ""
            tbxBody.Text = ""
            tbxLink.Text = ""
            radStatus.SelectedIndex = 0
        End Sub

        Protected Overrides Function SaveItem() As Integer
            If Page.IsValid Then
                Dim helper As New ba.WebHelper
                Dim admintitle As String = helper.FilterUserInput(tbxAdminTitle.Text, True)
                Dim title As String = helper.FilterUserInput(tbxTitle.Text, True)
                Dim body As String = helper.FilterUserInput(tbxBody.Text, True)
                Dim link As String = helper.FilterUserInput(tbxLink.Text, True)
                Dim imageExtension As String = ""

                If tbxFile.PostedFile.ContentLength = 0 And title = "" And body = "" Then
                    ltrContentError.Visible = True
                    Return 0
                End If

                If tbxFile.PostedFile.ContentLength > 0 Then
                    Dim fInfo As FileInfo = New FileInfo(tbxFile.PostedFile.FileName)
                    imageExtension = fInfo.Extension
                    If (Not ba.ImageHelper.IsImage(imageExtension)) Then
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

                Dim isrequired As Boolean = (radStatus.SelectedIndex = 0)
                Dim isarchived As Boolean = (radStatus.SelectedIndex = 2)

                Dim moduleId As Integer = New dt.ModuleData().Create(
                  admintitle, imageExtension, title, body, link, pageId, isrequired, isarchived, Identity.UserId)

                If moduleId > 0 Then
                    updateModuleLabels(moduleId)
                    If tbxFile.PostedFile.ContentLength > 0 Then
                        ProcessImage(moduleId, imageExtension)
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

        Private Sub ProcessImage(ByVal moduleid As Integer, ByVal imageExtension As String)
            Dim ih As New ba.ImageHelper
            ih.MakeImage(
             tbxFile.PostedFile.InputStream, Server.MapPath(ba.UrlHelper.GetModuleImageUrl(moduleid, imageExtension)),
             ba.ConfigurationHelper.ModuleWidth,
             ba.ConfigurationHelper.MaxModuleHeight)
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 43
            End Get
        End Property

    End Class

End Namespace