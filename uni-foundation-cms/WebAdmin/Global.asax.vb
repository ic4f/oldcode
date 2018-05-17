Imports System.Web
Imports System.Web.SessionState
Imports System.Web.Security
Imports System.Security
Imports ba = Foundation.BusinessAdmin

Imports cr = Core

Public Class [Global]
    Inherits System.Web.HttpApplication

#Region " Component Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

#End Region

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        Dim infoCookieName As String = System.Configuration.ConfigurationSettings.AppSettings("InfoCookieName")
        Dim accessDeniedPage As String = System.Configuration.ConfigurationSettings.AppSettings("AccessDeniedPage")
        Dim authCookie As HttpCookie = Context.Request.Cookies(FormsAuthentication.FormsCookieName)
        Dim infoCookie As HttpCookie = Context.Request.Cookies(infoCookieName)

        If Not authCookie Is Nothing Then
            Try
                Dim authTicket As FormsAuthenticationTicket = FormsAuthentication.Decrypt(authCookie.Value)
                If Not authTicket Is Nothing Then
                    Dim newIdentity As New ba.SiteIdentity(authTicket, infoCookie)
                    Dim newPrincipal As New ba.SitePrincipal(newIdentity)
                    Context.User = newPrincipal
                Else
                    Response.Redirect(accessDeniedPage)
                End If
            Catch ex As Exception
                FormsAuthentication.SignOut()
            End Try
        End If
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class
