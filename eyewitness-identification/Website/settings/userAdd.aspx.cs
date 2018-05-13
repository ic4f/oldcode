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
using b = Ei.Business;
using d = Ei.Data;

namespace Ei.Website.settings
{
    public class userAdd : b.AddBasePage
    {
        protected TextBox tbxEmail;
        protected TextBox tbxFirstName;
        protected TextBox tbxLastName;
        protected CustomValidator valUnique;

        protected override void Page_Load(object sender, System.EventArgs e) { }

        protected override string ItemName { get { return "user"; } }

        protected override string RedirectPage { get { return "userList.aspx"; } }

        protected override int SaveItem()
        {
            int cmsUserId = new d.CmsUserData().Create(
                tbxEmail.Text.Trim(), generatePassword(8), tbxFirstName.Text.Trim(), "", tbxLastName.Text.Trim(), Identity.UserId);
            if (cmsUserId < 0)
            {
                valUnique.IsValid = false;
                return 0;
            }
            return 1;
        }

        protected override void ResetFormFields()
        {
            tbxFirstName.Text = "";
            tbxLastName.Text = "";
            tbxEmail.Text = "";
        }

        private string generatePassword(int length)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < length; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(26 * random.NextDouble() + 65));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        protected override int PageId { get { return 30; } }
    }
}
