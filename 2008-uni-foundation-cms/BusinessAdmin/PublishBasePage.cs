using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using dt = Foundation.Data;
using ba = Foundation.BusinessAdmin;

namespace Foundation.BusinessAdmin
{
    public abstract class PublishBasePage : EditBasePage
    {
        protected Button btnPublish;

        private void btnPublish_Click(object sender, EventArgs e)
        {
            pnlConfirm.Visible = true;
            int result = SaveItem();
            if (result > 0)
            {
                ltrConfirmMessage.Text = ItemName + " has been published";
                ResetForm(true, false, false);
                ResetFormFields();
                PublishItem();
            }
            else
                ResetForm(false, true, true);
        }

        protected virtual void PublishItem()
        {
            int pageId = GetId();
            new dt.PageData().Publish(pageId, Identity.UserId);
            new ba.ContextLinkAnalyzer().UpdateContextualLinks(pageId);
        }

        protected override void OnInit(EventArgs e)
        {
            this.btnPublish.Click += new EventHandler(btnPublish_Click);
            base.OnInit(e);
        }
    }
}
