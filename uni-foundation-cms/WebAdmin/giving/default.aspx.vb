Imports ba = Foundation.BusinessAdmin

Namespace giving

    Public Class _default
        Inherits ba.BasePage

        Protected pnlColleges As Panel
        Protected pnlPrograms As Panel

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                DisplayContent()
            End If
        End Sub

        Private Sub DisplayContent()
            pnlColleges.Visible = False
            pnlPrograms.Visible = False

            If HasPermission(ba.PermissionCode.Giving_Colleges_View) Or
              HasPermission(ba.PermissionCode.Giving_Colleges_ViewModify) Or
              HasPermission(ba.PermissionCode.Giving_Colleges_ViewModifyPublish) Then
                pnlColleges.Visible = True
            End If
            If HasPermission(ba.PermissionCode.Giving_Programs_View) Or
              HasPermission(ba.PermissionCode.Giving_Programs_ViewModify) Or
              HasPermission(ba.PermissionCode.Giving_Programs_ViewModifyPublish) Then
                pnlPrograms.Visible = True
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 21
            End Get
        End Property

    End Class

End Namespace
