Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data
Imports c = Core

Namespace contentorg

    Public Class labelEdit
        Inherits ba.EditBasePage

        Protected tbxText As TextBox
        Protected WithEvents valUnique As CustomValidator

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            Dim dl As New dt.ContentLabel(GetId())
            tbxText.Text = dl.Text
        End Sub

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "Label"
            End Get
        End Property

        Protected Overrides Function SaveItem() As Integer
            Dim dl As New dt.ContentLabel(GetId())
            dl.Text = tbxText.Text.Trim
            Dim result As Integer = dl.Update
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
                Return "labelList.aspx"
            End Get
        End Property

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 56
            End Get
        End Property

    End Class

End Namespace