Imports System.Web.Security
Imports ba = Foundation.BusinessAdmin
Imports dt = Foundation.Data

Public Class signout
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If (Context.User.Identity.IsAuthenticated) Then
            Dim ud As New dt.CmsUserData

            Dim principal As ba.SitePrincipal = Context.User
            Dim identity As ba.SiteIdentity = principal.Identity
            ud.LogUserSignOut(identity.UserId)

            FormsAuthentication.SignOut()
        End If
        Response.Redirect(ba.ConfigurationHelper.LoginPage)
    End Sub

End Class
