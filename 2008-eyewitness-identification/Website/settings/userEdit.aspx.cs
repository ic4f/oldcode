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
    public class userEdit : b.EditBasePage
    {
        protected TextBox tbxEmail;
        protected TextBox tbxPassword;
        protected TextBox tbxFirstName;
        protected TextBox tbxLastName;
        protected CustomValidator valUnique;
        protected TextBox tbxDisplayedName;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            d.CmsUser user = new d.CmsUser(GetId());
            tbxEmail.Text = user.Login;
            tbxFirstName.Text = user.FirstName;
            tbxLastName.Text = user.LastName;
            tbxDisplayedName.Text = user.DisplayedName;
        }

        protected override int SaveItem()
        {
            d.CmsUser user = new d.CmsUser(GetId());
            if (tbxPassword.Text.Trim() != "")
                user.Password = tbxPassword.Text.Trim();
            user.Login = tbxEmail.Text.Trim();
            user.FirstName = tbxFirstName.Text.Trim();
            user.LastName = tbxLastName.Text.Trim();
            user.DisplayedName = tbxDisplayedName.Text.Trim();

            if (user.Update() < 0)
            {
                valUnique.IsValid = false;
                return 0;
            }
            return 1;
        }

        protected override void ResetFormFields() { BindData(); }

        protected override string ItemName { get { return "user"; } }

        protected override string RedirectPage { get { return "userList.aspx"; } }

        protected override int PageId { get { return 33; } }
    }
}
