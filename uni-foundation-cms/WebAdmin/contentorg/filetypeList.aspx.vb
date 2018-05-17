Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data
Imports c = Core

Namespace contentorg

    Public Class filetypeList
        Inherits ba.BasePage

        Protected pnlGrid As Panel
        Protected WithEvents dgrTypes As c.MySortGrid

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            dgrTypes.BindingMethod = AddressOf BindData
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            dgrTypes.Prefix = LinkPrefix
            Dim dt As DataTable = New dt.FileTypeData().GetRecords(dgrTypes.SortExpression).Tables(0)
            If (dt.Rows.Count > 0) Then
                pnlGrid.Visible = True
                dgrTypes.DataSource = dt
                dgrTypes.DataBind()
            Else
                pnlGrid.Visible = False
            End If
        End Sub

        Private Sub dgrTypes_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrTypes.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                Dim lnkEdit As HyperLink = e.Item.Cells(2).Controls(0)
                lnkEdit.CssClass = "gridlink"
                Dim extension As String = DataBinder.Eval(e.Item.DataItem, "extension").ToString()
                Dim type As String = DataBinder.Eval(e.Item.DataItem, "description").ToString()
                e.Item.Cells(1).Text = "<img src='../_system/images/filetypes/" + extension.Substring(1) + ".gif'>" + type
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 51
            End Get
        End Property

    End Class

End Namespace
