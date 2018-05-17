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
using d = Ei.Data;

namespace Ei.Website.operations
{
    public class caseDetails : b.BasePage
    {
        protected Button btnDelete;
        protected Literal ltrNoDelete;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindDelete();
        }

        private void BindDelete()
        {
            int caseId = Convert.ToInt32(Request["Id"]);
            DataTable dtLineups = new d.LineupData().GetLockedLineupsByCase(caseId, "l.id");
            if (dtLineups.Rows.Count > 0)
            {
                btnDelete.Visible = false;
                ltrNoDelete.Text = "To delete this case, you must first find and delete all its associated <a href='lineups.aspx'>lineups</a>";
            }
            else
                btnDelete.Attributes["onclick"] = "javascript:return confirm('Are you sure you want to delete this case?')";
        }

        protected override int PageId { get { return 15; } }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            d.CaseData cd = new d.CaseData();
            cd.Delete(Convert.ToInt32(Request["Id"]));
            Response.Redirect("caseList.aspx");
        }

        protected override void OnInit(EventArgs e)
        {
            this.btnDelete.Click += new EventHandler(btnDelete_Click);
            base.OnInit(e);
        }
    }
}
