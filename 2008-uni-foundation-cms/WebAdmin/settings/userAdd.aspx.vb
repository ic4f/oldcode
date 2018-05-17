Imports System.Text
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Namespace settings

    Public Class userAdd
        Inherits ba.AddBasePage

        Protected tbxEmail As TextBox
        Protected tbxFirstName As TextBox
        Protected tbxMiddle As TextBox
        Protected tbxLastName As TextBox
        Protected valUnique As CustomValidator

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

        End Sub

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "user"
            End Get
        End Property

        Protected Overrides ReadOnly Property RedirectPage() As String
            Get
                Return "userList.aspx"
            End Get
        End Property

        Protected Overrides Function SaveItem() As Integer
            Dim password As String = GeneratePassword(8)
            Dim helper As New ba.WebHelper
            Dim login As String = helper.FilterUserInput(tbxEmail.Text, True)
            Dim firstName As String = helper.formatName(helper.FilterUserInput(tbxFirstName.Text, True))
            Dim middle As String = helper.formatName(helper.FilterUserInput(tbxMiddle.Text, True))
            Dim lastName As String = helper.formatName(helper.FilterUserInput(tbxLastName.Text, True))
            Dim userId As Integer = New dt.CmsUserData().Create(login, password, firstName, middle, lastName, Identity.UserId)

            If (userId < 0) Then
                valUnique.IsValid = False
                Return 0
            End If
            Return 1
        End Function

        Private Function GeneratePassword(ByVal length As Integer) As String
            Dim sb As New StringBuilder
            Dim rnd As New Random
            For i As Integer = 0 To length
                sb.Append(Convert.ToChar(Convert.ToInt32(26 * rnd.NextDouble() + 65)))
            Next
            Return sb.ToString
        End Function

        Protected Overrides Sub ResetFormFields()
            tbxFirstName.Text = ""
            tbxMiddle.Text = ""
            tbxLastName.Text = ""
            tbxEmail.Text = ""
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 59
            End Get
        End Property

    End Class

End Namespace
