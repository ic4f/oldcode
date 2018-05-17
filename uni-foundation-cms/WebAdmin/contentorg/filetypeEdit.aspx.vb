Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data
Imports c = Core

Namespace contentorg

    Public Class filetypeEdit
        Inherits ba.EditBasePage

        Protected tbxDescription As TextBox

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            Dim dft As New dt.FileType(GetExtension())
            tbxDescription.Text = dft.Description
        End Sub

        Private Function GetExtension() As String
            Return Request("Extension")
        End Function

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "File type"
            End Get
        End Property

        Protected Overrides Function SaveItem() As Integer
            Dim dft As New dt.FileType(GetExtension())
            dft.Description = tbxDescription.Text.Trim
            Return dft.Update()
        End Function

        Protected Overrides Sub ResetFormFields()
            BindData()
        End Sub

        Protected Overrides ReadOnly Property RedirectPage() As String
            Get
                Return "filetypeList.aspx"
            End Get
        End Property


        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 52
            End Get
        End Property

    End Class

End Namespace