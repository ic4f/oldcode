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

namespace Ei.Website.operations
{
    public class caseSuspects : b.BasePage
    {
        protected c.DataCheckboxList cblSuspects;
        protected Button btnSave;
        protected Button btnCancel;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            DataTable dtSuspects = new d.SuspectData().GetAllSuspectsByCase(getCaseId());
            cblSuspects.DataSource = dtSuspects;
            cblSuspects.DataTextField = "externalref";
            cblSuspects.DataValueField = "Id";
            cblSuspects.DataCheckField = "Selected";
            cblSuspects.DataBind();
        }

        private int getCaseId()
        {
            return Convert.ToInt32(Request["CaseId"]);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ArrayList suspects = cblSuspects.GetSelectedValues();
            d.CaseSuspectLink csl = new d.CaseSuspectLink();
            int caseId = getCaseId();
            csl.DeleteByCase(caseId);
            foreach (string suspectId in suspects)
                csl.Create(caseId, Convert.ToInt32(suspectId));
            Response.Redirect("caseList.aspx");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("caseList.aspx");
        }

        protected override int PageId { get { return 18; } }

        protected override void OnInit(EventArgs e)
        {
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            base.OnInit(e);
        }
    }
}
