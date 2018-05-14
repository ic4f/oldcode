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
using d = IrProject.Data;
using w = IrProject.WebControls;
using id = IrProject.Indexing;

namespace IrProject.Website
{
    public class web : BasePage
    {
        protected TextBox tbxUrl;
        protected Button btnExtract;
        protected Button btnIndex;
        protected Panel pnlExtract;
        protected Panel pnlIndex;
        protected Label lblExtract;
        protected Literal ltrExtractedUrl;
        protected Literal ltrIndexUrl;
        protected Literal ltrTermCount;
        protected w.MySortGrid dgrTerms;

        private int count;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            dgrTerms.BindingMethod = new w.BindingDelegate(BindData);
            if (!Page.IsPostBack)
            {
                dgrTerms.SortColumnIndex = 0;
                dgrTerms.SortOrder = w.MySortOrder.DESCENDING;
            }
        }

        private void BindData()
        {
            string url = filterInput(tbxUrl.Text);
            pnlExtract.Visible = false;
            pnlIndex.Visible = true;
            id.PageParser pp = new id.PageParser(url);
            setUrlLiteral(ltrIndexUrl, url, pp.GetTitle());
            dgrTerms.DataSource = pp.GetTerms(dgrTerms.SortExpression);
            dgrTerms.DataBind();
            ltrTermCount.Text = pp.GetTermCount().ToString();
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string url = filterInput(tbxUrl.Text);
                pnlExtract.Visible = true;
                pnlIndex.Visible = false;
                id.PageParser pp = new id.PageParser(url);
                setUrlLiteral(ltrExtractedUrl, url, pp.GetTitle());
                lblExtract.Text = pp.GetSource();
            }
        }

        private void btnIndex_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
                BindData();
        }

        private void setUrlLiteral(Literal ltr, string url, string title)
        {
            ltr.Text = "<a href='" + url + "' target='_blank'>" + url + "</a> (" + title + ")";
        }

        private void dgrDocs_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                Label lblNumber = (Label)e.Item.Cells[0].FindControl("lblNumber");
                lblNumber.Text = count++.ToString();
            }
        }

        private string filterInput(string input)
        {
            input = input.Replace("'", "''");
            input = input.Replace("<", "");
            input = input.Replace(">", "");
            if (input.IndexOf("/*") > -1 || input.IndexOf("--") > -1)
                return "";
            else
                return input.Trim();
        }

        protected override void OnInit(EventArgs e)
        {
            this.btnExtract.Click += new EventHandler(btnExtract_Click);
            this.btnIndex.Click += new EventHandler(btnIndex_Click);
            this.Load += new System.EventHandler(this.Page_Load);
            base.OnInit(e);
        }
    }
}

