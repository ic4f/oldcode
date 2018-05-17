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
using c = Core;
using d = Ei.Data;

namespace Ei.Website.settings
{
    public class caseList : b.BasePage
    {
        protected c.MySortGrid dgrCases;
        protected Panel pnlGrid;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            dgrCases.BindingMethod = new c.BindingDelegate(BindData);
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            DataTable dtCases = new d.CaseData().GetRecords(dgrCases.SortExpression);
            if (dtCases.Rows.Count > 0)
            {
                pnlGrid.Visible = true;
                dgrCases.Prefix = LinkPrefix;
                dgrCases.DataSource = dtCases;
                dgrCases.DataBind();
            }
            else
                pnlGrid.Visible = false;
        }

        private void dgrCases_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                HyperLink lnkSuspects = (HyperLink)e.Item.Cells[4].Controls[0];
                HyperLink lnkUsers = (HyperLink)e.Item.Cells[5].Controls[0];
                HyperLink lnkDetails = (HyperLink)e.Item.Cells[6].Controls[0];
                HyperLink lnkEdit = (HyperLink)e.Item.Cells[7].Controls[0];
                lnkSuspects.CssClass = "gridlink";
                lnkUsers.CssClass = "gridlink";
                lnkEdit.CssClass = "gridlink";
                lnkDetails.CssClass = "gridlink";
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.dgrCases.ItemDataBound += new DataGridItemEventHandler(dgrCases_ItemDataBound);
            base.OnInit(e);
        }

        protected override int PageId { get { return 14; } }
    }
}
