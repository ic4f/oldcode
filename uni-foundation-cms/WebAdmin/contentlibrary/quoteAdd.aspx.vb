Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data
Imports c = Core

Namespace contentlibrary

    Public Class quoteAdd
        Inherits ba.AddBasePage

        Protected tbxAuthor As TextBox
        Protected tbxQuote As TextBox
        Protected tbxComment As TextBox

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        End Sub

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "Quote"
            End Get
        End Property

        Protected Overrides Function SaveItem() As Integer
            Dim helper As New ba.WebHelper
            Dim author As String = helper.FilterUserInput(tbxAuthor.Text, True)
            Dim text As String = helper.FilterUserInput(tbxQuote.Text, True)
            Dim comment As String = helper.FilterUserInput(tbxComment.Text, True)
            Dim quoteId As Integer = New dt.QuoteData().Create(text, author, comment, Identity.UserId)
            If (quoteId < 0) Then
                Return 0
            End If
            Return 1
        End Function

        Protected Overrides Sub ResetFormFields()
            tbxAuthor.Text = ""
            tbxQuote.Text = ""
            tbxComment.Text = ""
        End Sub

        Protected Overrides ReadOnly Property RedirectPage() As String
            Get
                Return "quoteList.aspx"
            End Get
        End Property

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 39
            End Get
        End Property

    End Class

End Namespace