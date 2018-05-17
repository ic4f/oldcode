Imports ba = Foundation.BusinessAdmin
Imports cr = Core
Imports dt = Foundation.Data

Namespace reports

    Public Class userlogs
        Inherits ba.BasePage

        Protected WithEvents ddlUser As DropDownList
        Protected WithEvents dgrLogs As cr.MyBaseGrid
        Protected pnlGrig As Panel

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            dgrLogs.BindingMethod = AddressOf BindData
            If Not Page.IsPostBack Then
                BindData()
                BindGrid()
            End If
        End Sub

        Private Sub BindData()
            ddlUser.DataSource = New dt.CmsUserData().GetAllUsersList
            ddlUser.DataValueField = "id"
            ddlUser.DataTextField = "displayedname"
            ddlUser.DataBind()
            ddlUser.AutoPostBack = True
        End Sub

        Private Sub BindGrid()
            Dim userId As Integer = Convert.ToInt32(ddlUser.SelectedValue)
            dgrLogs.Prefix = LinkPrefix
            Dim data As DataTable = New dt.CmsUserData().GetUserLogs(userId)
            dgrLogs.DataSource = data
            dgrLogs.DataBind()
            If (data.Rows.Count > 0) Then
                pnlGrig.Visible = True
            Else
                pnlGrig.Visible = False
            End If
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 114
            End Get
        End Property

        Private Sub ddlUser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUser.SelectedIndexChanged
            BindGrid()
        End Sub

    End Class

End Namespace
