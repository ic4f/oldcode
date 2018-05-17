Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data
Imports c = Core

Namespace contentorg

    Public Class tagEdit
        Inherits ba.EditBasePage

        Protected tbxText As TextBox
        Protected WithEvents valUnique As CustomValidator

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            Dim dl As New dt.Tag(GetId())
            tbxText.Text = dl.Text
        End Sub

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "Tag"
            End Get
        End Property

        Protected Overrides Function SaveItem() As Integer
            Dim dt As New dt.Tag(GetId())
            dt.Text = tbxText.Text.Trim
            Dim result As Integer = dt.Update
            If (result < 0) Then
                valUnique.IsValid = False
            End If
            Return result
        End Function

        Protected Overrides Sub ResetFormFields()
            BindData()
        End Sub

        Protected Overrides ReadOnly Property RedirectPage() As String
            Get
                Return "tagList.aspx"
            End Get
        End Property

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 79
            End Get
        End Property

    End Class

End Namespace