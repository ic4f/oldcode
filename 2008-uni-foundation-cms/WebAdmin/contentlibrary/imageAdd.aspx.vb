Imports System.Text
Imports System.IO
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data
Imports c = Core

Namespace contentlibrary

    Public Class imageAdd
        Inherits ba.AddBasePage

        Protected tbxFile As HtmlInputFile
        Protected tbxWidth As TextBox
        Protected tbxHeight As TextBox
        Protected tbxDescription As TextBox
        Protected tbxComment As TextBox
        Protected valUnique As CustomValidator
        Protected valFileType As CustomValidator
        Protected hidCats As HtmlControls.HtmlInputHidden
        Protected ltrPreview As Literal
        Protected ltrTypes As Literal

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            btnSave.Attributes.Add("OnClick", "getlabels();")
            ltrTypes.Text = "You may select the following file types: " + getImageFileTypes()
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
                Return "Image"
            End Get
        End Property

        Protected Overrides ReadOnly Property Message() As String
            Get
                Return "uploaded"
            End Get
        End Property

        Protected Overrides Sub ResetFormFields()
            tbxDescription.Text = ""
            tbxComment.Text = ""
        End Sub

        Protected Overrides ReadOnly Property RedirectPage() As String
            Get
                Return "imageList.aspx"
            End Get
        End Property

        Protected Overrides Function SaveItem() As Integer

            If Page.IsValid Then

                Dim postedFile As HttpPostedFile = tbxFile.PostedFile
                Dim fInfo As FileInfo = New FileInfo(postedFile.FileName)
                Dim extension As String = fInfo.Extension
                Dim description As String = tbxDescription.Text.Trim()
                Dim comment As String = tbxComment.Text.Trim()

                If (Not ba.ImageHelper.IsImage(extension)) Then
                    valFileType.IsValid = False
                    Return 0
                End If

                Dim fd As New dt.FileData
                Dim imageId As Integer = fd.Create(extension, description, comment, Identity.UserId)

                If (imageId <= 0) Then
                    valUnique.IsValid = False
                    Return 0
                End If

                Dim width As Integer = -1
                If (tbxWidth.Text.Trim <> "") Then
                    width = Convert.ToInt32(tbxWidth.Text.Trim)
                End If
                Dim height As Integer = -1
                If (tbxHeight.Text.Trim <> "") Then
                    height = Convert.ToInt32(tbxHeight.Text.Trim)
                End If

                Dim imagePath As String = Server.MapPath(ba.UrlHelper.GetImageUrl(imageId, extension))
                Dim thumbUrl As String = ba.UrlHelper.GetImageThumbnailUrl(imageId, extension)
                Dim thumbPath As String = Server.MapPath(thumbUrl)

                Dim ih As New ba.ImageHelper
                ih.MakeImage(postedFile.InputStream, thumbPath, ba.ConfigurationHelper.ThumbnailWidth, ba.ConfigurationHelper.ThumbnailHeight)
                Dim imdim As ba.ImageDimensions = ih.MakeImage(postedFile.InputStream, imagePath, width, height)

                Dim selected As String = hidCats.Value
                Dim labels As DataTable = New dt.ContentLabelData().GetList
                Dim dtfl As New dt.FileContentLabelData
                dtfl.DeleteByFile(imageId) 'this is a safeguard
                Dim cats As String() = selected.Split()
                Dim labelId As Integer
                For i As Integer = 0 To cats.Length - 1
                    If (cats(i) = "true") Then
                        labelId = Convert.ToInt32(labels.Rows(i)(0))
                        dtfl.AddLink(imageId, labelId)
                    End If
                Next

                Dim fInfo2 As New FileInfo(imagePath)
                Dim myfile As New dt.File(imageId)
                myfile.Size = fInfo2.Length
                myfile.ImageWidth = imdim.Width
                myfile.ImageHeight = imdim.Height
                myfile.Update()

                ltrPreview.Text = "<img border='1' src='" + thumbUrl + "'>"

                Return 1

            Else
                Return 0
            End If

        End Function

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 73
            End Get
        End Property

    End Class

End Namespace