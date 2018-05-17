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

namespace IrProject.Website
{
    public abstract class BasePage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.Load += new System.EventHandler(Page_Load);
            base.OnInit(e);
        }

        protected abstract void Page_Load(object sender, System.EventArgs e);
    }
}