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
using System.Text;
using d = DataMining.Data;

namespace DataMining.Website
{
    public class _default : System.Web.UI.Page
    {
        protected TextBox tbxSearch;
        protected Button btnSearch;
        protected Button btnReset;
        protected Literal ltrSearchResults;
        protected DropDownList ddlDocs;
        protected Button btnGetSim;
        protected Button btnResetSim;
        protected Literal ltrSimResults;
        protected Button btnCats;
        protected TextBox tbxK;
        protected TextBox tbxC;

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            d.DocsLoader dLoader = new d.DocsLoader();
            d.DocRecord[] drs = dLoader.GetDocs();
            foreach (d.DocRecord dr in drs)
            {
                ddlDocs.Items.Add(new ListItem(dr.Title, dr.DocId.ToString()));
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            execSearch();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ltrSearchResults.Text = "";
            tbxSearch.Text = "";
        }

        private void btnGetSim_Click(object sender, EventArgs e)
        {
            execSim();
        }

        private void btnResetSim_Click(object sender, EventArgs e)
        {
            ltrSimResults.Text = "";
            ddlDocs.SelectedIndex = -1;
        }

        private void btnCats_Click(object sender, EventArgs e)
        {
            execCats();
        }

        private void execCats()
        {
            int docId = Convert.ToInt32(ddlDocs.SelectedValue);
            int k = Convert.ToInt32(tbxK.Text);
            int c = Convert.ToInt32(tbxC.Text);
            d.CatsLoader cLoader = new d.CatsLoader();
            d.DocCatsLoader dcLoader = new d.DocCatsLoader(cLoader);
            d.Tester tester = new d.Tester(new d.DocsLoader(), cLoader, dcLoader);
            Hashtable result = tester.GetNewCategories(docId, k, c);

            ArrayList arrRelevant = dcLoader.GetDocCategories(docId);
            Hashtable relevant = new Hashtable();
            if (arrRelevant != null)
            {
                foreach (int catId in arrRelevant)
                    relevant.Add(catId, true);
            }

            d.PerformanceCalculator pc = new d.PerformanceCalculator(result, relevant);
            StringBuilder sb = new StringBuilder();

            sb.Append("<p><b>Newly assigned categories:</b>");
            if (result.Count > 0)
            {
                sb.Append("<ul>");
                IDictionaryEnumerator en = result.GetEnumerator();
                while (en.MoveNext())
                {
                    int catId = Convert.ToInt32(en.Key);
                    sb.AppendFormat("<li><a href='http://en.wikipedia.org/wiki/Category:{0}'>{0}</a>", cLoader.GetCategory(catId));
                }
                sb.Append("</ul>");
            }
            else
                sb.Append("<p>none assigned");

            sb.Append("<p><b>Relevant categories:</b>");
            if (relevant.Count > 0)
            {
                sb.Append("<ul>");
                IDictionaryEnumerator en = relevant.GetEnumerator();
                while (en.MoveNext())
                {
                    int catId = Convert.ToInt32(en.Key);
                    sb.AppendFormat("<li><a href='http://en.wikipedia.org/wiki/Category:{0}'>{0}</a>", cLoader.GetCategory(catId));
                }
                sb.Append("</ul>");
            }
            else
                sb.Append("<p>none relevant");


            sb.Append("<p><b>Evaluation:</b>");
            sb.Append("<ul>");
            sb.AppendFormat("<li>Precision: {0}", pc.Precision);
            sb.AppendFormat("<li>Recall: {0}", pc.Recall);
            sb.AppendFormat("<li>F-Measure: {0}", pc.FMeasure);
            sb.Append("</ul>");

            ltrSimResults.Text = sb.ToString();
        }

        private void execSim()
        {
            int docId = Convert.ToInt32(ddlDocs.SelectedValue);
            d.Index index = new d.Index(d.Helper.INDEX_PATH);
            d.DocVSSearcher searcher = new d.DocVSSearcher(index, new d.DocsLoader(), new d.CatsLoader());
            d.ResultDocument[] results = searcher.Search(docId, 20);
            StringBuilder sb = new StringBuilder();

            if (results != null)
            {
                sb.AppendFormat("<p>Displaying <b>20</b> most similar documents:</p><table cellpadding=5 cellspacing=0 border=1>", results.Length);
                sb.Append("<tr bgcolor=#f1f1f1><td><b>similarity</b></td><td><b>title</b></td></tr>");
                int max = Math.Min(20, results.Length);
                for (int i = 0; i < max; i++)
                    sb.AppendFormat("<tr><td>{0}</td><td><a href='http://en.wikipedia.org/wiki/{1}'>{1}</a></td></tr>",
                        results[i].Similarity, index.GetURL(results[i].DocId));
                sb.Append("</table>");
            }
            else
                sb.Append("<p>Found <b>0</b> results");

            ltrSimResults.Text = sb.ToString();

        }

        private void execSearch()
        {
            string query = tbxSearch.Text;
            d.Index index = new d.Index(d.Helper.INDEX_PATH);
            d.VSSearcher searcher = new d.VSSearcher(index);
            d.ResultDocument[] results = searcher.Search(query);
            StringBuilder sb = new StringBuilder();

            if (results != null)
            {
                sb.AppendFormat("<p>Found <b>{0}</b> relevant documents / displaying top 20:</p><table cellpadding=5 cellspacing=0 border=1>", results.Length);
                sb.Append("<tr bgcolor=#f1f1f1><td><b>similarity</b></td><td><b>title</b></td></tr>");
                int max = Math.Min(20, results.Length);
                for (int i = 0; i < max; i++)
                    sb.AppendFormat("<tr><td>{0}</td><td><a href='http://en.wikipedia.org/wiki/{1}'>{1}</a></td></tr>",
                        results[i].Similarity, index.GetURL(results[i].DocId));
                sb.Append("</table>");
            }
            else
                sb.Append("<p>Found <b>0</b> results");

            ltrSearchResults.Text = sb.ToString();
        }

        override protected void OnInit(EventArgs e)
        {
            this.btnSearch.Click += new EventHandler(btnSearch_Click);
            this.btnReset.Click += new EventHandler(btnReset_Click);
            this.btnGetSim.Click += new EventHandler(btnGetSim_Click);
            this.btnResetSim.Click += new EventHandler(btnResetSim_Click);
            this.btnCats.Click += new EventHandler(btnCats_Click);
            this.Load += new System.EventHandler(this.Page_Load);
            base.OnInit(e);
        }
    }
}
