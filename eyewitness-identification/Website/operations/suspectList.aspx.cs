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
    public class suspectList : b.BasePage
    {
        protected c.MySortGrid dgrSuspects;
        protected Panel pnlGrid;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            dgrSuspects.BindingMethod = new c.BindingDelegate(BindData);
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            DataTable dtSuspects = new d.SuspectData().GetRecords(dgrSuspects.SortExpression);
            if (dtSuspects.Rows.Count > 0)
            {
                pnlGrid.Visible = true;
                dgrSuspects.Prefix = LinkPrefix;
                dgrSuspects.DataSource = dtSuspects;
                dgrSuspects.DataBind();
            }
            else
                pnlGrid.Visible = false;
        }

        private void dgrSuspects_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                Label lblPreview = (Label)e.Item.Cells[0].Controls[0];
                HyperLink lnkDetails = (HyperLink)e.Item.Cells[10].Controls[0];
                HyperLink lnkEdit = (HyperLink)e.Item.Cells[11].Controls[0];
                lnkEdit.CssClass = "gridlink";
                lnkDetails.CssClass = "gridlink";

                int suspectId = Convert.ToInt16(dgrSuspects.DataKeys[e.Item.ItemIndex]);
                string urlSmall = b.UrlHelper.GetSuspectImageUrl(suspectId, b.ImageHelper.SMALL_PHOTO_SUFFIX);
                string urlLarge = b.UrlHelper.GetSuspectImageUrl(suspectId, b.ImageHelper.LARGE_PHOTO_SUFFIX);
                lblPreview.Text = "<a href='" + urlLarge + "' target='_blank'><img src='" + urlSmall + "' border='1'></a>";
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.dgrSuspects.ItemDataBound += new DataGridItemEventHandler(dgrSuspects_ItemDataBound);
            base.OnInit(e);
        }

        protected override int PageId { get { return 21; } }
    }
}
