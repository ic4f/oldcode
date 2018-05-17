Imports ba = Foundation.BusinessAdmin
Imports c = Core
Imports dt = Foundation.Data

Namespace settings

    Public Class userRoles
        Inherits ba.BasePage

        Protected cblRoles As c.DataCheckboxList
        Protected WithEvents btnSave As Button
        Protected WithEvents btnCancel As Button

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            Dim dtRoles As DataTable = New dt.RoleData().GetAllRolesByUser(GetUserId())
            cblRoles.DataSource = dtRoles
            cblRoles.DataTextField = "Name"
            cblRoles.DataValueField = "Id"
            cblRoles.DataCheckField = "Selected"
            cblRoles.DataBind()
        End Sub

        Private Function GetUserId() As Integer
            Return Convert.ToInt32(Request("CmsUserId"))
        End Function

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 62
            End Get
        End Property

        Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
            Dim roles As ArrayList = cblRoles.GetSelectedValues()
            Dim dtLink As New dt.CmsUserRoleLink
            Dim cmsuserId As Integer = GetUserId()
            dtLink.DeleteByUser(cmsuserId)
            For Each roleId As Integer In roles
                dtLink.Create(cmsuserId, roleId)
            Next
            Response.Redirect("userList.aspx")
        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Response.Redirect("userList.aspx")
        End Sub

    End Class

End Namespace
