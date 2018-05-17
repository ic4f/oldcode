Imports System.Text
Imports dt = Foundation.Data
Imports ba = Foundation.BusinessAdmin
Imports c = Core

Public Class contentSelector
    Inherits System.Web.UI.Page

#Region "declarations"

    Protected WithEvents btnImages As Button
    Protected WithEvents btnFiles As Button
    Protected WithEvents btnPages As Button

    Protected pnlImages As Panel
    Protected WithEvents lnkShowImageOptions As LinkButton
    Protected pnlImageSearch As Panel
    Protected ddlImageLabel As DropDownList
    Protected tbxImageKeyword As TextBox
    Protected WithEvents btnImageSearch As Button
    Protected WithEvents btnImageReset As Button
    Protected pnlImageGrid As Panel
    Protected WithEvents dgrImages As c.MySortGrid

    Protected pnlFiles As Panel
    Protected WithEvents lnkShowFileOptions As LinkButton
    Protected pnlFileSearch As Panel
    Protected ddlFileLabel As DropDownList
    Protected ddlFileType As DropDownList
    Protected tbxFileKeyword As TextBox
    Protected WithEvents btnFileSearch As Button
    Protected WithEvents btnFileReset As Button
    Protected pnlFileGrid As Panel
    Protected WithEvents dgrFiles As c.MySortGrid

    Protected pnlPages As Panel
    Protected WithEvents lnkShowPageOptions As LinkButton
    Protected pnlPageSearch As Panel
    Protected ddlPageLabel As DropDownList
    Protected ddlPageType As DropDownList
    Protected tbxPageKeyword As TextBox
    Protected ltrTreeDisplay As Literal
    Protected WithEvents btnPageSearch As Button
    Protected WithEvents btnPageReset As Button
    Protected pnlPageGrid As Panel
    Protected WithEvents dgrPages As c.MySortGrid
    Protected hidMenu As System.Web.UI.HtmlControls.HtmlInputHidden

#End Region

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        dgrImages.BindingMethod = AddressOf BindImageData
        dgrFiles.BindingMethod = AddressOf BindFileData
        dgrPages.BindingMethod = AddressOf BindPageData
        If Not Page.IsPostBack Then
            btnPageSearch.Attributes.Add("OnClick", "getMenuId();")
            BindSearchOptions()
        End If
        MyBase.OnLoad(e)
    End Sub

#Region "main button handlers"

    Private Sub btnImages_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImages.Click
        changeButtonStatus(btnImages, False)
        changeButtonStatus(btnFiles, True)
        changeButtonStatus(btnPages, True)
        pnlImages.Visible = True
        pnlFiles.Visible = False
        pnlPages.Visible = False
    End Sub

    Private Sub btnFiles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiles.Click
        changeButtonStatus(btnImages, True)
        changeButtonStatus(btnFiles, False)
        changeButtonStatus(btnPages, True)
        pnlImages.Visible = False
        pnlFiles.Visible = True
        pnlPages.Visible = False
    End Sub

    Private Sub btnPages_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPages.Click
        changeButtonStatus(btnImages, True)
        changeButtonStatus(btnFiles, True)
        changeButtonStatus(btnPages, False)
        pnlImages.Visible = False
        pnlFiles.Visible = False
        pnlPages.Visible = True
    End Sub

    Private Sub changeButtonStatus(ByVal btn As Button, ByVal enableButton As Boolean)
        btn.Enabled = enableButton
        If enableButton Then
            btn.CssClass = "button"
            If (btn.Text.IndexOf("Show ") = -1) Then
                btn.Text = "Show " + btn.Text
            End If
        Else
            btn.CssClass = "button_a"
            btn.Text = btn.Text.Replace("Show ", "")
        End If
    End Sub

#End Region

#Region "bind search options"

    Private Sub BindImageOptions(ByVal dtLabels As DataTable)
        tbxImageKeyword.Text = ""
        bindLabelsList(ddlImageLabel, dtLabels)
    End Sub

    Private Sub BindFileOptions(ByVal dtLabels As DataTable)
        tbxFileKeyword.Text = ""
        bindLabelsList(ddlFileLabel, dtLabels)
        ddlFileType.DataSource = New dt.FileTypeData().GetCurrentNonImageTypeList()
        ddlFileType.DataTextField = "description"
        ddlFileType.DataValueField = "extension"
        ddlFileType.DataBind()
        ddlFileType.Items.Insert(0, New ListItem("-- select all", "0"))
    End Sub

    Private Sub BindPageOptions(ByVal dtLabels As DataTable)
        tbxPageKeyword.Text = ""
        bindLabelsList(ddlPageLabel, dtLabels)
        Dim dt As DataTable = New dt.StagingMenuData().GetOrdered().Tables(0)
        Dim pmt As New ba.PublicMenuTree(ba.PublicMenuTree.ConvertData(dt), c.Tree.ROOT_ID, "../../")
        ltrTreeDisplay.Text = pmt.GetMenuForCmsMenuSelection()
    End Sub

    Private Sub bindLabelsList(ByVal ddl As DropDownList, ByVal dtLabels As DataTable)
        ddl.DataSource = dtLabels
        ddl.DataTextField = "text"
        ddl.DataValueField = "id"
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("-- select all", "0"))
    End Sub

