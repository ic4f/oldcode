Imports System.Collections
Imports System.Text
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Public Class _default
    Inherits ba.BasePage

    Protected pnlLoad As Panel
    Protected WithEvents btnLoad As Button
    Protected ltrUserName As Literal

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            CheckLoaded()
            BindData()
        End If
    End Sub

    Private Sub CheckLoaded()
        If Not (New dt.PageData).SystemIsLoaded Then
            pnlLoad.Visible = True
        End If
    End Sub

    Private Sub BindData()
        ltrUserName.Text = Identity.Name
    End Sub

    Protected Overrides ReadOnly Property PageId() As Integer
        Get
            Return 1
        End Get
    End Property

    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Dim sl As New ba.SystemLoader(Identity.UserId)
        sl.Load()
        Response.Redirect("default.aspx")
    End Sub

End Class
