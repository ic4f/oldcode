Imports System.IO
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Namespace contentlibrary

    Public Class myFileInfo
        Inherits ba.BasePage

        Protected ltrTitle As Literal
        Protected ltrUrl As Literal
        Protected ltrId As Literal
        Protected WithEvents btnEdit As Button

        Protected pnlCanDelete As Panel
        Protected WithEvents btnDelete As Button

        Protected pnlDependencies As Panel

        Protected pnlDependPages As Panel
        Protected ltrInboundPageLinks As Literal
        Protected WithEvents rptDependPages As Repeater

        Public Const MAX_RECORDS As Integer = 10

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            btnDelete.Attributes("onclick") = "javascript:return confirm('Are you sure you want to delete this file?')"
            Dim myfile As New dt.File(GetId())
            BindInfo(myfile)
            Dim fd As New dt.FileData
            If HasDependencies(myfile, fd) Then
                pnlDependencies.Visible = True
            Else
                pnlCanDelete.Visible = True
            End If
        End Sub

        Private Function GetId() As Integer
            Return Convert.ToInt32(Request("Id"))
        End Function

        Private Sub BindInfo(ByVal myfile As dt.File)
            ltrTitle.Text = myfile.Description
            Dim url As String = ba.UrlHelper.GetFileUrl(myfile.Id, myfile.Extension)
            ltrUrl.Text = "<a href='" + url + "' target='_blank'>" + url + "</a>"
            ltrId.Text = myfile.Id.ToString
        End Sub

        Private Function HasDependencies(ByVal myfile As dt.File, ByVal fd As dt.FileData) As Boolean
            Dim depends As Boolean = False

            Dim dsPages As DataSet = fd.GetInboundPageLinks(myfile.Id)
            Dim countPages As Integer = Convert.ToInt32(dsPages.Tables(1).Rows(0)(0))
            If countPages > 0 Then
                If countPages = 1 Then
                    ltrInboundPageLinks.Text = countPages.ToString + " page refers to this file"
                Else
                    ltrInboundPageLinks.Text = countPages.ToString + " pages refer to this file"
                End If
                If (MAX_RECORDS < countPages) Then
                    ltrInboundPageLinks.Text += " (displaying top " + MAX_RECORDS.ToString + ")"
                End If
                ltrInboundPageLinks.Text += ":"
                rptDependPages.DataSource = dsPages.Tables(0)
                rptDependPages.DataBind()
                pnlDependPages.Visible = True
                depends = True
            End If

            Return depends
        End Function

        Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
            Response.Redirect("fileEdit.aspx?Id=" + GetId().ToString)
        End Sub

        Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
            Dim fileId As Integer = GetId()
            Dim f As New dt.File(fileId)
            File.Delete(Server.MapPath(ba.UrlHelper.GetFileUrl(fileId, f.Extension)))
            Dim fd As New dt.FileData
            fd.Delete(fileId)
            Response.Redirect("fileList.aspx")
        End Sub

        Private Sub rptDependPages_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDependPages.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                Dim ltrDependPage As Literal = e.Item.FindControl("ltrDependPage")
                Dim pageId As String = DataBinder.Eval(e.Item.DataItem, "id")
                Dim title As String = DataBinder.Eval(e.Item.DataItem, "pagetitle")
                ltrDependPage.Text = "<a href='../webpages/pageInfo.aspx?Id=" + pageId + "'>" + title + " [" + pageId + "]</a>"
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 107
            End Get
        End Property

    End Class

End Namespace