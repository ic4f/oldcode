Imports System.Web.Mail
Imports dt = Foundation.Data
Imports bm = Foundation.BusinessMain

Public Class feedback
    Inherits BasePage

    Protected tbxName As TextBox
    Protected tbxEmail As TextBox
    Protected tbxComments As TextBox
    Protected WithEvents btnSave As Button
    Protected ltrConfirm As Literal
    Protected pnlFeedback As Panel

    Protected Overrides Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Page.IsValid Then
            Dim helper As New bm.WebHelper
            Dim mail As New MailMessage
            Dim name As String = helper.FilterUserInput(tbxName.Text.Trim, True)
            mail.To = bm.ConfigurationHelper.AdminEmail
            mail.From = helper.FilterUserInput(tbxEmail.Text.Trim, True)
            mail.Subject = "Feedback from UNI Foundation website"
            mail.Body = "Message from " + name + ": " + vbCrLf + helper.FilterUserInput(tbxComments.Text.Trim, True)
            SmtpMail.Send(mail)
            pnlFeedback.Visible = False
            ltrConfirm.Visible = True
            ltrConfirm.Text = name + ": your message has been sent, thank you!"

        End If
    End Sub

End Class
