Imports System.IO
Imports c = Core
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Namespace contentlibrary

    Public Class lefthimagelist
        Inherits ba.BasePage

        Protected pnlGrid As Panel
        Protected WithEvents dgrImages As c.MySortGrid

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            dgrImages.BindingMethod = AddressOf BindData
            If Not Page.IsPostBack Then
                dgrImages.SortColumnIndex = 2
                dgrImages.SortOrder = " desc"
                BindData()
            End If
        End Sub

        Private Sub BindData()
            dgrImages.Prefix = LinkPrefix
            Dim dt As DataTable = New dt.HeaderImageData().GetRecords(ba.HeaderImageLocationCode.Left, dgrImages.SortExpression)
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

                Dim lnkInfo As HyperLink = e.Item.Cells(4).Controls(0)
                lnkInfo.CssClass = "gridlink"

                Dim lnkPreview As HyperLink = e.Item.FindControl("lnkPreview")
                Dim id As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "id"))
                Dim url As String = ba.UrlHelper.GetHeaderImageUrlLeft(id)
                lnkPreview.Text = "<a href='" + url + "' target='_blank'><img src='" + url + "' border='1'></a>"
            End If
        End Sub



        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 112
            End Get
        End Property

    End Class

End Namespace