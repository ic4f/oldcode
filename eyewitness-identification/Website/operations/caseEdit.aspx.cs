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
    public class caseEdit : b.EditBasePage
    {
        protected TextBox tbxNumber;
        protected TextBox tbxDescription;
        protected CustomValidator valUnique;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            d.CCase ccase = new d.CCase(GetId());
            tbxNumber.Text = ccase.Number;
            tbxDescription.Text = ccase.Description;
        }

        protected override int SaveItem()
        {
            d.CCase ccase = new d.CCase(GetId());
            ccase.Number = tbxNumber.Text.Trim();
            ccase.Description = tbxDescription.Text.Trim();

            if (ccase.Update() < 0)
            {
                valUnique.IsValid = false;
                return 0;
            }
            return 1;
        }

        protected override void ResetFormFields() { BindData(); }

        protected override string ItemName { get { return "case"; } }

        protected override string RedirectPage { get { return "caseList.aspx"; } }

        protected override int PageId { get { return 16; } }
    }
}
