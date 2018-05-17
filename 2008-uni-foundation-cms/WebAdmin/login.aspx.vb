Imports System.Security
Imports System.Web.Security
Imports System.Web.Mail
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Public Class login
    Inherits System.Web.UI.Page

    Protected ltrLayout1 As Literal
    Protected ltrLayout2 As Literal

    Protected pnlLogin As Panel
    Protected tbxLogin As TextBox
    Protected tbxPassword As TextBox
    Protected WithEvents btnLogin As Button
    Protected lblAuthFailure As Label
    Protected WithEvents lnkHelp As LinkButton
    Protected pnlHelp As Panel
    Protected lblHelp As Label
    Protected lblEmailNotFound As Label
    Protected WithEvents tbxHelp As TextBox
    Protected WithEvents btnHelp As Button
    Protected WithEvents lnkBack As LinkButton
    Protected valHelp As RequiredFieldValidator
    Protected lblPasswordSent As Label

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            displayLogin()
            Dim lHelper As New ba.LayoutHelper
            ltrLayout1.Text = lHelper.GetLayout1("Sign-in")
            ltrLayout2.Text = lHelper.GetLayout2()
        End If
    End Sub

    Private Sub displayLogin()
        pnlLogin.Visible = True
        pnlHelp.Visible = False
        lblAuthFailure.Visible = False
    End Sub

    Private Sub displayHelp()
        pnlLogin.Visible = False
        pnlHelp.Visible = True
        lblEmailNotFound.Visible = False
    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Dim ud As New dt.CmsUserData
        Dim cmsuserId As Integer = ud.ValidateUser(tbxLogin.Text, tbxPassword.Text)
        If (cmsuserId > 0) Then
            Dim newUser As New dt.CmsUser(cmsuserId)

            Dim permissionList As ArrayList = ud.GetPermissionCodesByUser(cmsuserId)
            Dim permissions As String = String.Empty
            Dim i As Integer
            For i = 0 To (permissionList.Count - 1)
                permissions += permissionList.Item(i).ToString() + "|"
            Next
            If permissions.Length > 0 Then
                permissions = permissions.Substring(0, permissions.Length - 1) 'gets rid of last delimeter
            End If

            Dim authTicket As FormsAuthenticationTicket = New FormsAuthenticationTicket(
               1,
               CType(newUser.UserId, String),
               DateTime.Now,
               DateTime.Now.AddMinutes(1),
               False,
               permissions)   'version + uerid + creation + expiration + persistent + user roles

            Dim encryptedTicket As String = FormsAuthentication.Encrypt(authTicket)
            Dim authCookie As HttpCookie = New HttpCookie(
               FormsAuthentication.FormsCookieName, encryptedTicket)

            Dim infoCookieName As String = System.Configuration.ConfigurationSettings.AppSettings("InfoCookieName")
            Dim infoCookie As HttpCookie = New HttpCookie(infoCookieName)
            infoCookie.Values("Login") = newUser.Login
            infoCookie.Values("FirstName") = newUser.FirstName
            infoCookie.Values("LastName") = newUser.LastName

            Response.Cookies.Add(authCookie)
            Response.Cookies.Add(infoCookie)

            ud.LogUserSignIn(newUser.UserId)

            Response.Redirect(FormsAuthentication.GetRedirectUrl(tbxLogin.Text, False))
        Else
            lblAuthFailure.Text = "Login failed for " + tbxLogin.Text
            lblAuthFailure.Visible = True
            tbxLogin.Text = String.Empty
        End If
    End Sub

    Private Sub ProcessHelp()
        Dim cud As New dt.CmsUserData
        Dim userId As Integer = cud.GetUserIdByLogin(tbxHelp.Text.Trim())
        If userId > 0 Then
            Dim objUser As New dt.CmsUser(cud.GetIdByUserId(userId))
            Dim mail As New MailMessage
            mail.To = objUser.Login
            mail.From = ba.ConfigurationHelper.AutoEmail
            mail.Subject = "Your password for the UNI Foundation Content Management System"
            mail.Body = "Dear " & objUser.FirstName & ", " & vbCrLf & vbCrLf
            mail.Body += "Your password for online access to the UNI Foundation Content Management System is " & objUser.Password & vbCrLf & vbCrLf
            SmtpMail.Send(mail)
            lblHelp.Text = "<img src='_system/images/layout/new.gif'> Your password has been sent to you."
            tbxHelp.Text = ""
        Else
            lblHelp.Text = "This email is not part of the system."
        End If
        lblHelp.Visible = True
    End Sub

    Private Sub lnkHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkHelp.Click
        displayHelp()
    End Sub

    Private Sub lnkBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBack.Click
        displayLogin()
    End Sub

    Private Sub btnHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHelp.Click
        ProcessHelp()
    End Sub

    Private Sub tbxHelp_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbxHelp.TextChanged
        ProcessHelp()
    End Sub



End Class