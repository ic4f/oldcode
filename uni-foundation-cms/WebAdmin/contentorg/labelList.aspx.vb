Imports ba = Foundation.BusinessAdmin
Imports cr = Core
Imports dt = Foundation.Data

Namespace contentorg

    Public Class labelList
        Inherits ba.BasePage

        Protected WithEvents dgrLabels As cr.MyBaseGrid
        Protected pnlGrig As Panel

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            dgrLabels.BindingMethod = AddressOf BindData
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            dgrLabels.Prefix = LinkPrefix
            Dim data As DataTable = New dt.ContentLabelData().GetRecords().Tables(0)
            dgrLabels.DataSource = data
            dgrLabels.DataBind()
            If (data.Rows.Count > 0) Then
                pnlGrig.Visible = True
            Else
                pnlGrig.Visible = False
            End If
        End Sub

        Private Sub dgrLabels_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgrLabels.ItemCommand
            If e.CommandName = "Delete" Then
                Dim labelId As Integer = dgrLabels.DataKeys(e.Item.ItemIndex)
                Dim dl As New dt.ContentLabelData
                dl.Delete(labelId)

            ElseIf e.CommandName = "Up" Then
                Dim labelId As Integer = dgrLabels.DataKeys(e.Item.ItemIndex)
                Dim dt As DataTable = New dt.ContentLabelData().GetRecords().Tables(0)
                Dim ids(dt.Rows.Count) As Integer
                Dim temp As Integer
                For pos As Integer = 0 To dt.Rows.Count - 1
                    ids(pos) = dt.Rows(pos)(0)
                    If (dt.Rows(pos)(0) = labelId And pos > 0) Then
                        temp = ids(pos - 1)
                        ids(pos - 1) = labelId
                        ids(pos) = temp
                    End If
                Next
                Dim ld As New dt.ContentLabelData
                Dim rank As Integer = 0
                For Each currid As Integer In ids
                    ld.UpdateRank(currid, rank, Identity.UserId)
                    rank = rank + 1
                Next

            ElseIf e.CommandName = "Down" Then
                Dim labelId As Integer = dgrLabels.DataKeys(e.Item.ItemIndex)
                Dim dt As DataTable = New dt.ContentLabelData().GetRecords().Tables(0)
                Dim ids(dt.Rows.Count) As Integer
                For pos As Integer = 0 To dt.Rows.Count - 1
                    ids(pos) = dt.Rows(pos)(0)
                    If (dt.Rows(pos)(0) = labelId And pos < dt.Rows.Count - 1) Then
                        ids(pos) = dt.Rows(pos + 1)(0)
                        ids(pos + 1) = labelId
                        pos = pos + 2
                    End If
                Next
                Dim ld As New dt.ContentLabelData
                Dim rank As Integer = 0
                For Each currid As Integer In ids
                    ld.UpdateRank(currid, rank, Identity.UserId)
                    rank = rank + 1
                Next
            End If
            BindData()
        End Sub

        Private Sub dgrLabels_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrLabels.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then

                Dim lnkPages As HyperLink = e.Item.Cells(2).Controls(0)
                Dim lnkModules As HyperLink = e.Item.Cells(3).Controls(0)
                Dim lnkFiles As HyperLink = e.Item.Cells(4).Controls(0)
                Dim lnkImages As HyperLink = e.Item.Cells(5).Controls(0)
                Dim lnkEdit As HyperLink = e.Item.Cells(9).Controls(0)
                Dim deleteLink As LinkButton = e.Item.Cells(10).Controls(0)
                lnkPages.CssClass = "gridlink"
                lnkModules.CssClass = "gridlink"
                lnkFiles.CssClass = "gridlink"
                lnkImages.CssClass = "gridlink"
                lnkEdit.CssClass = "gridlink"
                deleteLink.CssClass = "gridlinkalert"

                Dim labelText As String = e.Item.Cells(0).Text.ToUpper
                deleteLink.Attributes("onclick") = "javascript:return confirm('Are you sure you want to delete label " + labelText + " ?')"
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 55
            End Get
        End Property

    End Class

End Namespace