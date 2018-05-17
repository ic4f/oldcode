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
    public class caseAdd : b.AddBasePage
    {
        protected TextBox tbxNumber;
        protected TextBox tbxDescription;
        protected CustomValidator valUnique;

        protected override void Page_Load(object sender, System.EventArgs e) { }

        protected override string ItemName { get { return "case"; } }

        protected override string RedirectPage { get { return "caseList.aspx"; } }

        protected override int SaveItem()
        {
            int caseId = new d.CaseData().Create(tbxNumber.Text.Trim(), tbxDescription.Text.Trim(), Identity.UserId);
            if (caseId < 0)
            {
                valUnique.IsValid = false;
                return 0;
            }
            return 1;
        }

        protected override void ResetFormFields()
        {
            tbxNumber.Text = "";
            tbxDescription.Text = "";
        }

        protected override int PageId { get { return 13; } }
    }
}
