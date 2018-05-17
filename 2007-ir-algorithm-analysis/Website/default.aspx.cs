using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using d = Giggle.Data;

namespace Giggle.Website
{
    public class _default : System.Web.UI.Page
    {
        protected TextBox tbxQuery;
        protected Button btnSearch;
        protected Button btnAuthorities;
        protected Button btnCluster;
        protected Button btnReset;

        protected RadioButton radLIndex;
        protected RadioButton radGIndex;

        protected CheckBox cbxBold;
        protected CheckBox cbxHeading;
        protected CheckBox cbxTitle;
        protected CheckBox cbxUrl;

        protected TextBox tbxPagerank;

        protected TextBox tbxRoot;
        protected TextBox tbxChildren;
        protected TextBox tbxParents;

        protected RadioButton radKmeans;
        protected RadioButton radBuckshot;
        protected RadioButton radBisect;

        protected TextBox tbxKmeans;
        protected TextBox tbxDocs;

        protected TextBox tbxDisplay;

        protected Literal ltrResults;

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                execReset();
        }

        private void btnSearch_Click(object sender, EventArgs e) { execSearch(); }

        private void btnAuthorities_Click(object sender, EventArgs e) { execAuthorities(); }

        private void btnCluster_Click(object sender, EventArgs e) { execCluster(); }

        private void btnReset_Click(object sender, EventArgs e) { execReset(); }

        private void radLIndex_CheckedChanged(object sender, EventArgs e)
        {
            execIndexSelection();
        }

        private void radGIndex_CheckedChanged(object sender, EventArgs e)
        {
            execIndexSelection();
        }

        private void execSearch()
        {
            float w = Convert.ToSingle(tbxPagerank.Text);
            d.Index index = new d.Index(getIndexDir());
            d.VSSearcher searcher = new d.VSSearcher(index, w);
            d.ResultDocument[] results = searcher.Search(tbxQuery.Text.Trim());
            if (results != null && results.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<p style='border-bottom:solid 1px #999999;'>Your search returned <b>{0}</b> results. Displaying the top {1}:</p>", results.Length, tbxDisplay.Text);
                sb.Append("<table style='font-family:Verdana;font-size:10pt;' cellpadding='3' cellspacing='2' width='100%'>");
                sb.Append("<tr bgcolor='#f1f1f1'><td>#</td><td>similarity</td><td>pagerank</td><td>document</td></tr>");
                int max = Math.Min(results.Length, Convert.ToInt32(tbxDisplay.Text));
                for (int i = 0; i < max; i++)
                {
                    int docId = results[i].DocId;
                    string url = "http://" + index.GetURL(docId).Replace("%%", "/");
                    sb.AppendFormat("<tr><td>{4}</td><td>{0}</td><td>{1}</td><td><p style='margin-bottom:-10px;'><a style='font-size:11pt;' href='{2}'>{3}</a>",
                        results[i].Similarity, results[i].PageRank, url, index.GetTitle(docId), i + 1);
                    sb.AppendFormat("<p><a style='color:green;font-size:9pt' href='{0}'>{0}</a></td></tr>", url);
                }
                sb.Append("</table>");
                ltrResults.Text = sb.ToString();
            }
            else
                ltrResults.Text = "There were no results";
        }

