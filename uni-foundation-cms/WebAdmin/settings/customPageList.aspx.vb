Imports System.Text
Imports ba = Foundation.BusinessAdmin
Imports cr = Core
Imports dt = Foundation.Data

Namespace settings

    Public Class customPageList
        Inherits ba.BasePage

        Protected ltrCount As Literal
        Protected WithEvents dgrPages As cr.MySortGrid
        Protected pnlGrid As Panel

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            dgrPages.BindingMethod = AddressOf BindData
            If Not Page.IsPostBack Then
                dgrPages.SortColumnIndex = 5
                dgrPages.SortOrder = " desc"
                BindData()
            End If
        End Sub

        Private Sub BindData()
            Dim pages As DataTable = New dt.PageData().GetCustomPages(dgrPages.SortExpression)
            ltrCount.Text = pages.Rows.Count.ToString
            If pages.Rows.Count > 0 Then
                pnlGrid.Visible = True
                dgrPages.Prefix = LinkPrefix
                dgrPages.DataSource = pages
                dgrPages.DataBind()
            Else
                pnlGrid.Visible = False
            End If
        End Sub


        Private Sub dgrPages_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrPages.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then

                Dim lnkInfo As HyperLink = e.Item.Cells(8).Controls(0)
                Dim lnkEdit As HyperLink = e.Item.Cells(9).Controls(0)
                lnkInfo.CssClass = "gridlink"
                lnkEdit.CssClass = "gridlink"

                Dim ispublished As Boolean = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "ispublished"))
                If ispublished Then
                    e.Item.Cells(4).Text = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "publisheddate")).ToShortDateString
                Else
                    e.Item.Cells(4).Text = "-"
                End If

                Dim url As String = DataBinder.Eval(e.Item.DataItem, "url")
                e.Item.Cells(3).Text = ba.UrlHelper.GetPageFileNameByUrl(url)
                e.Item.Cells(10).Text = "<a href='" + url + "&draft=1' class='gridlink' target='_blank'>preview</a>"
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 102
            End Get
        End Property

    End Class

End Namespace