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

namespace Ei.Website.operations
{
    public class _default : b.BasePage
    {
        protected Panel pnlCases;
        protected Panel pnlSuspects;
        protected Panel pnlLineups;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                displayContent();
        }

        private void displayContent()
        {
            pnlCases.Visible = false;
            pnlSuspects.Visible = false;
            pnlLineups.Visible = false;
            if (HasPermission(b.PermissionCode.Operations_CSL_Full))
            {
                pnlCases.Visible = true;
                pnlSuspects.Visible = true;
                pnlLineups.Visible = true;
            }
        }

        protected override int PageId { get { return 11; } }
    }
}
