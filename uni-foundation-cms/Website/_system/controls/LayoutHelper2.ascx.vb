Imports System.Text
Imports dt = Foundation.Data
Imports bm = Foundation.BusinessMain

Public Class LayoutHelper2
    Inherits System.Web.UI.UserControl

    Protected ltrRightBar As Literal

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Friend Sub LoadContent()
        LoadContent(-1)
    End Sub

    Friend Sub LoadContent(ByVal pageId As Integer)
        If pageId > 0 Then

            Dim sb As New StringBuilder
            Dim dtModules As DataTable = New dt.ModuleData().GetForDisplay(pageId)
            Dim id As Integer
            Dim imageextension As String
            Dim title As String
            Dim body As String
            Dim link As String
            For Each dr As DataRow In dtModules.Rows
                id = Convert.ToInt32(dr(0).ToString())
                imageextension = dr(1).ToString()
                title = dr(2).ToString()
                body = dr(3).ToString()
                link = dr(5).ToString()

                sb.AppendFormat("<div class='module'><a href='{0}' style='text-decoration: none;'>", link)
                If imageextension <> "" Then
                    sb.AppendFormat("<img src='{0}' border='0'>", bm.UrlHelper.GetModuleImageUrl(id, imageextension))
                End If

                If title <> "" Or body <> "" Then
                    If imageextension = "" Then
                        sb.Append("<div class='sidebarmodule-textnoimagediv'>")
                    Else
                        sb.Append("<div class='sidebarmodule-textdiv'>")
                    End If
                    If title <> "" Then
                        If body = "" Then
                            sb.AppendFormat("<p class='sidebarmodule-title' style='text-align:center;margin-top:5px;'>{0}</p>", title)
                        Else
                            sb.AppendFormat("<p class='sidebarmodule-title'>{0}</p>", title)
                        End If
                    End If
                    If body <> "" Then
                        sb.AppendFormat("<p class='sidebarmodule-body'>{0}</p>", body)
                    End If
                    sb.Append("</div>")
                End If

                sb.Append("</a></div>")
            Next
            ltrRightBar.Text = sb.ToString()

        End If
    End Sub

End Class
