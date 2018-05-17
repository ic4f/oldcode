Imports System.IO
Imports System.Text
Imports System.Drawing.Imaging
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data
Imports c = Core

Namespace contentlibrary

    Public Class himageAdd
        Inherits ba.AddBasePage

        Protected tbxFile As HtmlInputFile

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        End Sub

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "Right Header Image"
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
                Return "himageList.aspx"
            End Get
        End Property

        Protected Overrides Function SaveItem() As Integer

            If Page.IsValid Then

                Dim fInfo As FileInfo = New FileInfo(tbxFile.PostedFile.FileName)
                If (fInfo.Extension <> ".jpg" And fInfo.Extension <> ".jpeg") Then
                    ltrErrorMessage.Text = "Wrong file format"
                    Return 0
                End If

                Dim btm As New Bitmap(tbxFile.PostedFile.InputStream)
                If (btm.Width <> 520) Then
                    ltrErrorMessage.Text = "The image file has wrong dimensions: width = " + btm.Width.ToString
                    Return 0
                ElseIf (btm.Height <> 130) Then
                    ltrErrorMessage.Text = "The image file has wrong dimensions: height = " + btm.Height.ToString
                    Return 0
                End If

                Dim imageId As Integer = New dt.HeaderImageData().Create(ba.HeaderImageLocationCode.Right, "", Identity.UserId)

                Dim filePath As String = Server.MapPath(ba.UrlHelper.GetHeaderImageUrlRight(imageId))
                tbxFile.PostedFile.SaveAs(filePath)

                Return 1
            Else
                Return 0
            End If

        End Function

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 36
            End Get
        End Property

    End Class

End Namespace