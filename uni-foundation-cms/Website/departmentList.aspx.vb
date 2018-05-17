Imports bm = Foundation.BusinessMain
Imports dt = Foundation.Data

Public Class departmentList
    Inherits BasePage

    Protected WithEvents rptDepartments As Repeater

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub BindData()
        rptDepartments.DataSource = New dt.DepartmentPageData().GetPublishedDepartmentPages
        rptDepartments.DataBind()
    End Sub

    Private Sub rptDepartments_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDepartments.ItemDataBound
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim lnkDepartment As HyperLink = e.Item.FindControl("lnkDepartment")
            lnkDepartment.Text = DataBinder.Eval(e.Item.DataItem, "contenttitle").ToString()
            lnkDepartment.NavigateUrl = DataBinder.Eval(e.Item.DataItem, "url").ToString()
        End If
    End Sub

End Class
