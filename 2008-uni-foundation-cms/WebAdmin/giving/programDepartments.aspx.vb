Imports ba = Foundation.BusinessAdmin
Imports c = Core
Imports dt = Foundation.Data

Namespace giving

    Public Class programDepartments
        Inherits ba.BasePage

        Protected cblDepartments As c.DataCheckboxList
        Protected WithEvents btnSave As Button
        Protected WithEvents btnCancel As Button

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            Dim dtDepartments As DataTable = New dt.DepartmentPageData().GetAllDepartmentsByProgram(GetProgramPageId())
            cblDepartments.DataSource = dtDepartments
            cblDepartments.DataTextField = "department"
            cblDepartments.DataValueField = "id"
            cblDepartments.DataCheckField = "selected"
            cblDepartments.DataBind()
        End Sub

        Private Function GetProgramPageId() As Integer
            Return New dt.ProgramPageData().GetIdByPageId(Convert.ToInt32(Request("Id")))
        End Function

        Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
            Dim departments As ArrayList = cblDepartments.GetSelectedValues()
            Dim dtLink As New dt.ProgramDepartmentLink
            Dim programId As Integer = GetProgramPageId()
            dtLink.DeleteByProgram(programId)
            For Each dptId As Integer In departments
                dtLink.Create(programId, dptId)
            Next
            Response.Redirect("programList.aspx")
        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Response.Redirect("programList.aspx")
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 105
            End Get
        End Property

    End Class

End Namespace
