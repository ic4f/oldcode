Imports System.Text
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Namespace contentlibrary

    Public Class moduleView
        Inherits ba.BasePage

        Protected ltrModule As Literal

        Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Page.IsPostBack Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            Dim m As New dt.Module(GetId())
            Dim sb As New StringBuilder

            Dim link As String
            If m.PageId > 0 Then
                Dim p As New dt.Page(m.PageId)
                link = p.Url
            Else
                link = m.ExternalLink
            End If

            sb.AppendFormat("<a href='{0}' style='text-decoration: none;'>", link)

            If m.ImageExtension <> "" Then
                sb.AppendFormat("<img src='{0}' border='0'>", ba.UrlHelper.GetModuleImageUrl(m.Id, m.ImageExtension))
            End If

            If m.Title <> "" Or m.Body <> "" Then
                If m.ImageExtension = "" Then
                    sb.Append("<div class='sidebarmodule-textnoimagediv'>")
                Else
                    sb.Append("<div class='sidebarmodule-textdiv'>")
                End If
                If m.Title <> "" Then
                    If m.Body = "" Then
                        sb.AppendFormat("<p class='sidebarmodule-title' style='text-align:center;margin-top:5px;'>{0}</p>", m.Title)
                    Else
                        sb.AppendFormat("<p class='sidebarmodule-title'>{0}</p>", m.Title)
                    End If
                End If
                If m.Body <> "" Then
                    sb.AppendFormat("<p class='sidebarmodule-body'>{0}</p>", m.Body)
                End If
                sb.Append("</div>")
            End If

            sb.Append("</a>")
            ltrModule.Text = sb.ToString()
        End Sub

        Private Function GetId() As Integer
            Return Convert.ToInt32(Request("Id"))
        End Function

        Protected Overrides ReadOnly Property PageId() As Integer
            Get
                Return 94
            End Get
        End Property

    End Class

End Namespace