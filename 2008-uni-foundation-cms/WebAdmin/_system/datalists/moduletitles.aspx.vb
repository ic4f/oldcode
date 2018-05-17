Imports System.Text
Imports dt = Foundation.Data
Imports c = Core

Public Class moduletitles
    Inherits System.Web.UI.Page

    Protected ltrTitles As Literal

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            BindData()
        End If
        MyBase.OnLoad(e)
    End Sub

    Private Sub BindData()
        Dim sb As New StringBuilder
        Dim dt As DataTable = New dt.ModuleData().GetTitles()
        For Each dr As DataRow In dt.Rows
            sb.AppendFormat("{0}<br>", dr(0))
        Next
        ltrTitles.Text = sb.ToString
    End Sub

End Class