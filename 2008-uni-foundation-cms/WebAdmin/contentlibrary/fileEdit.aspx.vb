Imports System.Text
Imports System.IO
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data
Imports c = Core

Namespace contentlibrary

    Public Class fileEdit
        Inherits ba.EditBasePage

        Protected tbxFile As HtmlInputFile
        Protected tbxDescription As TextBox
        Protected tbxComment As TextBox
        Protected hidCats As HtmlControls.HtmlInputHidden
        Protected WithEvents valUnique As CustomValidator
        Protected valFileType As CustomValidator
        Protected ltrFrame As Literal
        Protected ltrFile As Literal
        Protected ltrTypes As Literal

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
                ltrTypes.Text = "You may select a file of the same format"
            End If
        End Sub

        Private Sub BindData()
            btnSave.Attributes.Add("OnClick", "getlabels();")
            ltrFrame.Text = "<iframe id=""iframecat"" name=""iframecat"" src=""../_system/datalists/selectedFileLabels.aspx?FileId=" + GetId().ToString + """ class=""sidebaroptionframe"" style=""height:243px;""></iframe>"
            Dim f As New dt.File(GetId())
            tbxDescription.Text = f.Description
            tbxComment.Text = f.Comment
            Dim fileUrl = ba.UrlHelper.GetFileUrl(f.Id, f.Extension)
            Dim fileName As String = ba.UrlHelper.GetFileName(f.Id, f.Extension)

            ltrFile.Text = "<a href='" + fileUrl + "' target='_blank'><img src='../_system/images/filetypes/" +
              f.Extension.Substring(1) + ".gif' border='0'>" + fileName + "</a>"
        End Sub

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "File"
            End Get
        End Property

        Protected Overrides Function SaveItem() As Integer

            Dim fileId As Integer = GetId()

            Dim df As New dt.File(fileId)
            df.Description = tbxDescription.Text.Trim
            df.Comment = tbxComment.Text.Trim

            Dim result As Integer = df.Update
            If (result < 0) Then
                valUnique.IsValid = False
            Else
                Dim dl As New dt.FileContentLabelData
                dl.DeleteByFile(fileId)
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

                If tbxFile.PostedFile.ContentLength > 0 Then
                    If uploadNewFile(df) = 0 Then
                        Return -1
                    End If
                End If

            End If
            Return result
        End Function

        Private Function uploadNewFile(ByVal df As dt.File) As Integer

            Dim postedFile As HttpPostedFile = tbxFile.PostedFile
            Dim fInfo As FileInfo = New FileInfo(postedFile.FileName)

            If (df.Extension <> fInfo.Extension) Then
                valFileType.IsValid = False
                Return 0
            End If

            Dim path As String = Server.MapPath(ba.UrlHelper.GetFileUrl(df.Id, fInfo.Extension))
            postedFile.SaveAs(path)

            Dim fInfo2 As New FileInfo(path)
            df.Size = fInfo2.Length
            df.Update()

            Return 1

        End Function

        Protected Overrides Sub ResetFormFields()
            BindData()
        End Sub

        Protected Overrides ReadOnly Property RedirectPage() As String
            Get
                Return "fileList.aspx"
            End Get
        End Property

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 49
            End Get
        End Property

    End Class

End Namespace