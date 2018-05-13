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
using b = Ei.Business;

namespace Ei.Website
{
    public class help : b.BasePage
    {
        protected override void Page_Load(object sender, System.EventArgs e)
        {
        }

        protected override int PageId { get { return 3; } }
    }
}
