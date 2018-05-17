Imports System.IO
Imports System.Text
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data
Imports c = Core

Namespace contentlibrary

    Public Class imageEdit
        Inherits ba.EditBasePage

        Protected tbxFile As HtmlInputFile
        Protected tbxWidth As TextBox
        Protected tbxHeight As TextBox
        Protected tbxDescription As TextBox
        Protected tbxComment As TextBox
        Protected hidCats As HtmlControls.HtmlInputHidden
        Protected WithEvents valUnique As CustomValidator
        Protected ltrFrame As Literal
        Protected ltrFile As Literal
        Protected ltrPreview As Literal
        Protected ltrPreview2 As Literal
        Protected ltrTypes As Literal
        Protected valFileType As CustomValidator

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
                ltrTypes.Text = "You may select the following file types: " + getImageFileTypes()
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
            btnSave.Attributes.Add("OnClick", "getlabels();")
            ltrFrame.Text = "<iframe id=""iframecat"" name=""iframecat"" src=""../_system/datalists/selectedFileLabels.aspx?FileId=" + GetId().ToString + """ class=""sidebaroptionframe"" style=""height:243px;""></iframe>"
            Dim f As New dt.File(GetId())
            tbxDescription.Text = f.Description
            tbxComment.Text = f.Comment
            Dim imagePath As String = ba.UrlHelper.GetImageUrl(f.Id, f.Extension)
            Dim thumbPath As String = ba.UrlHelper.GetImageThumbnailUrl(f.Id, f.Extension)
            Dim imageName As String = ba.UrlHelper.GetImageName(f.Id, f.Extension)

            ltrFile.Text = "<a href='" + imagePath + "' target='_blank'><img src='../_system/images/filetypes/" +
              f.Extension.Substring(1) + ".gif' border='0'>" + imageName + "</a>"
            ltrPreview.Text = "<a href='" + imagePath + "' target='_blank'><img src='" + thumbPath + "' align='right' style='border:1px #cacaca solid;'></a>"
        End Sub

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "image"
            End Get
        End Property

        Protected Overrides Function SaveItem() As Integer

            Dim result As Integer = -1

            If Page.IsValid Then

                Dim imageId As Integer = GetId()

                Dim df As New dt.File(imageId)
                df.Description = tbxDescription.Text.Trim
                df.Comment = tbxComment.Text.Trim
                result = df.Update()

                If (result < 0) Then
                    valUnique.IsValid = False
                Else
                    Dim dtfl As New dt.FileContentLabelData
                    dtfl.DeleteByFile(imageId)
                    Dim selected As String = hidCats.Value
                    Dim labels As DataTable = New dt.ContentLabelData().GetList
                    Dim cats As String() = selected.Split()
                    Dim labelId As Integer
                    For i As Integer = 0 To cats.Length - 1
                        If (cats(i) = "true") Then
                            labelId = Convert.ToInt32(labels.Rows(i)(0))
                            dtfl.AddLink(imageId, labelId)
                        End If
                    Next

                    If tbxFile.PostedFile.ContentLength > 0 Then
                        result = uploadNewImage(df)
                    End If

                End If

                Return result

            End If

        End Function

        Private Function uploadNewImage(ByVal df As dt.File) As Integer

            Dim postedFile As HttpPostedFile = tbxFile.PostedFile
            Dim fInfo As FileInfo = New FileInfo(postedFile.FileName)
            Dim extension As String = df.Extension 'extension stays the same!

            If (Not ba.ImageHelper.IsImage(fInfo.Extension)) Then
                valFileType.IsValid = False
                Return -1
            End If

            Dim imagePath As String = Server.MapPath(ba.UrlHelper.GetImageUrl(df.Id, extension))
            Dim thumbUrl As String = ba.UrlHelper.GetImageThumbnailUrl(df.Id, extension)
            Dim thumbPath As String = Server.MapPath(thumbUrl)

            Dim width As Integer = 0
            Dim height As Integer = 0

            Try
                width = Convert.ToInt32(tbxWidth.Text.Trim)
            Catch ex As Exception
            End Try
            Try
                height = Convert.ToInt32(tbxHeight.Text.Trim)
            Catch ex As Exception
            End Try

            Dim ih As New ba.ImageHelper
            ih.MakeImage(postedFile.InputStream, thumbPath, ba.ConfigurationHelper.ThumbnailWidth, ba.ConfigurationHelper.ThumbnailHeight)
            Dim imdim As ba.ImageDimensions = ih.MakeImage(postedFile.InputStream, imagePath, width, height)

            Dim fInfo2 As New FileInfo(imagePath)
            df.Size = fInfo2.Length
            df.ImageWidth = imdim.Width
            df.ImageHeight = imdim.Height
            df.Update()

            ltrPreview2.Text = "<img border='1' src='" + thumbUrl + "'>"

            Return 1

        End Function

        Protected Overrides Sub ResetFormFields()
            BindData()
        End Sub

        Protected Overrides ReadOnly Property RedirectPage() As String
            Get
                Return "imageList.aspx"
            End Get
        End Property

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 75
            End Get
        End Property

    End Class

End Namespace