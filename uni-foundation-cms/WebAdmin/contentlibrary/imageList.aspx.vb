Imports ba = Foundation.BusinessAdmin
Imports System.IO
Imports System.Text
Imports dt = Foundation.Data
Imports c = Core

Namespace contentlibrary

    Public Class imageList
        Inherits ba.BasePage

        Protected pnlGrid As Panel
        Protected WithEvents dgrImages As c.MySortGrid
        Protected ddlLabel As DropDownList
        Protected tbxKeyword As TextBox
        Protected WithEvents btnSearch As Button
        Protected ltrCount As Literal

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            dgrImages.BindingMethod = AddressOf BindData
            If Not Page.IsPostBack Then
                dgrImages.SortColumnIndex = 8
                dgrImages.SortOrder = " desc"
                BindLists()
                BindLabelId()
                BindData()
            End If
        End Sub

        Private Sub BindLabelId()
            If Request("LabelId") <> "" Then
                ddlLabel.SelectedValue = Request("LabelId")
            End If
        End Sub

        Private Sub BindData()
            Dim dt As DataTable
            Dim keywords As String = (New ba.WebHelper).FilterUserInput(tbxKeyword.Text.Trim, True)

            If (ddlLabel.SelectedIndex > 0 Or keywords <> "") Then
                dt = New dt.FileData().GetImagesWithQuery(makeQuery(keywords))
            Else
                dt = New dt.FileData().GetImages(dgrImages.SortExpression).Tables(0)
            End If

            ltrCount.Text = dt.Rows.Count.ToString

            If (dt.Rows.Count > 0) Then
                pnlGrid.Visible = True
                dgrImages.Prefix = LinkPrefix
                dgrImages.DataSource = dt
                dgrImages.DataBind()
            Else
                pnlGrid.Visible = False
            End If
        End Sub

        Private Function makeQuery(ByVal keywords As String) As String
            Dim sb As New StringBuilder

            If (ddlLabel.SelectedIndex > 0) Then
                sb.AppendFormat(" and fll.contentlabelid = {0} ", ddlLabel.SelectedValue)
            End If

            If (keywords <> "") Then
                Dim arr As String() = keywords.Split()
                If arr.Length > 0 Then
                    Dim sbKeywords As New StringBuilder
                    For Each term As String In arr
                        sbKeywords.AppendFormat(" (f.description like '%{0}%' or f.admincomment like '%{0}%') and ", term)
                    Next
                    Dim result As String = sbKeywords.ToString
                    sb.AppendFormat(" and {0} ", result.Substring(0, result.Length - 4))
                End If
            End If

            sb.AppendFormat(" order by {0} ", dgrImages.SortExpression)

            Dim temp As String = sb.ToString()
            Return " where " + temp.Substring(4)
        End Function

        Private Sub BindLists()
            ddlLabel.DataSource = New dt.ContentLabelData().GetList()
            ddlLabel.DataTextField = "text"
            ddlLabel.DataValueField = "id"
            ddlLabel.DataBind()
            ddlLabel.Items.Insert(0, New ListItem("-- select all", "0"))
        End Sub

        Private Sub dgrImages_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrImages.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then

                Dim lnkInfo As HyperLink = e.Item.Cells(10).Controls(0)
                Dim lnkEdit As HyperLink = e.Item.Cells(11).Controls(0)
                lnkInfo.CssClass = "gridlink"
                lnkEdit.CssClass = "gridlink"

                Dim lblFile As Label = e.Item.Cells(1).Controls(0)
                Dim lblPreview As Label = e.Item.Cells(2).Controls(0)
                Dim extension As String = DataBinder.Eval(e.Item.DataItem, "extension")
                Dim id As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "id"))
                Dim imageUrl As String = ba.UrlHelper.GetImageUrl(id, extension)
                Dim thumbUrl As String = ba.UrlHelper.GetImageThumbnailUrl(id, extension)
                Dim imageName As String = ba.UrlHelper.GetImageName(id, extension)

                lblFile.Text = "<a href='" + imageUrl + "' target='_blank'><img src='../_system/images/filetypes/" +
                  extension.Substring(1) + ".gif' border='0'>" + imageName

                lblPreview.Text = "<a href='" + imageUrl + "' target='_blank'><img src='" + thumbUrl + "' border='1'></a>"
            End If
        End Sub


        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            BindData()
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 74
            End Get
        End Property

    End Class

End Namespace