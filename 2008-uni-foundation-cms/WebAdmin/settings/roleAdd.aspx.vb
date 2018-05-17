Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Namespace settings

    Public Class roleAdd
        Inherits ba.AddBasePage

        Protected tbxRole As TextBox
        Protected valUnique As CustomValidator

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

        End Sub

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "role"
            End Get
        End Property

        Protected Overrides ReadOnly Property RedirectPage() As String
            Get
                Return "roleList.aspx"
            End Get
        End Property

        Protected Overrides Function SaveItem() As Integer
            Dim helper As New ba.WebHelper
            Dim name As String = helper.FilterUserInput(tbxRole.Text, True)
            Dim roleId As Integer = New dt.RoleData().Create(name, Identity.UserId)
            If (roleId < 0) Then
                valUnique.IsValid = False
                Return 0
            End If
            Return 1
        End Function

        Protected Overrides Sub ResetFormFields()
            tbxRole.Text = ""
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 64
            End Get
        End Property

    End Class

End Namespace
