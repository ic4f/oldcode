using System;
using System.Collections;
using System.ComponentModel;
using System.Security;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using b = Ei.Business;
using cc = Core.Cms;

namespace Ei.Website
{
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Global()
        {
            InitializeComponent();
        }

        protected void Application_Start(Object sender, EventArgs e)
        {

        }

        protected void Session_Start(Object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {

        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            string infoCookieName = b.ConfigurationHelper.InfoCookieName;
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            HttpCookie infoCookie = Context.Request.Cookies[infoCookieName];

            if (authCookie != null)
            {
                try
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    if (authTicket != null)
                    {
                        cc.SiteIdentity identity = new cc.SiteIdentity(authTicket, infoCookie);
                        cc.SitePrincipal principal = new cc.SitePrincipal(identity);
                        Context.User = principal;
                    }
                    else
                        Response.Redirect(b.ConfigurationHelper.AccessDeniedPage);
                }
                catch (Exception)
                {
                    FormsAuthentication.SignOut();
                }
            }
        }

        protected void Application_Error(Object sender, EventArgs e)
        {

        }

        protected void Session_End(Object sender, EventArgs e)
        {

        }

        protected void Application_End(Object sender, EventArgs e)
        {

        }

        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
        }
        #endregion
    }
}

