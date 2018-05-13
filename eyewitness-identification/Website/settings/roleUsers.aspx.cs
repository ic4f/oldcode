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
    public class roleUsers : b.BasePage
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
            DataTable dtUsers = new d.CmsUserData().GetAllUsersByRole(getRoleId());
            cblUsers.DataSource = dtUsers;
            cblUsers.DataTextField = "Displayed";
            cblUsers.DataValueField = "Id";
            cblUsers.DataCheckField = "Selected";
            cblUsers.DataBind();
        }

        private int getRoleId()
        {
            return Convert.ToInt32(Request["RoleId"]);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ArrayList users = cblUsers.GetSelectedValues();
            d.CmsUserRoleLink curl = new d.CmsUserRoleLink();
            int roleId = getRoleId();
            curl.DeleteByRole(roleId);
            foreach (string userId in users)
                curl.Create(Convert.ToInt32(userId), roleId);
            Response.Redirect("roleList.aspx");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("roleList.aspx");
        }

        protected override int PageId { get { return 40; } }

        protected override void OnInit(EventArgs e)
        {
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            base.OnInit(e);
        }
    }
}
