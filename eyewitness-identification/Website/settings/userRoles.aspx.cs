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
    public class userRoles : b.BasePage
    {
        protected c.DataCheckboxList cblRoles;
        protected Button btnSave;
        protected Button btnCancel;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            DataTable dtRoles = new d.RoleData().GetAllRolesByUser(getUserId());
            cblRoles.DataSource = dtRoles;
            cblRoles.DataTextField = "Name";
            cblRoles.DataValueField = "Id";
            cblRoles.DataCheckField = "Selected";
            cblRoles.DataBind();
        }

        private int getUserId()
        {
            return Convert.ToInt32(Request["CmsUserId"]);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ArrayList roles = cblRoles.GetSelectedValues();
            d.CmsUserRoleLink curl = new d.CmsUserRoleLink();
            int userId = getUserId();
            curl.DeleteByUser(userId);
            foreach (string roleId in roles)
                curl.Create(userId, Convert.ToInt32(roleId));
            Response.Redirect("userList.aspx");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("userList.aspx");
        }

        protected override int PageId { get { return 34; } }

        protected override void OnInit(EventArgs e)
        {
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            base.OnInit(e);
        }
    }
}