#End Region

#Region "search button handlers"

    Private Sub btnImageSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImageSearch.Click
        BindImageData()
        switchSearchDisplay(lnkShowImageOptions, pnlImageSearch, False)
    End Sub

    Private Sub btnFileSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFileSearch.Click
        BindFileData()
        switchSearchDisplay(lnkShowFileOptions, pnlFileSearch, False)
    End Sub

    Private Sub btnPageSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPageSearch.Click
        BindPageData()
        switchSearchDisplay(lnkShowPageOptions, pnlPageSearch, False)
    End Sub

#End Region

#Region "reset button handlers"

    Private Sub btnImageReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImageReset.Click
        BindImageOptions(getLabels())
        pnlImageGrid.Visible = False
    End Sub

    Private Sub btnFileReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFileReset.Click
        BindFileOptions(getLabels())
        pnlFileGrid.Visible = False
    End Sub

    Private Sub btnPageReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPageReset.Click
        BindPageOptions(getLabels())
        pnlPageGrid.Visible = False
    End Sub

#End Region

#Region "show search options handlers"

    Private Sub lnkImageOptions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkShowImageOptions.Click
        switchSearchDisplay(lnkShowImageOptions, pnlImageSearch, True)
    End Sub

    Private Sub lnkFileOptions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkShowFileOptions.Click
        switchSearchDisplay(lnkShowFileOptions, pnlFileSearch, True)
    End Sub

    Private Sub lnkPageOptions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkShowPageOptions.Click
        switchSearchDisplay(lnkShowPageOptions, pnlPageSearch, True)
    End Sub

#End Region

#Region "bind image data"

    Private Sub BindImageData()
        Dim dt As DataTable
        Dim keywords As String = (New ba.WebHelper).FilterUserInput(tbxImageKeyword.Text.Trim, True)

        If (ddlImageLabel.SelectedIndex > 0 Or keywords <> "") Then
            dt = New dt.FileData().GetImagesWithQuery(makeImageQuery(keywords))
        Else
            dt = New dt.FileData().GetImages(dgrImages.SortExpression).Tables(0)
        End If

        If (dt.Rows.Count > 0) Then
            pnlImageGrid.Visible = True
            dgrImages.Prefix = "../../"
            dgrImages.DataSource = dt
            dgrImages.DataBind()
        Else
            pnlImageGrid.Visible = False
        End If
    End Sub

    Private Function makeImageQuery(ByVal keywords As String) As String
        Dim sb As New StringBuilder

        If (ddlImageLabel.SelectedIndex > 0) Then
            sb.AppendFormat(" and fll.contentlabelid = {0} ", ddlImageLabel.SelectedValue)
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

#End Region

#Region "bind file data"

    Private Sub BindFileData()
        Dim dt As DataTable
        Dim keywords As String = (New ba.WebHelper).FilterUserInput(tbxFileKeyword.Text.Trim, True)

        If (ddlFileLabel.SelectedIndex > 0 Or ddlFileType.SelectedIndex > 0 Or keywords <> "") Then
            dt = New dt.FileData().GetNonImageFilesWithQuery(makeFileQuery(keywords))
        Else
            dt = New dt.FileData().GetNonImageFiles(dgrFiles.SortExpression).Tables(0)
        End If

        If (dt.Rows.Count > 0) Then
            pnlFileGrid.Visible = True
            dgrFiles.Prefix = "../../"
            dgrFiles.DataSource = dt
            dgrFiles.DataBind()
        Else
            pnlFileGrid.Visible = False
        End If
    End Sub

    Private Function makeFileQuery(ByVal keywords As String) As String
        Dim sb As New StringBuilder

        If (ddlFileLabel.SelectedIndex > 0) Then
            sb.AppendFormat(" and fll.contentlabelid = {0} ", ddlFileLabel.SelectedValue)
        End If

        If (ddlFileType.SelectedIndex > 0) Then
            sb.AppendFormat(" and f.extension = '{0}' ", ddlFileType.SelectedValue)
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

#End Region

