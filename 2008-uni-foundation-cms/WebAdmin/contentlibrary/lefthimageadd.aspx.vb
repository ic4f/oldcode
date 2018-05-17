Imports System.IO
Imports System.Text
Imports System.Drawing.Imaging
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data
Imports c = Core

Namespace contentlibrary

    Public Class lefthimageadd
        Inherits ba.AddBasePage

        Protected tbxFile As HtmlInputFile
        Protected tbxText As TextBox
        Protected tbxDescription As TextBox
        Protected valUnique As CustomValidator

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        End Sub

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "Left Header Image"
            End Get
        End Property

        Protected Overrides ReadOnly Property Message() As String
            Get
                Return "uploaded"
            End Get
        End Property

        Protected Overrides Sub ResetFormFields()
        End Sub

        Protected Overrides ReadOnly Property RedirectPage() As String
            Get
                Return "lefthimageList.aspx"
            End Get
        End Property

        Protected Overrides Function SaveItem() As Integer

            If Page.IsValid Then

                Dim fInfo As FileInfo = New FileInfo(tbxFile.PostedFile.FileName)
                If (fInfo.Extension <> ".jpg" And fInfo.Extension <> ".jpeg") Then
                    ltrErrorMessage.Text = "Wrong file format"
                    Return 0
                End If

                Dim imageId As Integer = New dt.HeaderImageData().Create(ba.HeaderImageLocationCode.Left, tbxDescription.Text.Trim, Identity.UserId)

                If (imageId <= 0) Then
                    valUnique.IsValid = False
                    Return 0
                End If

                Dim width As Integer = ba.ConfigurationHelper.HeaderImageLeftWidth
                Dim photoHeight As Integer = ba.ConfigurationHelper.HeaderImageLeftPhotoHeight

                Dim btm As New Bitmap(tbxFile.PostedFile.InputStream)
                If (btm.Width <> width) Then
                    ltrErrorMessage.Text = "The image file has wrong dimensions: width = " + btm.Width.ToString
                    Return 0
                ElseIf (btm.Height <> photoHeight) Then
                    ltrErrorMessage.Text = "The image file has wrong dimensions: height = " + btm.Height.ToString
                    Return 0
                End If

                Dim filePath As String = Server.MapPath(ba.UrlHelper.GetHeaderImageUrlLeft(imageId))
                Dim ih As New ba.ImageHelper
                ih.MakeLeftHeader(tbxFile.PostedFile.InputStream, filePath, tbxText.Text.Trim)

                Return 1
            Else
                Return 0
            End If

        End Function

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 111
            End Get
        End Property

    End Class

End Namespace