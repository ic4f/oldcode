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
    public class roleEdit : b.EditBasePage
    {
        protected TextBox tbxRole;
        protected CustomValidator valUnique;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            d.Role role = new d.Role(GetId());
            tbxRole.Text = role.Name;
        }

        protected override int SaveItem()
        {
            d.Role role = new d.Role(GetId());
            role.Name = tbxRole.Text.Trim();

            if (role.Update() < 0)
            {
                valUnique.IsValid = false;
                return 0;
            }
            return 1;
        }

        protected override void ResetFormFields() { BindData(); }

        protected override string ItemName { get { return "role"; } }

        protected override string RedirectPage { get { return "roleList.aspx"; } }

        protected override int PageId { get { return 39; } }
    }
}
