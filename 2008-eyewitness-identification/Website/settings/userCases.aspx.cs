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

namespace Ei.Website.settings
{
    public class userCases : b.BasePage
    {
        protected c.DataCheckboxList cblCases;
        protected Button btnSave;
        protected Button btnCancel;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            DataTable dtCases = new d.CaseData().GetAllCasesByUser(getUserId());
            cblCases.DataSource = dtCases;
            cblCases.DataTextField = "externalref";
            cblCases.DataValueField = "Id";
            cblCases.DataCheckField = "Selected";
            cblCases.DataBind();
        }

        private int getUserId()
        {
            return Convert.ToInt32(Request["UserId"]);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ArrayList cases = cblCases.GetSelectedValues();
            d.CaseUserLink cul = new d.CaseUserLink();
            int userId = getUserId();
            cul.DeleteByUser(userId);
            foreach (string caseId in cases)
                cul.Create(Convert.ToInt32(caseId), userId);
            Response.Redirect("userList.aspx");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("userList.aspx");
        }

        protected override int PageId { get { return 50; } }

        protected override void OnInit(EventArgs e)
        {
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            base.OnInit(e);
        }
    }
}
