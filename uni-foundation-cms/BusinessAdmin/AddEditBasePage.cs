using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Foundation.BusinessAdmin
{
    public abstract class AddEditBasePage : BasePage
    {
        protected Panel pnlConfirm;
        protected Literal ltrConfirmMessage;
        protected Button btnReturn;

        protected Panel pnlError;
        protected Literal ltrErrorMessage;

        protected Panel pnlForm;
        protected Button btnSave;
        protected Button btnCancel;

        protected abstract string ItemName { get; }

        protected abstract string RedirectPage { get; }

        protected abstract string Message { get; }

        protected abstract int SaveItem();

        protected abstract void ResetFormFields();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Page.IsPostBack)
            {
                btnCancel.CausesValidation = false;
                ltrErrorMessage.Text = "There were errors in your form. Please review the messages below";
                ResetForm(false, false, true);
            }
        }

        protected void ResetForm(bool showConfirm, bool showError, bool showForm)
        {
            pnlConfirm.Visible = showConfirm;
            pnlError.Visible = showError;
            pnlForm.Visible = showForm;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            pnlConfirm.Visible = true;
            if (Page.IsValid && SaveItem() > 0)
            {
                ltrConfirmMessage.Text = ItemName + " has been " + Message;
                ResetForm(true, false, false);
                ResetFormFields();
            }
            else
                ResetForm(false, true, true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(RedirectPage);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(RedirectPage);
        }

        protected override void OnInit(EventArgs e)
        {
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.btnReturn.Click += new EventHandler(btnReturn_Click);
            base.OnInit(e);
        }
    }
}
