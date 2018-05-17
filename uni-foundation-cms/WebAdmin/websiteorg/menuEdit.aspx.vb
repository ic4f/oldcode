Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data
Imports c = Core

Namespace websiteorg

    Public Class menuEdit
        Inherits ba.EditBasePage

        Protected tbxText As TextBox
        Protected ltrTreeDisplay As Literal
        Protected ltrTopLevel As Literal
        Protected ddlPages As DropDownList
        Protected ddlImage As DropDownList

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            Try
                Dim sm As New dt.StagingMenu(GetId())
                tbxText.Text = sm.Text
                BindTopLevel(sm.ParentId)
                BindMenuBrowser(GetId(), sm.Id, sm.ParentId)
                ddlPages.DataSource = New dt.PageData().GetPublishedByMenuId(sm.Id)
                ddlPages.DataTextField = "pagetitle"
                ddlPages.DataValueField = "id"
                ddlPages.DataBind()

                ddlPages.Items.Insert(0, New ListItem("select page...", "-1"))
                If sm.PageId > -1 Then
                    ddlPages.SelectedValue = sm.PageId
                End If

                ddlImage.DataSource = New dt.HeaderImageData().GetImagesForDisplay(ba.HeaderImageLocationCode.Left)
                ddlImage.DataTextField = "description"
                ddlImage.DataValueField = "id"
                ddlImage.DataBind()
                ddlImage.Items.Insert(0, New ListItem("select image...", "-1"))
                If sm.HeaderImageId > -1 Then
                    ddlImage.SelectedValue = sm.HeaderImageId
                Else
                    ddlImage.Items.Insert(0, New ListItem("select image...", "-1"))
                End If

            Catch ex As Exception
            End Try
        End Sub

        Private Sub BindTopLevel(ByVal parentMenuId As Integer)
            Dim selected As String = ""
            If (parentMenuId = c.Tree.ROOT_ID) Then
                selected = "checked='checked'"
            End If
            ltrTopLevel.Text = "<input type='radio' style='margin-bottom:-3px;' value='-1' " + selected + " name='" + ba.PublicMenuTreeItem.RADIOBUTTON_NAME + "'><span style='font-size:11px;'>None (top level menu)</span>"
        End Sub

        Private Sub BindMenuBrowser(ByVal selectedId As Integer, ByVal editMenuId As Integer, ByVal editMenuParentId As Integer)
            Dim dt As DataTable = New dt.StagingMenuData().GetOrdered().Tables(0)
            Dim pmt As New ba.PublicMenuTree(ba.PublicMenuTree.ConvertData(dt), selectedId, "../")
            ltrTreeDisplay.Text = pmt.GetMenuForCmsParentSelection(editMenuId, editMenuParentId)
        End Sub

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "Menu Item"
            End Get
        End Property

        Protected Overrides Function SaveItem() As Integer

            Dim menuId As Integer = GetId()
            Dim sm As New dt.StagingMenu(menuId)
            Dim smeld As New dt.StagingMenuEditLogData
            Dim smd As New dt.StagingMenuData
            Dim log As String

            Dim oldtext As String = sm.Text
            Dim newText As String = tbxText.Text.Trim
            If (oldtext <> newText) Then
                smd.UpdateText(menuId, newText)
                log = "UPDATED TEXT: [" + oldtext + "] to [" + newText + "]"
                smeld.CreateLog(log, Identity.UserId)
            End If

            Dim oldParentId As Integer = sm.ParentId
            Dim newParentId As Integer = Convert.ToInt32(Request(ba.PublicMenuTreeItem.RADIOBUTTON_NAME))
            If (oldParentId <> newParentId) Then
                smd.UpdateParent(menuId, newParentId)
                log = "UPDATED PARENT MENU: [" + newText + "]"
                smeld.CreateLog(log, Identity.UserId)
            End If

            Dim oldPageId As Integer = sm.PageId
            Dim newPageId As Integer = Convert.ToInt32(ddlPages.SelectedValue)
            If (oldPageId <> newPageId) Then
                smd.UpdatePage(menuId, newPageId)
                log = "UPDATED MENU PAGE: [" + newText + "] page changed to [" + ddlPages.SelectedItem.Text + "]"
                smeld.CreateLog(log, Identity.UserId)
            End If

            Dim oldImageId As Integer = sm.HeaderImageId
            Dim newImageId As Integer = Convert.ToInt32(ddlImage.SelectedValue)
            If (oldImageId <> newImageId) Then
                smd.UpdateHeaderImage(menuId, newImageId)
                log = "UPDATED MENU IMAGE: [" + newText + "] image changed to [" + ddlImage.SelectedItem.Text + "]"
                smeld.CreateLog(log, Identity.UserId)
            End If

            Return 1
        End Function

        Protected Overrides Sub ResetFormFields()
            BindData()
        End Sub

        Private Function getParentId() As Integer
            Dim sm As New dt.StagingMenu(GetId())
            Return sm.ParentId
        End Function

        Protected Overrides ReadOnly Property RedirectPage() As String
            Get
                Return "menus.aspx?Id=" + getParentId().ToString()
            End Get

        End Property
        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 4
            End Get
        End Property

    End Class

End Namespace