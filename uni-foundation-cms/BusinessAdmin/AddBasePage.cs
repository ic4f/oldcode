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
    public abstract class AddBasePage : AddEditBasePage
    {
        protected Button btnAddAnother;

        protected override string Message { get { return "created"; } }

        private void btnAddAnother_Click(object sender, EventArgs e)
        {
            ResetForm(false, false, true);
        }

        protected override void OnInit(EventArgs e)
        {
            this.btnAddAnother.Click += new EventHandler(btnAddAnother_Click);
            base.OnInit(e);
        }
    }
}
