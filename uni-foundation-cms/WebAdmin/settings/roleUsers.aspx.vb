Imports ba = Foundation.BusinessAdmin
Imports c = Core
Imports dt = Foundation.Data

Namespace settings

    Public Class roleUsers
        Inherits ba.BasePage

        Protected cblUsers As c.DataCheckboxList
        Protected WithEvents btnSave As Button
        Protected WithEvents btnCancel As Button

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            Dim dtUsers As DataTable = New dt.CmsUserData().GetAllUsersByRole(GetRoleId())
            cblUsers.DataSource = dtUsers
            cblUsers.DataTextField = "Displayed"
            cblUsers.DataValueField = "Id"
            cblUsers.DataCheckField = "Selected"
            cblUsers.DataBind()
        End Sub

        Private Function GetRoleId() As Integer
            Return Convert.ToInt32(Request("RoleId"))
        End Function

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 67
            End Get
        End Property

        Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
            Dim users As ArrayList = cblUsers.GetSelectedValues()
            Dim dtLink As New dt.CmsUserRoleLink
            Dim roleId As Integer = GetRoleId()
            dtLink.DeleteByRole(roleId)
            For Each userId As Integer In users
                dtLink.Create(userId, roleId)
            Next
            Response.Redirect("roleList.aspx")
        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Response.Redirect("roleList.aspx")
        End Sub

    End Class

End Namespace
