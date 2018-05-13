using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using b = Ei.Business;

namespace Ei.Website
{
    public class signout : System.Web.UI.Page
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
                FormsAuthentication.SignOut();
            Response.Redirect(b.ConfigurationHelper.LoginPage);
        }

        override protected void OnInit(EventArgs e)
        {
            this.Load += new System.EventHandler(this.Page_Load);
            base.OnInit(e);
        }
    }
}
