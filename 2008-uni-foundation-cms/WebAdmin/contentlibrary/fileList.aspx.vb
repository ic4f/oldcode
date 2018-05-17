Imports System.IO
Imports System.Text
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data
Imports c = Core

Namespace contentlibrary

    Public Class fileList
        Inherits ba.BasePage

        Protected pnlGrid As Panel
        Protected WithEvents dgrFiles As c.MySortGrid
        Protected ddlLabel As DropDownList
        Protected ddlType As DropDownList
        Protected tbxKeyword As TextBox
        Protected WithEvents btnSearch As Button
        Protected ltrCount As Literal

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            dgrFiles.BindingMethod = AddressOf BindData
            If Not Page.IsPostBack Then
                dgrFiles.SortColumnIndex = 6
                dgrFiles.SortOrder = " desc"
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

        Private Sub BindLists()
            ddlLabel.DataSource = New dt.ContentLabelData().GetList()
            ddlLabel.DataTextField = "text"
            ddlLabel.DataValueField = "id"
            ddlLabel.DataBind()
            ddlLabel.Items.Insert(0, New ListItem("-- select all", "0"))

            ddlType.DataSource = New dt.FileTypeData().GetCurrentNonImageTypeList()
            ddlType.DataTextField = "description"
            ddlType.DataValueField = "extension"
            ddlType.DataBind()
            ddlType.Items.Insert(0, New ListItem("-- select all", "0"))
        End Sub

        Private Sub BindData()
            Dim dt As DataTable
            Dim keywords As String = (New ba.WebHelper).FilterUserInput(tbxKeyword.Text.Trim, True)

            If (ddlLabel.SelectedIndex > 0 Or ddlType.SelectedIndex > 0 Or keywords <> "") Then
                dt = New dt.FileData().GetNonImageFilesWithQuery(makeQuery(keywords))
            Else
                dt = New dt.FileData().GetNonImageFiles(dgrFiles.SortExpression).Tables(0)
            End If

            ltrCount.Text = dt.Rows.Count.ToString

            If (dt.Rows.Count > 0) Then
                pnlGrid.Visible = True
                dgrFiles.Prefix = LinkPrefix
                dgrFiles.DataSource = dt
                dgrFiles.DataBind()
            Else
                pnlGrid.Visible = False
            End If
        End Sub

        Private Function makeQuery(ByVal keywords As String) As String
            Dim sb As New StringBuilder

            If (ddlLabel.SelectedIndex > 0) Then
                sb.AppendFormat(" and fll.contentlabelid = {0} ", ddlLabel.SelectedValue)
            End If

            If (ddlType.SelectedIndex > 0) Then
                sb.AppendFormat(" and f.extension = '{0}' ", ddlType.SelectedValue)
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

            sb.AppendFormat(" order by {0} ", dgrFiles.SortExpression)

            Return sb.ToString
        End Function

        Private Sub dgrFiles_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrFiles.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then

                Dim lnkInfo As HyperLink = e.Item.Cells(8).Controls(0)
                Dim lnkEdit As HyperLink = e.Item.Cells(9).Controls(0)
                lnkEdit.CssClass = "gridlink"
                lnkInfo.CssClass = "gridlink"
                Dim lblFile As Label = e.Item.Cells(1).Controls(0)
                Dim extension As String = DataBinder.Eval(e.Item.DataItem, "extension")
                Dim id As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "id"))
                Dim url As String = ba.UrlHelper.GetFileUrl(id, extension)
                Dim fileName As String = ba.UrlHelper.GetFileName(id, extension)
                lblFile.Text = "<a href='" + url + "' target='_blank'><img src='../_system/images/filetypes/" +
                  extension.Substring(1) + ".gif' border='0'>" + fileName + "</a>"
            End If
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            BindData()
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 48
            End Get
        End Property

    End Class

End Namespace