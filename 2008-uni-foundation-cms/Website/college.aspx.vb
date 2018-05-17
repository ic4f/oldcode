Imports bm = Foundation.BusinessMain
Imports dt = Foundation.Data

Public Class college
    Inherits BasePage

    Protected WithEvents rptPrograms As Repeater

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub BindData()
        Dim collegeId As Integer = New dt.CollegePageData().GetIdByPageId(PageRecord.Id)
        rptPrograms.DataSource = New dt.ProgramPageData().GetPublishedProgramPagesByCollege(collegeId)
        rptPrograms.DataBind()
    End Sub

    Private Sub rptPrograms_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptPrograms.ItemDataBound
        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
            Dim lnkProgram As HyperLink = e.Item.FindControl("lnkProgram")
            lnkProgram.Text = DataBinder.Eval(e.Item.DataItem, "contenttitle").ToString()
            lnkProgram.NavigateUrl = DataBinder.Eval(e.Item.DataItem, "url").ToString()
        End If
    End Sub

End Class
