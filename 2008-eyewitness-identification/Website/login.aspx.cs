using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Security;
using System.Web.Security;
using b = Ei.Business;
using d = Ei.Data;

namespace Ei.Website
{
    public class login : System.Web.UI.Page
    {
        protected Literal ltrLayout1;
        protected Literal ltrLayout2;
        protected Panel pnlLogin;
        protected TextBox tbxLogin;
        protected TextBox tbxPassword;
        protected Button btnLogin;
        protected Label lblAuthFailure;

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                displayLogin();
                b.LayoutHelper lHelper = new b.LayoutHelper();
                ltrLayout1.Text = lHelper.GetLayout1("Sign-in");
                ltrLayout2.Text = lHelper.GetLayout2();
            }
        }

        private void displayLogin()
        {
            pnlLogin.Visible = true;
            lblAuthFailure.Visible = false;
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            d.CmsUserData cud = new d.CmsUserData();
            int cmsuserId = cud.ValidateUser(tbxLogin.Text, tbxPassword.Text);
            if (cmsuserId > 0)
            {
                d.CmsUser user = new d.CmsUser(cmsuserId);
                ArrayList permList = cud.GetPermissionCodesByUser(cmsuserId);
                string permissions = "";
                for (int i = 0; i < permList.Count; i++)
                    permissions += permList[i] + "|";

                if (permissions.Length > 0)
                    permissions = permissions.Substring(0, permissions.Length - 1); //gets rid of last delimeter

                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    1,
                    cmsuserId.ToString(),
                    DateTime.Now,
                    DateTime.Now.AddMinutes(20),
                    false,
                    permissions);  //version + userid + creation + expiration + persistent + user permissions(codes)

                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                string infoCookieName = b.ConfigurationHelper.InfoCookieName;
                HttpCookie infoCookie = new HttpCookie(infoCookieName);
                infoCookie.Values["Login"] = user.Login;
                infoCookie.Values["FirstName"] = user.FirstName;
                infoCookie.Values["LastName"] = user.LastName;
                Response.Cookies.Add(authCookie);
                Response.Cookies.Add(infoCookie);

                string redirect = FormsAuthentication.GetRedirectUrl(tbxLogin.Text, false);
                Response.Redirect(redirect);
            }
            else
            {
                lblAuthFailure.Text = "Login failed for " + tbxLogin.Text;
                lblAuthFailure.Visible = true;
                tbxLogin.Text = string.Empty;
            }
        }

        override protected void OnInit(EventArgs e)
        {
            this.btnLogin.Click += new EventHandler(btnLogin_Click);
            this.Load += new System.EventHandler(this.Page_Load);
            base.OnInit(e);
        }
    }
}
