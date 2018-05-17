Imports System.IO
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Namespace webpages

    Public Class pageInfo
        Inherits ba.BasePage

        Protected ltrTitle As Literal
        Protected ltrUrl As Literal
        Protected ltrId As Literal
        Protected ltrGroup As Literal
        Protected ltrMenu As Literal
        Protected WithEvents btnEdit As Button

        Protected pnlCanDelete As Panel
        Protected WithEvents btnDelete As Button

        Protected pnlAdminDelete As Panel
        Protected WithEvents btnAdminDelete As Button

        Protected pnlDependencies As Panel

        Protected pnlDependSystem As Panel

        Protected pnlDependDepartments As Panel
        Protected ltrDependDepartments As Literal
        Protected WithEvents rptDependDepartments As Repeater

        Protected pnlDependPages As Panel
        Protected ltrInboundPageLinks As Literal
        Protected WithEvents rptDependPages As Repeater

        Protected pnlDependModules As Panel
        Protected ltrInboundModuleLinks As Literal
        Protected WithEvents rptDependModules As Repeater

        Protected pnlDependMenus As Panel
        Protected ltrDependMenus As Literal
        Protected WithEvents rptDependMenus As Repeater

        Protected pnlLinks As Panel

        Protected pnlLinksToPages As Panel
        Protected ltrLinksToPages As Literal
        Protected WithEvents rptLinksToPages As Repeater

        Protected pnlLinksToFiles As Panel
        Protected ltrLinksToFiles As Literal
        Protected WithEvents rptLinksToFiles As Repeater

        Protected pnlLinksToImages As Panel
        Protected ltrLinksToImages As Literal
        Protected WithEvents rptLinksToImages As Repeater

        Public Const MAX_RECORDS As Integer = 3

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            btnAdminDelete.Attributes("onclick") = "javascript:return confirm('Are you absolutely sure you want to delete this custom page?')"
            btnDelete.Attributes("onclick") = "javascript:return confirm('Are you sure you want to delete this page?')"
            Dim mypage As New dt.Page(GetId())
            BindInfo(mypage)

            Dim pd As New dt.PageData
            If HasDependencies(mypage, pd) Then
                pnlDependencies.Visible = True
            Else
                pnlCanDelete.Visible = True
            End If

            If HasOutboundLinks(mypage, pd) Then
                pnlLinks.Visible = True
            End If
        End Sub

        Private Sub BindInfo(ByVal mypage As dt.Page)
            ltrTitle.Text = mypage.PageTitle
            ltrUrl.Text = "<a href='" + mypage.Url + "' target='_blank'>" + mypage.Url + "</a>"
            ltrId.Text = mypage.Id.ToString
            Dim mycat As New dt.PageCategory(mypage.PageCategoryId)
            ltrGroup.Text = mycat.Text
            Dim mymenu As New dt.StagingMenu(mypage.MenuId)
            ltrMenu.Text = mymenu.Text
        End Sub

        Private Function HasDependencies(ByVal mypage As dt.Page, ByVal pd As dt.PageData) As Boolean
            Dim depends As Boolean = False
            Dim systemLockOnly = False

            If Not mypage.CanDelete Then
                pnlDependSystem.Visible = True
                depends = True
                systemLockOnly = True
            End If

            Dim cd As New dt.CollegePageData
            Dim dsDepartments As DataSet = cd.GetDependentDepartments(cd.GetIdByPageId(mypage.Id))
            Dim countDepartments As Integer = Convert.ToInt32(dsDepartments.Tables(1).Rows(0)(0))
            If countDepartments > 0 Then
                If countDepartments = 1 Then
                    ltrDependDepartments.Text = countDepartments.ToString + " departments belong to this college"
                Else
                    ltrDependDepartments.Text = countDepartments.ToString + " department belongs to this college"
                End If
                If (MAX_RECORDS < countDepartments) Then
                    ltrDependDepartments.Text += " (displaying top " + MAX_RECORDS.ToString + ")"
                End If
                ltrDependDepartments.Text += ":"
                rptDependDepartments.DataSource = dsDepartments.Tables(0)
                rptDependDepartments.DataBind()
                pnlDependDepartments.Visible = True
                depends = True
                systemLockOnly = False
            End If

            Dim dsPages As DataSet = pd.GetInboundPageLinks(mypage.Id)
            Dim countPages As Integer = Convert.ToInt32(dsPages.Tables(1).Rows(0)(0))
            If countPages > 0 Then
                If countPages = 1 Then
                    ltrInboundPageLinks.Text = countPages.ToString + " page links to this page"
                Else
                    ltrInboundPageLinks.Text = countPages.ToString + " pages link to this page"
                End If
                If (MAX_RECORDS < countPages) Then
                    ltrInboundPageLinks.Text += " (displaying top " + MAX_RECORDS.ToString + ")"
                End If
                ltrInboundPageLinks.Text += ":"
                rptDependPages.DataSource = dsPages.Tables(0)
                rptDependPages.DataBind()
                pnlDependPages.Visible = True
                depends = True
                systemLockOnly = False
            End If

            Dim dsModules As DataSet = pd.GetInboundModuleLinks(mypage.Id)
            Dim countModules As Integer = Convert.ToInt32(dsModules.Tables(1).Rows(0)(0))
            If countModules > 0 Then
                If countModules = 1 Then
                    ltrInboundModuleLinks.Text = countModules.ToString + " sidebar module points to this page"
                Else
                    ltrInboundModuleLinks.Text = countModules.ToString + " sidebar modules point to this page"
                End If
                If (MAX_RECORDS < countModules) Then
                    ltrInboundModuleLinks.Text += " (displaying top " + MAX_RECORDS.ToString + ")"
                End If
                ltrInboundModuleLinks.Text += ":"
                rptDependModules.DataSource = dsModules.Tables(0)
                rptDependModules.DataBind()
                pnlDependModules.Visible = True
                depends = True
                systemLockOnly = False
            End If

            Dim dtMenus As DataTable = pd.GetDependentMenus(mypage.Id)
            Dim countMenus As Integer = dtMenus.Rows.Count
            If countMenus > 0 Then
                If countMenus = 1 Then
                    ltrDependMenus.Text = countMenus.ToString + " menus:"
                Else
                    ltrDependMenus.Text = countMenus.ToString + " menu:"
                End If
                rptDependMenus.DataSource = dtMenus
                rptDependMenus.DataBind()
                pnlDependMenus.Visible = True
                depends = True
                systemLockOnly = False
            End If

            If systemLockOnly And HasPermission(ba.PermissionCode.SystemSettings_CustomPages_ViewModify) And mypage.PageCategoryId = 1 And mypage.CanDelete = False Then
                pnlAdminDelete.Visible = True
            End If

            Return depends
        End Function

        Private Function HasOutboundLinks(ByVal mypage As dt.Page, ByVal pd As dt.PageData) As Boolean
            Dim hasLinks As Boolean = False

            Dim dsPages As DataSet = pd.GetOutboundPageLinks(mypage.Id)
            Dim countPages As Integer = Convert.ToInt32(dsPages.Tables(1).Rows(0)(0))
            If countPages > 0 Then
                If countPages = 1 Then
                    ltrLinksToPages.Text = countPages.ToString + " page"
                Else
                    ltrLinksToPages.Text = countPages.ToString + " pages"
                End If
                If (MAX_RECORDS < countPages) Then
                    ltrLinksToPages.Text += " (displaying top " + MAX_RECORDS.ToString + ")"
                End If
                ltrLinksToPages.Text += ":"
                rptLinksToPages.DataSource = dsPages.Tables(0)
                rptLinksToPages.DataBind()
                pnlLinksToPages.Visible = True
                hasLinks = True
            End If

            Dim dsFiles As DataSet = pd.GetOutboundFileLinks(mypage.Id)
            Dim countFiles As Integer = Convert.ToInt32(dsFiles.Tables(1).Rows(0)(0))
            If countFiles > 0 Then
                If countFiles = 1 Then
                    ltrLinksToFiles.Text = countFiles.ToString + " file"
                Else
                    ltrLinksToFiles.Text = countFiles.ToString + " files"
                End If
                If (MAX_RECORDS < countFiles) Then
                    ltrLinksToFiles.Text += " (displaying top " + MAX_RECORDS.ToString + ")"
                End If
                ltrLinksToFiles.Text += ":"
                rptLinksToFiles.DataSource = dsFiles.Tables(0)
                rptLinksToFiles.DataBind()
                pnlLinksToFiles.Visible = True
                hasLinks = True
            End If

            Dim dsImages As DataSet = pd.GetOutboundImageLinks(mypage.Id)
            Dim countImages As Integer = Convert.ToInt32(dsImages.Tables(1).Rows(0)(0))
            If countImages > 0 Then
                If countImages = 1 Then
                    ltrLinksToImages.Text = countImages.ToString + " image"
                Else
                    ltrLinksToImages.Text = countImages.ToString + " images"
                End If
                If (MAX_RECORDS < countImages) Then
                    ltrLinksToImages.Text += " (displaying top " + MAX_RECORDS.ToString + ")"
                End If
                ltrLinksToImages.Text += ":"
                rptLinksToImages.DataSource = dsImages.Tables(0)
                rptLinksToImages.DataBind()
                pnlLinksToImages.Visible = True
                hasLinks = True
            End If

            Return hasLinks
        End Function

        Private Function GetId() As Integer
            Return Convert.ToInt32(Request("Id"))
        End Function

        Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
            Dim mypage As New dt.Page(GetId())
            If mypage.PageCategoryId = 1 Then
                Response.Redirect("pageEdit.aspx?Id=" + GetId().ToString)
            ElseIf mypage.PageCategoryId = 2 Then
                Response.Redirect("newsEdit.aspx?Id=" + GetId().ToString)
            ElseIf mypage.PageCategoryId = 3 Then
                Response.Redirect("dstoryEdit.aspx?Id=" + GetId().ToString)
            ElseIf mypage.PageCategoryId = 4 Then
                Response.Redirect("../giving/collegeEdit.aspx?Id=" + GetId().ToString)
            ElseIf mypage.PageCategoryId = 5 Then
                Response.Redirect("../giving/departmentEdit.aspx?Id=" + GetId().ToString)
            ElseIf mypage.PageCategoryId = 6 Then
                Response.Redirect("../giving/programEdit.aspx?Id=" + GetId().ToString)
            End If
        End Sub

        Private Sub btnAdminDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdminDelete.Click
            Dim pd As New dt.PageData
            pd.Delete(GetId())
            Response.Redirect("../settings/customPageList.aspx")
        End Sub

        Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
            Dim pd As New dt.PageData
            Dim mypage As New dt.Page(GetId())
            If mypage.PageCategoryId = 1 Then
                pd.Delete(mypage.Id)
                Response.Redirect("pageList.aspx")
            ElseIf mypage.PageCategoryId = 2 Then
                Dim nd As New dt.NewsPageData
                Dim newsId As Integer = nd.GetIdByPageId(mypage.Id)
                File.Delete(Server.MapPath(ba.UrlHelper.GetNewsImageUrl(newsId)))
                nd.Delete(newsId)
                pd.Delete(mypage.Id)
                Response.Redirect("newsList.aspx")
            ElseIf mypage.PageCategoryId = 3 Then
                Dim dd As New dt.DStoryPageData
                Dim dstoryId As Integer = dd.GetIdByPageId(mypage.Id)
                File.Delete(Server.MapPath(ba.UrlHelper.GetDStoryImageUrl(dstoryId)))
                dd.Delete(dstoryId)
                pd.Delete(mypage.Id)
                Response.Redirect("dstoryList.aspx")
            ElseIf mypage.PageCategoryId = 4 Then
                Dim cd As New dt.CollegePageData
                cd.Delete(cd.GetIdByPageId(mypage.Id))
                pd.Delete(mypage.Id)
                Response.Redirect("../giving/collegeList.aspx")
            ElseIf mypage.PageCategoryId = 5 Then
                Dim dd As New dt.DepartmentPageData
                dd.Delete(dd.GetIdByPageId(mypage.Id))
                pd.Delete(mypage.Id)
                Response.Redirect("../giving/departmentList.aspx")
            ElseIf mypage.PageCategoryId = 6 Then
                Dim prd As New dt.ProgramPageData
                prd.Delete(prd.GetIdByPageId(mypage.Id))
                pd.Delete(mypage.Id)
                Response.Redirect("../giving/programList.aspx")
            End If
        End Sub

        Private Sub rptDependDepartments_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDependDepartments.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                Dim ltrDependDepartment As Literal = e.Item.FindControl("ltrDependDepartment")
                Dim pageId As String = DataBinder.Eval(e.Item.DataItem, "pageid")
                Dim title As String = DataBinder.Eval(e.Item.DataItem, "pagetitle")
                ltrDependDepartment.Text = "<a href='pageInfo.aspx?Id=" + pageId + "'>" + title + " [" + pageId + "]</a>"
            End If
        End Sub

        Private Sub rptDependPages_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDependPages.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                Dim ltrDependPage As Literal = e.Item.FindControl("ltrDependPage")
                Dim pageId As String = DataBinder.Eval(e.Item.DataItem, "id")
                Dim title As String = DataBinder.Eval(e.Item.DataItem, "pagetitle")
                ltrDependPage.Text = "<a href='pageInfo.aspx?Id=" + pageId + "'>" + title + " [" + pageId + "]</a>"
            End If
        End Sub

        Private Sub rptDependModules_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDependModules.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                Dim ltrDependModule As Literal = e.Item.FindControl("ltrDependModule")
                Dim moduleId As String = DataBinder.Eval(e.Item.DataItem, "id")
                Dim title As String = DataBinder.Eval(e.Item.DataItem, "admintitle")
                ltrDependModule.Text = "<a href='../contentlibrary/moduleEdit.aspx?Id=" + moduleId + "'>" + title + " [" + moduleId + "]</a>"
            End If
        End Sub

        Private Sub rptDependMenus_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDependMenus.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                Dim ltrDependMenu As Literal = e.Item.FindControl("ltrDependMenu")
                Dim menuId As String = DataBinder.Eval(e.Item.DataItem, "id")
                Dim text As String = DataBinder.Eval(e.Item.DataItem, "text")
                Dim type As String = DataBinder.Eval(e.Item.DataItem, "type")
                ltrDependMenu.Text = text + " [" + type + " / " + menuId + "]"
            End If
        End Sub

        Private Sub rptLinksToPages_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptLinksToPages.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                Dim ltrLinksToPage As Literal = e.Item.FindControl("ltrLinksToPage")
                Dim pageId As String = DataBinder.Eval(e.Item.DataItem, "id")
                Dim title As String = DataBinder.Eval(e.Item.DataItem, "pagetitle")
                ltrLinksToPage.Text = "<a href='pageInfo.aspx?Id=" + pageId + "'>" + title + " [" + pageId + "]</a>"
            End If
        End Sub

        Private Sub rptLinksToImages_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptLinksToImages.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                Dim ltrLinksToImage As Literal = e.Item.FindControl("ltrLinksToImage")
                Dim imageId As String = DataBinder.Eval(e.Item.DataItem, "id")
                Dim description As String = DataBinder.Eval(e.Item.DataItem, "description")
                Dim extension As String = DataBinder.Eval(e.Item.DataItem, "extension")
                Dim url As String = ba.UrlHelper.GetImageUrl(imageId, extension)
                Dim urlt As String = ba.UrlHelper.GetImageThumbnailUrl(imageId, extension)
                ltrLinksToImage.Text = "<a href='" + url + "' target='_blank'><img src='" + urlt + "' border='0'></a> " + description + " [" + imageId + "]"
            End If
        End Sub

        Private Sub rptLinksToFiles_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptLinksToFiles.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                Dim ltrLinksToFile As Literal = e.Item.FindControl("ltrLinksToFile")
                Dim fileId As String = DataBinder.Eval(e.Item.DataItem, "id")
                Dim description As String = DataBinder.Eval(e.Item.DataItem, "description")
                Dim extension As String = DataBinder.Eval(e.Item.DataItem, "extension")
                Dim url As String = ba.UrlHelper.GetFileUrl(fileId, extension)
                ltrLinksToFile.Text = "<img src='../_system/images/filetypes/" + extension.Substring(1) + ".gif'><a href='" + url + "'>" + description + " [" + fileId + "]</a>"
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 80
            End Get
        End Property

    End Class

End Namespace