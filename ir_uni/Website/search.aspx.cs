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
using id = IrProject.Indexing;

namespace IrProject.Website
{
    public class search : BasePage
    {
        protected TextBox tbxPageRank;
        protected RadioButtonList radIndex;

        protected TextBox tbxSearch;
        protected Button btnSearch;
        protected Button btnAH;

        protected Panel pnlSearch;
        protected Literal ltrSearchResult;
        protected w.MyBaseGrid dgrDocs;
        protected Panel pnlDocs;

        protected Panel pnlAH;
        protected Literal ltrAHResult;

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

        private void btnSearch_Click(object sender, EventArgs e) { if (Page.IsValid) execSearch(); }

        private void btnAH_Click(object sender, EventArgs e) { if (Page.IsValid) execAH(); }

        private void execSearch()
        {
            pnlSearch.Visible = true;
            pnlAH.Visible = false;

            id.QueryVector qv = new id.QueryVector(filterInput(tbxSearch.Text));

            float w = 1.0f;
            try { w = Convert.ToSingle(tbxPageRank.Text); }
            catch (Exception) { }

            DataSet ds = null;
            if (radIndex.SelectedValue == "0")
                ds = new d.SearchData().GetSearchResults(qv.QueryTerms, qv.QueryWeights, w);
            else if (radIndex.SelectedValue == "1")
                ds = new d.SearchData().GetSearchResults_w(qv.QueryTerms, qv.QueryWeights, w);
            else if (radIndex.SelectedValue == "2")
                ds = new d.SearchData().GetSearchResults_a(qv.QueryTerms, qv.QueryWeights, w);
            else if (radIndex.SelectedValue == "3")
                ds = new d.SearchData().GetSearchResults_wa(qv.QueryTerms, qv.QueryWeights, w);

            int resultcount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);

            if (resultcount > 0)
            {
                count = 1;
                pnlDocs.Visible = true;
                dgrDocs.DataSource = ds.Tables[0];
                dgrDocs.DataBind();
                ltrSearchResult.Text = "Your search returned <b>" + resultcount + "</b> results";
            }
            else
            {
                pnlDocs.Visible = false;
                ltrSearchResult.Text = "No relevant documents found";
            }
        }

        private void execAH()
        {
            pnlSearch.Visible = false;
            pnlAH.Visible = true;

            id.QueryVector qv = new id.QueryVector(filterInput(tbxSearch.Text));

            float w = 1.0f;
            try { w = Convert.ToSingle(tbxPageRank.Text); }
            catch (Exception) { }

            DataSet ds = new d.SearchData().GetSearchResults(qv.QueryTerms, qv.QueryWeights, w);
            DataTable dt = ds.Tables[0];
            int resultcount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);

            if (resultcount > 0)
            {
                int rootSize = 50; //Convert.ToInt32(tbxRoot.Text);
                int maxParents = 20; //Convert.ToInt32(tbxParents.Text);
                int maxChildren = 20; //Convert.ToInt32(tbxChildren.Text);
                int displayResults = 50;

                int[] resultIds = new int[resultcount];
                int j = 0;
                foreach (DataRow dr in dt.Rows)
                    resultIds[j++] = Convert.ToInt32(dr[0]);

                id.AHPageLoader ahl = new id.AHPageLoader(resultIds, rootSize, maxParents, maxChildren);
                id.AHDocument[] authorities = ahl.Authorities;

                int max = Math.Min(authorities.Length, Convert.ToInt32(displayResults));

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<p style='border-bottom:solid 1px #999999;'>Your search returned <b>{0}</b> results. " +
                    "Displaying the top {1} authorities:</p>", resultcount, max);
                sb.Append("<table style='font-family:Verdana;font-size:10pt;' cellpadding='3' cellspacing='2' width='100%'>");
                sb.Append("<tr bgcolor='#f1f1f1'><td>authority score</td><td>hub score</td><td>document</td></tr>");

                d.DocData dd = new d.DocData();
                DataTable docData;
                for (int i = 0; i < max; i++)
                {
                    int docId = authorities[i].DocId;

                    docData = dd.GetDocData(docId);
                    string url = docData.Rows[0][0].ToString();
                    sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td><p style='margin-bottom:-10px;'><a style='font-size:11pt;' href='{2}'>{3}</a>",
                        authorities[i].AuthorityScore, authorities[i].HubScore, url, docData.Rows[0][1].ToString());
                    sb.AppendFormat("<p><a style='color:green;font-size:9pt' href='{0}'>{0}</a></td></tr>", url);
                }
                sb.Append("</table>");
                ltrAHResult.Text = sb.ToString();
            }
            else
                ltrAHResult.Text = "There were no results";
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
            this.btnAH.Click += new EventHandler(btnAH_Click);
            this.dgrDocs.ItemDataBound += new DataGridItemEventHandler(dgrDocs_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);
            base.OnInit(e);
        }
    }
}

