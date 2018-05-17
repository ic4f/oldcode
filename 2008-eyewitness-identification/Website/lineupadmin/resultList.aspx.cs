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
    public class resultList : b.BasePage
    {
        protected c.MySortGrid dgrResults;
        protected Panel pnlGrid;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            dgrResults.BindingMethod = new c.BindingDelegate(BindData);
            if (!Page.IsPostBack)
            {
                dgrResults.SortColumnIndex = 2;
                dgrResults.SortOrder = " desc";
                BindData();
            }
        }

        private void BindData()
        {
            int lineupId = Convert.ToInt32(Request["LineupId"]);
            DataTable dtResults = new d.LineupViewData().GetCompletedLineupViewsByLineup(lineupId, dgrResults.SortExpression);
            if (dtResults.Rows.Count > 0)
            {
                pnlGrid.Visible = true;
                dgrResults.Prefix = LinkPrefix;
                dgrResults.DataSource = dtResults;
                dgrResults.DataBind();
            }
            else
                pnlGrid.Visible = false;
        }

        protected override int PageId { get { return 9; } }
    }
}
