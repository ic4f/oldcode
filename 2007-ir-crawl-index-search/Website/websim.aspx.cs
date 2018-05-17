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
    public class websim : BasePage
    {
        protected TextBox tbxPageRank;
        protected RadioButtonList radIndex;

        protected TextBox tbxUrl;
        protected Button btnSearch;
        protected w.MyBaseGrid dgrDocs;
        protected Panel pnlDocs;
        protected Literal ltrUrl;
        protected Literal ltrDocCount;
        protected Label lblError;

        private int count;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindOptions();
        }

        private void BindOptions()
        {
            radIndex.Items.Clear();
            radIndex.Items.Add(new ListItem("unweighted", "0"));
            radIndex.Items.Add(new ListItem("weighted", "1"));
            radIndex.Items.Add(new ListItem("unweighted with external anchor text", "2"));
            radIndex.Items.Add(new ListItem("weighted with external anchor text", "3"));
            radIndex.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
                execSearch();
        }

        private void execSearch()
        {
            float w = 1.0f;
            try { w = Convert.ToSingle(tbxPageRank.Text); }
            catch (Exception) { }

            string url = filterInput(tbxUrl.Text);
            pnlDocs.Visible = true;
            id.PageParser pp = new id.PageParser(url);
            ltrUrl.Text = "<a href='" + url + "' target='_blank'>" + url + "</a> (" + pp.GetTitle() + ")";

            int[] weights = { 1, 1, 1, 1, 1, 1 }; //text/bold/header/anchor/title/url
            id.QueryVector qv = pp.GetQuery(weights);
            /*
			 * if you ever change this, remember that the parserhelper.getDelims() protects against sql-injection by
			 * treating all punctuation as a delimiter. Therefore, any comment syntax will not be included in a term. 
			 */
            DataSet ds = null;
            if (radIndex.SelectedValue == "0")
                ds = new d.SearchData().GetSimilarityResults(qv.QueryTerms, qv.QueryWeights, w);
            else if (radIndex.SelectedValue == "1")
                ds = new d.SearchData().GetSimilarityResults_w(qv.QueryTerms, qv.QueryWeights, w);
            else if (radIndex.SelectedValue == "2")
                ds = new d.SearchData().GetSimilarityResults_a(qv.QueryTerms, qv.QueryWeights, w);
            else if (radIndex.SelectedValue == "3")
                ds = new d.SearchData().GetSimilarityResults_wa(qv.QueryTerms, qv.QueryWeights, w);

            int results = Convert.ToInt32(ds.Tables[1].Rows[0][0]);

            if (results > 0)
            {
                count = 1;
                dgrDocs.DataSource = ds.Tables[0];
                dgrDocs.DataBind();
                ltrDocCount.Text = results.ToString();
                lblError.Visible = false;
            }
            else
            {
                pnlDocs.Visible = false;
                lblError.Visible = true;
            }
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
            this.btnSearch.Click += new EventHandler(btnSearch_Click);
            this.dgrDocs.ItemDataBound += new DataGridItemEventHandler(dgrDocs_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);
            base.OnInit(e);
        }
    }
}

