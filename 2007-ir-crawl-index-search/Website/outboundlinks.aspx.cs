using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using d = IrProject.Data;
using w = IrProject.WebControls;

namespace IrProject.Website
{
    public class outboundlinks : BasePage
    {
        protected w.MyPager pager;
        protected w.MyGrid dgrPages;
        protected Label lblUrl;
        protected TextBox tbxUrl;
        protected TextBox tbxTitle;
        protected Button btnSearch;
        protected Button btnReset;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            pager.BindingMethod = new w.BindingDelegate(BindData);
            dgrPages.BindingMethod = new w.BindingDelegate(BindData);
            dgrPages.SetPager(pager);
            if (!Page.IsPostBack)
            {
                dgrPages.SortColumnIndex = 0;
                dgrPages.SortOrder = w.MySortOrder.DESCENDING;
                BindData();
                BindTitle();
            }
        }

        private void BindTitle()
        {
            d.Doc doc = new d.Doc(getId());
            lblUrl.Text = "<a href='" + doc.Url + "' target='_blank'>" + doc.Url + "</a> (" + doc.Title + ")";
        }

        private void BindData()
        {
            DataSet ds = new d.LinkData().GetOutboundLinksP(getId(), pager.PageSize, pager.CurrentPage, dgrPages.SortExpression);
            dgrPages.DataSource = ds.Tables[0];
            dgrPages.DataBind();
            pager.AdjustAfterBinding(Convert.ToInt32(ds.Tables[1].Rows[0][0]));
        }

        private int getId()
        {
            return Convert.ToInt32(Request["Id"]);
        }

        protected override void OnInit(EventArgs e)
        {
            this.Load += new System.EventHandler(this.Page_Load);
            base.OnInit(e);
        }
    }
}

