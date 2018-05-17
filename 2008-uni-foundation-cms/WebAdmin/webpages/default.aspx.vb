Imports ba = Foundation.BusinessAdmin

Namespace webpages

    Public Class _default
        Inherits ba.BasePage

        Protected pnlPages As Panel
        Protected pnlNews As Panel
        Protected pnlDstories As Panel
        Protected pnlGiving As Panel
        Protected pnlAll As Panel

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                DisplayContent()
            End If
        End Sub

        Private Sub DisplayContent()
            pnlPages.Visible = False
            pnlNews.Visible = False
            pnlDstories.Visible = False
            Dim displayAll As Boolean = True

            If HasPermission(ba.PermissionCode.WebPages_Pages_View) Or
            HasPermission(ba.PermissionCode.WebPages_Pages_ViewModify) Or
            HasPermission(ba.PermissionCode.WebPages_Pages_ViewModifyPublish) Then
                pnlPages.Visible = True
            Else
                displayAll = False
            End If
            If HasPermission(ba.PermissionCode.WebPages_News_View) Or
            HasPermission(ba.PermissionCode.WebPages_News_ViewModify) Or
            HasPermission(ba.PermissionCode.WebPages_News_ViewModifyPublish) Then
                pnlNews.Visible = True
            Else
                displayAll = False
            End If
            If HasPermission(ba.PermissionCode.WebPages_DStories_View) Or
            HasPermission(ba.PermissionCode.WebPages_DStories_ViewModify) Or
            HasPermission(ba.PermissionCode.WebPages_DStories_ViewModifyPublish) Then
                pnlDstories.Visible = True
            Else
                displayAll = False
            End If
            If HasPermission(ba.PermissionCode.Giving_Colleges_View) Or
              HasPermission(ba.PermissionCode.Giving_Colleges_ViewModify) Or
              HasPermission(ba.PermissionCode.Giving_Colleges_ViewModifyPublish) Then
                pnlGiving.Visible = True
            Else
                displayAll = False
            End If
            If displayAll Then
                pnlAll.Visible = True
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 2
            End Get
        End Property

    End Class

End Namespace