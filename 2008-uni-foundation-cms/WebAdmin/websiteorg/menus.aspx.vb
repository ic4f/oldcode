Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data
Imports c = Core
Imports System.Text

Namespace websiteorg

    Public Class menus
        Inherits ba.BasePage

        Public Const HOMEPAGE_MENU_ID = 1

        Protected ltrTreeDisplay As Literal
        Protected WithEvents dgrMenu As c.MyBaseGrid
        Protected WithEvents btnAdd As Button
        Protected tbxAdd As TextBox
        Protected WithEvents btnPublish As Button
        Protected lnkLevelUp1 As HyperLink
        Protected lnkLevelUp2 As HyperLink
        Protected pnlMenuGrid As Panel
        Protected lblNoMenu As Label
        Protected lblMenuTitle As Label
        Protected pnlAdd As Panel
        Protected ltrPublishText As Literal
        Protected ltrModified As Literal
        Protected ltrPublished As Literal
        Protected pnlLogGrid As Panel
        Protected dgrLog As c.MyBaseGrid
        Protected lblNoLog As Label

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()

            Dim dt As DataTable = New dt.StagingMenuData().GetOrdered().Tables(0)
            Dim pmTree As New ba.PublicMenuTree(ba.PublicMenuTree.ConvertData(dt), GetMenuId(), "../")

            BindHeaderData(pmTree)
            BindMenuDisplay(pmTree)
            BindMenu()
            BindPublisher()
            BindLog()
            btnPublish.Attributes("onclick") = "javascript:return confirm('The website will be updated with the current menu')"
        End Sub

        Private Sub BindHeaderData(ByVal pmTree As ba.PublicMenuTree)
            If (GetMenuId() > -1) Then
                Dim sm As New Data.StagingMenu(GetMenuId)
                lnkLevelUp1.Visible = True
                lnkLevelUp1.NavigateUrl = "menus.aspx?Id=" + sm.ParentId.ToString
                lnkLevelUp2.Visible = True
                lnkLevelUp2.NavigateUrl = "menus.aspx?Id=" + sm.ParentId.ToString
                lblMenuTitle.Text = "Menu: <span style=""font-weight:bold;color:#414141;"">" + sm.Text + "</span>"

                pnlAdd.Visible = True
                Dim pmTreeItem As ba.PublicMenuTreeItem = pmTree.GetItem(sm.Id)

                If (pmTreeItem.Depth >= (ba.ConfigurationHelper.PublicMenuLevels - 1) Or GetMenuId() = HOMEPAGE_MENU_ID) Then
                    pnlAdd.Visible = False
                End If

            Else
                lnkLevelUp1.Visible = False
                lnkLevelUp2.Visible = False
                lblMenuTitle.Text = "Top Level Menu"
            End If
        End Sub

        Private Sub BindMenuDisplay(ByVal pmTree As ba.PublicMenuTree)
            ltrTreeDisplay.Text = pmTree.GetMenuForCmsBrowsing()
        End Sub

        Private Function GetMenuId() As Int16
            Dim menuid As Integer = -1
            If Request("id") <> "" Then
                menuid = Convert.ToInt32(Request("id"))
            End If
            Return menuid
        End Function

        Private Sub BindMenu()
            Dim dt As DataTable = New dt.StagingMenuData().GetMenuByParent(GetMenuId).Tables(0)
            If (dt.Rows.Count > 0) Then
                dgrMenu.DataSource = dt
                dgrMenu.DataBind()
                pnlMenuGrid.Visible = True
                lblNoMenu.Visible = False
            Else
                pnlMenuGrid.Visible = False
                lblNoMenu.Visible = True
            End If
        End Sub

        Private Sub BindPublisher()
            If (New dt.StagingMenuData().IsPublishable()) Then
                btnPublish.Enabled = True
                ltrPublishText.Text = "The production menu will be replaced with the current staging menu"
            Else
                btnPublish.Enabled = False
                ltrPublishText.Text = "The menu can be published only if each menu item (including sub-menus) has a default web page assinged, which is published"
            End If
        End Sub

        Private Sub BindLog()
            Dim dt As DataTable = New dt.StagingMenuEditLogData().GetLogs()
            If (dt.Rows.Count > 0) Then
                dgrLog.DataSource = dt
                dgrLog.DataBind()
                pnlLogGrid.Visible = True
                lblNoLog.Visible = False

                ltrModified.Text = Convert.ToDateTime(dt.Rows(0)(2)) + " by " + dt.Rows(0)(4)
                ltrPublished.Text = Convert.ToDateTime(dt.Rows(dt.Rows.Count - 1)(2)) + " by " + dt.Rows(dt.Rows.Count - 1)(4)
            Else
                pnlLogGrid.Visible = False
                lblNoLog.Visible = True
            End If
        End Sub

        Private Function GetParentName() As String
            Dim parentid As Integer = GetMenuId()
            Dim parentName As String = "top level menu"
            If parentid > -1 Then
                Dim parent As New dt.StagingMenu(parentid)
                parentName = parent.Text
            End If
            Return parentName
        End Function

        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
            Dim newMenu As String = tbxAdd.Text.Trim
            If newMenu <> "" Then
                Dim smd As New dt.StagingMenuData
                Dim newMenuId As Integer = smd.Create(GetMenuId, newMenu, 0)

                Dim log As String = "CREATED: [" + newMenu + "] to [" + GetParentName() + "]"
                Dim smeld As New dt.StagingMenuEditLogData
                smeld.CreateLog(log, Identity.UserId)

                UpdateRanking(newMenuId, True, False)

                tbxAdd.Text = ""
                BindData()
            End If

        End Sub

        Private Sub dgrMenu_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrMenu.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then

                Dim menuId As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "id"))
                Dim childMenus As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "childmenus"))
                Dim childPages As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "pages"))
                Dim hasPage As Boolean = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "HasDefaultPage"))
                Dim isPageCategoryDefault As Boolean = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "IsPageCategoryDefault"))
                Dim canEdit As Boolean = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "can_edit"))

                Dim lnkItem As HyperLink = e.Item.Cells(0).Controls(0)
                Dim menusLink As HyperLink = e.Item.Cells(2).FindControl("lnkSubmenus")
                Dim pagesLink As HyperLink = e.Item.Cells(3).FindControl("lnkPages")
                Dim lnkEdit As HyperLink = e.Item.Cells(5).Controls(0)
                Dim deleteLink As LinkButton = e.Item.Cells(6).Controls(0)

                lnkItem.CssClass = "gridlink"
                menusLink.CssClass = "gridlink"
                pagesLink.CssClass = "gridlink"
                lnkEdit.CssClass = "gridlink"
                deleteLink.CssClass = "gridlinkalert"

                If hasPage Then
                    e.Item.Cells(4).Text = "<span style='color:green;'>yes</span>"
                Else
                    e.Item.Cells(4).Text = "<span style='color:maroon;'>no</span>"
                End If

                menusLink.Text = childMenus.ToString()
                If childMenus > 0 Then
                    menusLink.NavigateUrl = "menus.aspx?Id=" + menuId.ToString
                End If

                pagesLink.Text = childPages.ToString()
                'If childPages > 0 Then
                '  pagesLink.NavigateUrl = "../webpages/pageList.aspx?MenuId=" + menuId.ToString
                'End If

                If Not canEdit Then
                    lnkEdit.Enabled = False
                    lnkEdit.Text = "edit n/a"
                    lnkEdit.CssClass = "maroon"
                End If

                If childMenus > 0 Or childPages > 0 Or isPageCategoryDefault Or (Not canEdit) Then
                    deleteLink.Enabled = False
                    deleteLink.Text = "delete n/a"
                    deleteLink.CssClass = "maroon"
                Else
                    Dim menuText As String = DataBinder.Eval(e.Item.DataItem, "Text").ToString().ToUpper()
                    deleteLink.Attributes("onclick") = "javascript:return confirm('Are you sure you want to delete the item " + menuText + "?')"
                End If
            End If
        End Sub

        Private Sub dgrMenu_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgrMenu.ItemCommand

            Dim selectedId As Integer = dgrMenu.DataKeys(e.Item.ItemIndex)

            If e.CommandName = "Delete" Then

                Dim menu As New dt.StagingMenu(selectedId)
                Dim log As String = "DELETED: [" + menu.Text + "] from [" + GetParentName() + "]"
                Dim smeld As New dt.StagingMenuEditLogData
                smeld.CreateLog(log, Identity.UserId)

                Dim ml As New dt.StagingMenuData
                ml.Delete(selectedId)

            ElseIf e.CommandName = "Up" Then
                UpdateRanking(selectedId, True, True)
            ElseIf e.CommandName = "Down" Then
                UpdateRanking(selectedId, False, True)
            End If

            BindData()

        End Sub

        Private Sub UpdateRanking(ByVal menuId As Integer, ByVal moveUp As Boolean, ByVal addLog As Boolean)

            Dim dt As DataTable = New dt.StagingMenuData().GetMenuByParent(GetMenuId).Tables(0)
            Dim ids(dt.Rows.Count) As Integer

            If moveUp Then
                For pos As Integer = 0 To dt.Rows.Count - 1
                    ids(pos) = dt.Rows(pos)(0)
                    If (dt.Rows(pos)(0) = menuId And pos > 0) Then
                        ids(pos) = ids(pos - 1)
                        ids(pos - 1) = menuId
                    End If
                Next
            Else
                For pos As Integer = 0 To dt.Rows.Count - 1
                    ids(pos) = dt.Rows(pos)(0)
                    If (dt.Rows(pos)(0) = menuId And pos < dt.Rows.Count - 1) Then
                        ids(pos) = dt.Rows(pos + 1)(0)
                        ids(pos + 1) = menuId
                        pos = pos + 2
                    End If
                Next
            End If

            Dim smd As New dt.StagingMenuData
            Dim rank As Integer = 0
            For Each currid As Integer In ids
                smd.UpdateRank(currid, rank)
                rank = rank + 1
            Next

            If addLog Then
                Dim menu As New dt.StagingMenu(menuId)
                Dim log As String = "UPDATED ORDERING OF: [" + menu.Text + "] in [" + GetParentName() + "]"
                Dim smeld As New dt.StagingMenuEditLogData
                smeld.CreateLog(log, Identity.UserId)
            End If

        End Sub

        Private Sub btnPublish_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPublish.Click
            Dim smd As New dt.StagingMenuData
            smd.WriteStagingToMenu()

            Dim smeld As New dt.StagingMenuEditLogData
            smeld.ClearLogs()
            smeld.CreateLog("PUBLISHED MENU", Identity.UserId)

            BindData()
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 3
            End Get
        End Property

    End Class

End Namespace