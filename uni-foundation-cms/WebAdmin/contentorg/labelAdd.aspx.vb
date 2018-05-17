Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data
Imports c = Core

Namespace contentorg

    Public Class labelAdd
        Inherits ba.AddBasePage

        Protected tbxText As TextBox
        Protected WithEvents valUnique As CustomValidator

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        End Sub

        Protected Overrides ReadOnly Property ItemName() As String
            Get
                Return "Label"
            End Get
        End Property

        Protected Overrides Function SaveItem() As Integer
            Dim helper As New ba.WebHelper
            Dim text As String = helper.FilterUserInput(tbxText.Text, True)
            Dim labelId As Integer = New dt.ContentLabelData().Create(text, Identity.UserId)
            If (labelId < 0) Then
                valUnique.IsValid = False
                Return 0
            End If
            Return 1
        End Function

        Protected Overrides Sub ResetFormFields()
            tbxText.Text = ""
        End Sub

        Protected Overrides ReadOnly Property RedirectPage() As String
            Get
                Return "labelList.aspx"
            End Get

        End Property
        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 54
            End Get
        End Property

    End Class

End Namespace