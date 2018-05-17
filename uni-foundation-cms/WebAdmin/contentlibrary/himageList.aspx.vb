Imports System.IO
Imports c = Core
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Namespace contentlibrary

    Public Class himageList
        Inherits ba.BasePage

        Protected pnlGrid As Panel
        Protected WithEvents dgrImages As c.MyBaseGrid

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            dgrImages.BindingMethod = AddressOf BindData
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            dgrImages.Prefix = LinkPrefix
            Dim dt As DataTable = New dt.HeaderImageData().GetRecords(ba.HeaderImageLocationCode.Right, "i.created desc")
            If (dt.Rows.Count > 0) Then
                pnlGrid.Visible = True
                dgrImages.DataSource = dt
                dgrImages.DataBind()
            Else
                pnlGrid.Visible = False
            End If
        End Sub

        Private Sub dgrImages_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrImages.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then

                Dim lnkDelete As LinkButton = e.Item.Cells(3).Controls(0)
                lnkDelete.CssClass = "gridlinkalert"

                Dim lnkPreview As HyperLink = e.Item.FindControl("lnkPreview")
                Dim id As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "id"))
                Dim url As String = ba.UrlHelper.GetHeaderImageUrlRight(id)
                lnkPreview.Text = "<a href='" + url + "' target='_blank'><img src='" + url + "' border='1' width='300px'></a>"

                lnkDelete.Attributes("onclick") = "javascript:return confirm('Are you sure you want to delete this header image?')"
            End If
        End Sub

        Private Sub dgrImages_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgrImages.ItemCommand
            If e.CommandName = "Delete" Then
                Dim id As Integer = dgrImages.DataKeys(e.Item.ItemIndex)
                Dim hd As New dt.HeaderImageData
                hd.Delete(id)
                File.Delete(Server.MapPath(ba.UrlHelper.GetHeaderImageUrlRight(id)))
                BindData()
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 37
            End Get
        End Property

    End Class

End Namespace