#Region "bind page data"

    Private Sub BindPageData()
        Dim pages As DataTable

        Dim keywords As String = (New ba.WebHelper).FilterUserInput(tbxPageKeyword.Text.Trim, True)
        Dim menuid As Integer = Convert.ToInt32(hidMenu.Value)

        If (menuid > -1 Or ddlPageLabel.SelectedIndex > 0 Or keywords <> "") Then
            pages = New dt.PageData().GetPublishedPagesWithQuery(makePageQuery(menuid, keywords))
        Else
            pages = New dt.PageData().GetPublishedPages(dgrPages.SortExpression)
        End If

        If pages.Rows.Count > 0 Then
            pnlPageGrid.Visible = True
            dgrPages.Prefix = "../../"
            dgrPages.DataSource = pages
            dgrPages.DataBind()
        Else
            pnlPageGrid.Visible = False
        End If
    End Sub

    Private Function makePageQuery(ByVal menuid As Integer, ByVal keywords As String) As String
        Dim sb As New StringBuilder

        If (ddlPageLabel.SelectedIndex > 0) Then
            sb.AppendFormat(" and pll.contentlabelid = {0} ", ddlPageLabel.SelectedValue)
        End If

        If (menuid > 0) Then
            sb.AppendFormat(" and m.id = {0} ", menuid)
        End If

        If (keywords <> "") Then
            Dim arr As String() = keywords.Split()
            If arr.Length > 0 Then
                Dim sbKeywords As New StringBuilder
                For Each term As String In arr
                    sbKeywords.AppendFormat(" (p.pagetitle like '%{0}%' or p.contenttitle like '%{0}%' or p.body like '%{0}%' or p.bodydraft like '%{0}%' or p.admincomment like '%{0}%') and ", term)
                Next
                Dim result As String = sbKeywords.ToString
                sb.AppendFormat(" and {0} ", result.Substring(0, result.Length - 4))
            End If
        End If

        sb.AppendFormat(" order by {0} ", dgrPages.SortExpression)

        Return sb.ToString
    End Function

#End Region

#Region "datagrid handlers"

    Private Sub dgrImages_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrImages.ItemDataBound
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim id As String = DataBinder.Eval(e.Item.DataItem, "id").ToString()
            Dim extension As String = DataBinder.Eval(e.Item.DataItem, "extension").ToString()
            Dim lblPreview As Label = e.Item.Cells(1).Controls(0)
            Dim imageUrl = ba.UrlHelper.GetImageUrl(id, extension)
            Dim thumbUrl = ba.UrlHelper.GetImageThumbnailUrl(id, extension)
            lblPreview.Text = "&nbsp;&nbsp;<img src='" + thumbUrl + "'>&nbsp;&nbsp;"
            e.Item.Cells(2).Text = "<a class='gridlink' href='" + imageUrl + "'>" + imageUrl + "</a>"
        End If
    End Sub

    Private Sub dgrFiles_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrFiles.ItemDataBound
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim id As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "id"))
            Dim extension As String = DataBinder.Eval(e.Item.DataItem, "extension").ToString()
            Dim type As String = DataBinder.Eval(e.Item.DataItem, "type").ToString()
            Dim fileUrl As String = ba.UrlHelper.GetFileUrl(id, extension)
            e.Item.Cells(1).Text = "<a class='gridlink' href='" + fileUrl + "'>" + fileUrl + "</a>"
            e.Item.Cells(3).Text = "<img src='../../_system/images/filetypes/" + extension.Substring(1) + ".gif'>" + type
        End If
    End Sub

    Private Sub dgrPages_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrPages.ItemDataBound
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim url As String = DataBinder.Eval(e.Item.DataItem, "url").ToString()
            e.Item.Cells(1).Text = "<a href='" + url + "'>" + url + "</a>"
            e.Item.Cells(7).Text = "&nbsp;&nbsp;<a class='gridlink' target='_blank' href='" + url + "'>preview</a>&nbsp;&nbsp;"
        End If
    End Sub

#End Region

#Region "helper methods"

    Private Sub BindSearchOptions()
        Dim dtLabels As DataTable = getLabels()
        BindImageOptions(dtLabels)
        BindFileOptions(dtLabels)
        BindPageOptions(dtLabels)
    End Sub

    Private Function getLabels() As DataTable
        Return New dt.ContentLabelData().GetList()
    End Function

    Private Sub switchSearchDisplay(ByVal lnkShowSearchOptions As LinkButton, ByVal pnlSearch As Panel, ByVal showOptions As Boolean)
        lnkShowSearchOptions.Visible = Not showOptions
        pnlSearch.Visible = showOptions
    End Sub

#End Region



End Class