        private void execAuthorities()
        {
            float w = Convert.ToSingle(tbxPagerank.Text);
            d.Index index = new d.Index(getIndexDir());
            d.VSSearcher searcher = new d.VSSearcher(index, w);
            d.ResultDocument[] results = searcher.Search(tbxQuery.Text.Trim());
            if (results != null && results.Length > 0)
            {
                StringBuilder sb = new StringBuilder();

                int rootSize = Convert.ToInt32(tbxRoot.Text);
                int maxParents = Convert.ToInt32(tbxParents.Text);
                int maxChildren = Convert.ToInt32(tbxChildren.Text);
                d.AHPageLoader ahl = new d.AHPageLoader(index, results, rootSize, maxParents, maxChildren);
                d.AHDocument[] authorities = ahl.Authorities;

                int max = Math.Min(authorities.Length, Convert.ToInt32(tbxDisplay.Text));

                sb.AppendFormat("<p style='border-bottom:solid 1px #999999;'>Your search returned <b>{0}</b> results. " +
                    "Displaying the top {1} authorities:</p>", results.Length, max);
                sb.Append("<table style='font-family:Verdana;font-size:10pt;' cellpadding='3' cellspacing='2' width='100%'>");
                sb.Append("<tr bgcolor='#f1f1f1'><td>authority score</td><td>hub score</td><td>document</td></tr>");

                for (int i = 0; i < max; i++)
                {
                    int docId = authorities[i].DocId;
                    string url = "http://" + index.GetURL(docId).Replace("%%", "/");
                    sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td><p style='margin-bottom:-10px;'><a style='font-size:11pt;' href='{2}'>{3}</a>",
                        authorities[i].AuthorityScore, authorities[i].HubScore, url, index.GetTitle(docId));
                    sb.AppendFormat("<p><a style='color:green;font-size:9pt' href='{0}'>{0}</a></td></tr>", url);
                }
                sb.Append("</table>");
                ltrResults.Text = sb.ToString();
            }
            else
                ltrResults.Text = "There were no results";
        }

        private void execCluster()
        {
            float w = Convert.ToSingle(tbxPagerank.Text);
            d.Index index = new d.Index(getIndexDir());
            d.VSSearcher searcher = new d.VSSearcher(index, w);
            d.ResultDocument[] results = searcher.Search(tbxQuery.Text.Trim());
            if (results != null && results.Length > 0)
            {
                int toCluster = Convert.ToInt32(tbxDocs.Text);
                toCluster = Math.Min(toCluster, results.Length);
                short[] docIds = new short[toCluster];
                for (int i = 0; i < toCluster; i++)
                    docIds[i] = results[i].DocId;

                int k = Convert.ToInt32(tbxKmeans.Text);

                d.Cluster[] clusters;
                if (radKmeans.Checked)
                    clusters = new d.KMeansClustering(index, k, true).GetClusters(docIds, 10);
                else if (radBuckshot.Checked)
                    clusters = new d.BisectingClustering(index, k).GetClusters(docIds, 10);
                else
                    clusters = new d.BisectingClustering(index, k).GetClusters(docIds, 10);


                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("<p style='border-bottom:solid 1px #999999;'>Your search returned <b>{0}</b> results. " +
                    " Displaying the top {1} documents clustered into at most {2} clusters:</p>", results.Length, toCluster, k);

                for (int i = 0; i < clusters.Length; i++)
                {
                    sb.AppendFormat("<p style='margin-bottom:-10px;font-weight:bold;font-size:11pt;'>Cluster {0}", i + 1);
                    sb.Append("<p>Common terms: ");
                    IDictionaryEnumerator en = clusters[i].CommonTermIds.GetEnumerator();
                    while (en.MoveNext())
                        sb.AppendFormat("{0} ", index.GetTerm(Convert.ToInt32(en.Key)));

                    sb.Append("<p>");

                    en = clusters[i].DocIds.GetEnumerator();
                    int count = 0;
                    int topDocs = 3;
                    while (count < topDocs && en.MoveNext())
                    {
                        short docId = Convert.ToInt16(en.Key);
                        string url = "http://" + index.GetURL(docId).Replace("%%", "/");
                        sb.AppendFormat("<p style='margin-bottom:-10px;'><a style='font-size:11pt;' href='{0}'>{1}</a>", url, index.GetTitle(docId));
                        sb.AppendFormat("<p><a style='color:green;font-size:9pt' href='{0}'>{0}</a>", url);
                        count++;
                    }
                    sb.Append("<p style='border-bottom: solid 1px #999999;'>");
                }
                ltrResults.Text = sb.ToString();
            }
            else
                ltrResults.Text = "There were no results";
        }

        private void execReset()
        {
            tbxQuery.Text = "";
            radLIndex.Checked = true;
            radGIndex.Checked = false;
            cbxBold.Checked = false;
            cbxHeading.Checked = false;
            cbxTitle.Checked = false;
            cbxUrl.Checked = false;
            cbxBold.Enabled = false;
            cbxHeading.Enabled = false;
            cbxTitle.Enabled = false;
            cbxUrl.Enabled = false;
            tbxPagerank.Text = "0.6";
            tbxRoot.Text = "50";
            tbxChildren.Text = "20";
            tbxParents.Text = "20";
            radKmeans.Checked = true;
            radBuckshot.Checked = false;
            radBisect.Checked = false;
            tbxKmeans.Text = "3";
            tbxDocs.Text = "50";
            tbxDisplay.Text = "20";
            ltrResults.Text = "";
        }

        private void execIndexSelection()
        {
            if (radGIndex.Checked)
            {
                cbxBold.Enabled = true;
                cbxHeading.Enabled = true;
                cbxTitle.Enabled = true;
                cbxUrl.Enabled = true;
            }
            else if (radLIndex.Checked)
            {
                cbxBold.Enabled = false;
                cbxHeading.Enabled = false;
                cbxTitle.Enabled = false;
                cbxUrl.Enabled = false;
                cbxBold.Checked = false;
                cbxHeading.Checked = false;
                cbxTitle.Checked = false;
                cbxUrl.Checked = false;
            }
        }

        private string getIndexDir()
        {
            if (radLIndex.Checked)
                return @"C:\_current\development\data\isr\luceneindex\index\";
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"C:\_current\development\data\isr\giggleindex\");
                if (cbxBold.Checked) sb.Append("2");
                else sb.Append("1");
                sb.Append("_");
                if (cbxHeading.Checked) sb.Append("3");
                else sb.Append("1");
                sb.Append("_");
                if (cbxTitle.Checked) sb.Append("4");
                else sb.Append("1");
                sb.Append("_");
                if (cbxUrl.Checked) sb.Append("5");
                else sb.Append("1");
                sb.Append(@"\index\");
                return sb.ToString();
            }
        }

        override protected void OnInit(EventArgs e)
        {
            this.radLIndex.CheckedChanged += new EventHandler(radLIndex_CheckedChanged);
            this.radGIndex.CheckedChanged += new EventHandler(radGIndex_CheckedChanged);
            this.btnSearch.Click += new EventHandler(btnSearch_Click);
            this.btnAuthorities.Click += new EventHandler(btnAuthorities_Click);
            this.btnCluster.Click += new EventHandler(btnCluster_Click);
            this.btnReset.Click += new EventHandler(btnReset_Click);
            this.Load += new System.EventHandler(this.Page_Load);
            base.OnInit(e);
        }
    }
}
