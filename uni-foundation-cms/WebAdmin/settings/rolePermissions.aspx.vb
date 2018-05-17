Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Namespace settings

    Public Class rolePermissions
        Inherits ba.BasePage

        Protected WithEvents rptTopCats As Repeater
        Protected WithEvents rptCats As Repeater
        Protected pnlPermissions As Panel
        Protected ltrPermissionTitle As Literal
        Protected pnlConfirm As Panel
        Protected WithEvents btnSave As Button
        Protected WithEvents btnCancel As Button

        Private catId As Integer
        Private roleId As Integer
        Private permissionData As ba.PermissionData
        Private roleData As dt.RoleData
        Private arrExistingPermIds As ArrayList

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            arrExistingPermIds = roleData.GetPermissions(roleId)

            rptTopCats.DataSource = permissionData.TopCategories
            rptTopCats.DataBind()

            If (catId > 0) Then
                Dim role As New dt.Role(roleId)
                Dim cat As ba.PermissionCategory = permissionData.GetCategory(catId)
                pnlPermissions.Visible = True
                ltrPermissionTitle.Text = "Select <b>" + cat.Name + "</b> permissions for the <b>" + role.Name + "</b> role:"
                rptCats.DataSource = permissionData.GetCategories(catId)
                rptCats.DataBind()
            Else
                pnlPermissions.Visible = False
            End If
        End Sub

        Private Sub rptTopCats_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptTopCats.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                Dim cat As ba.PermissionCategory = e.Item.DataItem
                Dim lnkTopCat As HyperLink = e.Item.FindControl("lnkTopCat")
                lnkTopCat.Text = cat.Name
                lnkTopCat.NavigateUrl = "rolePermissions.aspx?RoleId=" + roleId.ToString() + "&CatId=" + cat.Id.ToString()
                If (cat.Id = catId) Then
                    lnkTopCat.Style.Add("font-weight", "bold")
                End If
            End If
        End Sub

        Private Sub rptCats_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptCats.ItemDataBound
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                Dim cat As ba.PermissionCategory = e.Item.DataItem
                Dim lblCat As Label = e.Item.FindControl("lblCat")
                lblCat.Text = cat.Name
                Dim arrPerms As ArrayList = permissionData.GetPermissions(cat.Id)
                Dim pnlPerms As Panel = e.Item.FindControl("pnlPerms")
                Dim ltrNone As New LiteralControl
                pnlPerms.Controls.Add(ltrNone)
                Dim selected As String = ""
                Dim exists As Boolean = False

                Dim perm As ba.Permission
                For Each perm In arrPerms
                    If (arrExistingPermIds.Contains(perm.Id)) Then
                        selected = "checked"
                        exists = True
                    Else
                        selected = ""
                    End If

                    Dim radio As String = "<input type='radio' name='radPerm_" + cat.Id.ToString() + "' value='" + perm.Id.ToString() + "' " + selected + ">"
                    pnlPerms.Controls.Add(New LiteralControl(radio + perm.Name + "<br>"))
                Next

                If (Not exists) Then
                    selected = "checked"
                End If

                ltrNone.Text = "<input type='radio' name='radPerm_" + cat.Id.ToString() + "' value='0' " + selected + ">none<br>"
            End If
        End Sub

        Private Function getRoleId() As Integer
            If (Request("RoleId") <> "") Then
                Return Convert.ToInt32(Request("RoleId"))
            End If
            Return -1
        End Function

        Private Function getCatId() As Integer
            If (Request("CatId") <> "") Then
                Return Convert.ToInt32(Request("CatId"))
            End If
            Return -1
        End Function

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 68
            End Get
        End Property

        Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
            Dim arrCats As ArrayList = permissionData.GetCategories(catId)
            Dim cat As ba.PermissionCategory
            For Each cat In arrCats
                Dim arrPerms As ArrayList = permissionData.GetPermissions(cat.Id)
                Dim perm As ba.Permission
                For Each perm In arrPerms
                    roleData.DeletePermission(roleId, perm.Id)
                Next

                Dim permId As Integer = Convert.ToInt32(Page.Request("radPerm_" + cat.Id.ToString))
                If (permId > 0) Then
                    roleData.AddPermission(roleId, permId)
                End If
            Next
            pnlConfirm.Visible = True
            BindData()
        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            pnlConfirm.Visible = False
            BindData()
        End Sub

        Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
            roleId = getRoleId()
            catId = getCatId()
            permissionData = New ba.PermissionData
            roleData = New dt.RoleData
            arrExistingPermIds = roleData.GetPermissions(roleId)
            MyBase.OnInit(e)
        End Sub


    End Class

End Namespace
