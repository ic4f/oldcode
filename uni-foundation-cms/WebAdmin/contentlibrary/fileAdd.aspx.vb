Imports System.IO
Imports System.Text
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data
Imports c = Core

Namespace contentlibrary

    Public Class fileAdd
        Inherits ba.AddBasePage

        Protected tbxFile As HtmlInputFile
        Protected tbxDescription As TextBox
        Protected tbxComment As TextBox
        Protected WithEvents valUnique As CustomValidator
        Protected hidCats As HtmlControls.HtmlInputHidden

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            btnSave.Attributes.Add("OnClick", "getlabels();")
        End Sub

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "File"
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
                Return "fileList.aspx"
            End Get
        End Property

        Protected Overrides Function SaveItem() As Integer

            If Page.IsValid Then

                Dim postedFile As HttpPostedFile = tbxFile.PostedFile
                Dim fInfo As FileInfo = New FileInfo(postedFile.FileName)

                Dim extension As String = fInfo.Extension
                Dim description As String = tbxDescription.Text.Trim()
                Dim comment As String = tbxComment.Text.Trim()

                If (ba.ImageHelper.IsImage(extension)) Then
                    ltrErrorMessage.Text = "Wrong file format: please select a non-image file"
                    Return 0
                End If

                Dim fd As New dt.FileData
                Dim fileId As Integer = fd.Create(extension, description, comment, Identity.UserId)

                If (fileId <= 0) Then
                    valUnique.IsValid = False
                    Return 0
                End If

                Dim path As String = Server.MapPath(ba.UrlHelper.GetFileUrl(fileId, extension))
                postedFile.SaveAs(path)

                Dim selected As String = hidCats.Value

                Dim labels As DataTable = New dt.ContentLabelData().GetList

                Dim dtfl As New dt.FileContentLabelData
                Dim cats As String() = selected.Split()
                Dim labelId As Integer
                For i As Integer = 0 To cats.Length - 1
                    If (cats(i) = "true") Then
                        labelId = Convert.ToInt32(labels.Rows(i)(0))
                        dtfl.AddLink(fileId, labelId)
                    End If
                Next

                Dim fInfo2 As New FileInfo(path)
                Dim myfile As New dt.File(fileId)
                myfile.Size = fInfo2.Length
                myfile.Update()

                Return 1

            Else
                Return 0
            End If

        End Function

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 47
            End Get
        End Property

    End Class

End Namespace