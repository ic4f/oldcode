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
    public class pages : BasePage
    {
        protected w.MyPager pager;
        protected w.MyGrid dgrPages;
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
                dgrPages.SortOrder = w.MySortOrder.ASCENDING;
                BindData();
            }
        }

        private void BindData()
        {
            DataSet ds = new d.DocData().GetRecordsP(makeQuery(), pager.PageSize, pager.CurrentPage, dgrPages.SortExpression);
            dgrPages.DataSource = ds.Tables[0];
            dgrPages.DataBind();
            pager.AdjustAfterBinding(Convert.ToInt32(ds.Tables[1].Rows[0][0]));
        }

        private string makeQuery()
        {
            string url = filterInput(tbxUrl.Text);
            string title = filterInput(tbxTitle.Text);
            if (url == "" && title == "")
                return "";
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("where ");
                if (url != "")
                {
                    sb.AppendFormat("url like '%{0}%' ", url);
                    if (title != "")
                        sb.Append("and ");
                }
                if (title != "")
                    sb.AppendFormat("title like '%{0}%' ", title);

                return sb.ToString();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            pager.Reset();
            BindData();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            tbxUrl.Text = "";
            tbxTitle.Text = "";
            pager.Reset();
            BindData();
        }

        protected override void OnInit(EventArgs e)
        {
            this.btnSearch.Click += new EventHandler(btnSearch_Click);
            this.btnReset.Click += new EventHandler(btnReset_Click);
            this.Load += new System.EventHandler(this.Page_Load);
            base.OnInit(e);
        }
    }
}

