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
    public class caseUsers : b.BasePage
    {
        protected c.DataCheckboxList cblUsers;
        protected Button btnSave;
        protected Button btnCancel;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            DataTable dtUsers = new d.UserData().GetAllUsersByCase(getCaseId());
            cblUsers.DataSource = dtUsers;
            cblUsers.DataTextField = "displayedname";
            cblUsers.DataValueField = "id";
            cblUsers.DataCheckField = "Selected";
            cblUsers.DataBind();
        }

        private int getCaseId()
        {
            return Convert.ToInt32(Request["CaseId"]);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ArrayList users = cblUsers.GetSelectedValues();
            d.CaseUserLink cul = new d.CaseUserLink();
            int caseId = getCaseId();
            cul.DeleteByCase(caseId);
            foreach (string userId in users)
                cul.Create(caseId, Convert.ToInt32(userId));
            Response.Redirect("caseList.aspx");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("caseList.aspx");
        }

        protected override int PageId { get { return 17; } }

        protected override void OnInit(EventArgs e)
        {
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            base.OnInit(e);
        }
    }
}
