Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data
Imports c = Core

Namespace contentlibrary

    Public Class quoteEdit
        Inherits ba.EditBasePage

        Protected tbxAuthor As TextBox
        Protected tbxQuote As TextBox
        Protected tbxComment As TextBox

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            Dim dq As New dt.Quote(GetId())
            tbxAuthor.Text = dq.Author
            tbxQuote.Text = dq.Text
            tbxComment.Text = dq.AdminComment
        End Sub

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "Quote"
            End Get
        End Property

        Protected Overrides Function SaveItem() As Integer
            Dim dq As New dt.Quote(GetId())
            dq.Author = tbxAuthor.Text.Trim
            dq.Text = tbxQuote.Text.Trim
            dq.AdminComment = tbxComment.Text.Trim
            Return dq.Update
        End Function

        Protected Overrides Sub ResetFormFields()
            BindData()
        End Sub

        Protected Overrides ReadOnly Property RedirectPage() As String
            Get
                Return "quoteList.aspx"
            End Get
        End Property

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 41
            End Get
        End Property

    End Class

End Namespace