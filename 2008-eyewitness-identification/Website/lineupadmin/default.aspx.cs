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

namespace Ei.Website.lineupadmin
{
    public class _default : b.BasePage
    {
        protected Panel pnlCreate;
        protected Panel pnlAdminister;
        protected Panel pnlLineups;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                displayContent();
        }

        private void displayContent()
        {
            pnlCreate.Visible = false;
            pnlAdminister.Visible = false;
            pnlLineups.Visible = false;
            if (HasPermission(b.PermissionCode.LineupAdmin_Lineups_CreateAdminister))
            {
                pnlCreate.Visible = true;
                pnlAdminister.Visible = true;
                pnlLineups.Visible = true;
            }
        }

        protected override int PageId { get { return 4; } }
    }
}
