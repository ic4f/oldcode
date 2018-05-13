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

namespace Ei.Website.settings
{
    public class roleAdd : b.AddBasePage
    {
        protected TextBox tbxRole;
        protected CustomValidator valUnique;

        protected override void Page_Load(object sender, System.EventArgs e) { }

        protected override string ItemName { get { return "role"; } }

        protected override string RedirectPage { get { return "roleList.aspx"; } }

        protected override int SaveItem()
        {
            int roleId = new d.RoleData().Create(tbxRole.Text.Trim(), Identity.UserId);
            if (roleId < 0)
            {
                valUnique.IsValid = false;
                return 0;
            }
            return 1;
        }

        protected override void ResetFormFields()
        {
            tbxRole.Text = "";
        }

        protected override int PageId { get { return 37; } }
    }
}
