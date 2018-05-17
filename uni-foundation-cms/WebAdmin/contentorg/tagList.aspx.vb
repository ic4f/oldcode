Imports ba = Foundation.BusinessAdmin
Imports cr = Core
Imports dt = Foundation.Data

Namespace contentorg

    Public Class tagList
        Inherits ba.BasePage

        Protected WithEvents dgrTags As cr.MySortGrid
        Protected pnlGrig As Panel

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            dgrTags.BindingMethod = AddressOf BindData
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            dgrTags.Prefix = LinkPrefix
            Dim data As DataTable = New dt.TagData().GetRecords(dgrTags.SortExpression).Tables(0)
            dgrTags.DataSource = data
            dgrTags.DataBind()
            If (data.Rows.Count > 0) Then
                pnlGrig.Visible = True
            Else
                pnlGrig.Visible = False
            End If
        End Sub

        Private Sub dgrTags_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgrTags.ItemCommand
            If e.CommandName = "Delete" Then
                Dim tagId As Integer = dgrTags.DataKeys(e.Item.ItemIndex)
                Dim dt As New dt.TagData
                dt.Delete(tagId)
            End If
            BindData()
        End Sub

        Private Sub dgrTags_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrTags.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then

                Dim lnkPages As HyperLink = e.Item.Cells(1).Controls(0)
                Dim lnkEdit As HyperLink = e.Item.Cells(5).Controls(0)
                Dim deleteLink As LinkButton = e.Item.Cells(6).Controls(0)
                lnkPages.CssClass = "gridlink"
                lnkEdit.CssClass = "gridlink"
                deleteLink.CssClass = "gridlinkalert"

                Dim tagText As String = e.Item.Cells(0).Text.ToUpper
                deleteLink.Attributes("onclick") = "javascript:return confirm('Are you sure you want to delete tag " + tagText + " ?')"
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 78
            End Get
        End Property

    End Class

End Namespace