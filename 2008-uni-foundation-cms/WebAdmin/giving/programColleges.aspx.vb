Imports ba = Foundation.BusinessAdmin
Imports c = Core
Imports dt = Foundation.Data

Namespace giving

    Public Class programColleges
        Inherits ba.BasePage

        Protected cblColleges As c.DataCheckboxList
        Protected WithEvents btnSave As Button
        Protected WithEvents btnCancel As Button

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            Dim dtColleges As DataTable = New dt.CollegePageData().GetAllCollegesByProgram(GetProgramPageId())
            cblColleges.DataSource = dtColleges
            cblColleges.DataTextField = "college"
            cblColleges.DataValueField = "id"
            cblColleges.DataCheckField = "selected"
            cblColleges.DataBind()
        End Sub

        Private Function GetProgramPageId() As Integer
            Return New dt.ProgramPageData().GetIdByPageId(Convert.ToInt32(Request("Id")))
        End Function

        Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
            Dim colleges As ArrayList = cblColleges.GetSelectedValues()
            Dim dtLink As New dt.ProgramCollegeLink
            Dim programId As Integer = GetProgramPageId()
            dtLink.DeleteByProgram(programId)
            For Each collegeId As Integer In colleges
                dtLink.Create(programId, collegeId)
            Next
            Response.Redirect("programList.aspx")
        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Response.Redirect("programList.aspx")
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 104
            End Get
        End Property

    End Class

End Namespace
