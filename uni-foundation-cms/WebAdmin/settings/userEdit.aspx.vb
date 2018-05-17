Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Namespace settings

    Public Class userEdit
        Inherits ba.EditBasePage

        Protected tbxEmail As TextBox
        Protected tbxPassword As TextBox
        Protected tbxFirstName As TextBox
        Protected tbxMiddle As TextBox
        Protected tbxLastName As TextBox
        Protected tbxDisplayedName As TextBox
        Protected valUnique As CustomValidator
        Protected ltrModified As Literal
        Protected ltrModifiedBy As Literal

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            Dim user As New dt.CmsUser(GetId())
            tbxEmail.Text = user.Login
            tbxFirstName.Text = user.FirstName
            tbxMiddle.Text = user.Middle
            tbxLastName.Text = user.LastName
            tbxDisplayedName.Text = user.DisplayedName
            ltrModified.Text = user.Modified
            ltrModifiedBy.Text = user.ModifiedByName
        End Sub

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "user"
            End Get
        End Property

        Protected Overrides Function SaveItem() As Integer
            Dim helper As New ba.WebHelper

            Dim login As String = helper.FilterUserInput(tbxEmail.Text, True)
            Dim password As String = helper.FilterUserInput(tbxPassword.Text, True)
            Dim firstName As String = helper.formatName(helper.FilterUserInput(tbxFirstName.Text, True))
            Dim middle As String = helper.formatName(helper.FilterUserInput(tbxMiddle.Text, True))
            Dim lastName As String = helper.formatName(helper.FilterUserInput(tbxLastName.Text, True))
            Dim displayedName As String = helper.FilterUserInput(tbxDisplayedName.Text, True)

            Dim user As New dt.CmsUser(GetId())

            user.Login = login
            user.FirstName = firstName
            user.Middle = middle
            user.LastName = lastName
            user.DisplayedName = displayedName
            user.ModifiedBy = Identity.UserId
            If password <> "" Then
                user.Password = password
            End If

            If (user.Update() < 0) Then
                valUnique.IsValid = False
                Return 0
            End If
            Return 1

        End Function

        Protected Overrides ReadOnly Property RedirectPage() As String
            Get
                Return "userList.aspx"
            End Get
        End Property

        Protected Overrides Sub ResetFormFields()
            BindData()
        End Sub

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 61
            End Get
        End Property

    End Class

End Namespace
