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
    public class terms : BasePage
    {
        protected w.MyPager pager;
        protected w.MyGrid dgrTerms;
        protected Label lblUrl;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            pager.BindingMethod = new w.BindingDelegate(BindData);
            dgrTerms.BindingMethod = new w.BindingDelegate(BindData);
            dgrTerms.SetPager(pager);
            if (!Page.IsPostBack)
            {
                dgrTerms.SortColumnIndex = 0;
                dgrTerms.SortOrder = w.MySortOrder.DESCENDING;
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
            DataSet ds = new d.SearchData().GetTermsByDocIdP(getId(), pager.PageSize, pager.CurrentPage, dgrTerms.SortExpression);
            dgrTerms.DataSource = ds.Tables[0];
            dgrTerms.DataBind();
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

