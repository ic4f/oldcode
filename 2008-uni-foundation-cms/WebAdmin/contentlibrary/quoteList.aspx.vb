Imports System.IO
Imports System.Text
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data
Imports c = Core

Namespace contentlibrary

    Public Class quoteList
        Inherits ba.BasePage

        Protected pnlGrid As Panel
        Protected WithEvents dgrQuotes As c.MySortGrid

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            dgrQuotes.BindingMethod = AddressOf BindData
            If Not Page.IsPostBack Then
                dgrQuotes.SortColumnIndex = 4
                dgrQuotes.SortOrder = " desc"
                BindData()
            End If
        End Sub

        Private Sub BindData()
            dgrQuotes.Prefix = LinkPrefix
            Dim dt As DataTable = New dt.QuoteData().GetRecords(dgrQuotes.SortExpression)
            If (dt.Rows.Count > 0) Then
                pnlGrid.Visible = True
                dgrQuotes.DataSource = dt
                dgrQuotes.DataBind()
            Else
                pnlGrid.Visible = False
            End If
        End Sub

        Private Sub dgrQuotes_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrQuotes.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then

                Dim lnkEdit As HyperLink = e.Item.Cells(6).Controls(0)
                Dim lnkDelete As LinkButton = e.Item.Cells(7).Controls(0)
                lnkEdit.CssClass = "gridlink"
                lnkDelete.CssClass = "gridlinkalert"

                Dim text As String = e.Item.Cells(1).Text.ToUpper
                lnkDelete.Attributes("onclick") = "javascript:return confirm('Are you sure you want to delete the quote " + text + " ?')"
            End If
        End Sub

        Private Sub dgrQuotes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgrQuotes.ItemCommand
            If e.CommandName = "Delete" Then
                Dim id As Integer = dgrQuotes.DataKeys(e.Item.ItemIndex)
                Dim qd As New dt.QuoteData
                qd.Delete(id)
                BindData()
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 40
            End Get
        End Property

    End Class

End Namespace