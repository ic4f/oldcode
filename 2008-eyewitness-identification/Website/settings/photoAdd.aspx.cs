using System;
using System.IO;
using System.Text;
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
using d = Ei.Data;

namespace Ei.Website.settings
{
    public class photoAdd : b.BasePage
    {
        protected Literal ltrUncatPhotos;
        protected HyperLink lnkNext12;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            int uncat = new d.PhotoData().GetUncategoriedNumber();
            ltrUncatPhotos.Text = uncat.ToString();
            if (uncat == 0)
                lnkNext12.Visible = false;
        }

        protected override int PageId { get { return 45; } }
    }
}
