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
using d = Ei.Data;
using c = Core;

namespace Ei.Website.lineupadmin
{
    public class lineupList : b.BasePage
    {
        protected DropDownList ddlCase;
        protected c.MySortGrid dgrLineups;
        protected Panel pnlGrid;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            dgrLineups.BindingMethod = new c.BindingDelegate(BindData);
            if (!Page.IsPostBack)
            {
                dgrLineups.SortColumnIndex = 2;
                dgrLineups.SortOrder = " desc";
                BindData();
            }
        }

        private void BindData()
        {
            BindCases();
            BindLineups();
        }

        private void BindCases()
        {
            ddlCase.DataSource = new d.CaseData().GetCasesWithSuspectsByUser(Identity.UserId);
            ddlCase.DataTextField = "externalref";
            ddlCase.DataValueField = "id";
            ddlCase.DataBind();
            ddlCase.AutoPostBack = true;
        }

        private void BindLineups()
        {
            if (ddlCase.SelectedValue != "")
            {
                int caseId = Convert.ToInt32(ddlCase.SelectedValue);
                DataTable dtLineups = new d.LineupData().GetLockedLineupsByCase(caseId, dgrLineups.SortExpression);
                if (dtLineups.Rows.Count > 0)
                {
                    pnlGrid.Visible = true;
                    dgrLineups.Prefix = LinkPrefix;
                    dgrLineups.DataSource = dtLineups;
                    dgrLineups.DataBind();
                }
                else
                    pnlGrid.Visible = false;
            }
        }

        protected override int PageId { get { return 7; } }

        private void ddlCase_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLineups();
        }

        protected override void OnInit(EventArgs e)
        {
            this.ddlCase.SelectedIndexChanged += new EventHandler(ddlCase_SelectedIndexChanged);
            base.OnInit(e);
        }
    }
}
