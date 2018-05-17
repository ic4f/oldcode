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

namespace Ei.Website.settings
{
    public class _default : b.BasePage
    {
        protected Panel pnlUsers;
        protected Panel pnlRoles;
        protected Panel pnlPhotos;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                displayContent();
        }

        private void displayContent()
        {
            pnlUsers.Visible = false;
            pnlRoles.Visible = false;
            pnlPhotos.Visible = false;
            if (HasPermission(b.PermissionCode.SystemSettings_UserRoles_Full))
            {
                pnlUsers.Visible = true;
                pnlRoles.Visible = true;
            }
            if (HasPermission(b.PermissionCode.SystemSettings_Photos_Full))
                pnlPhotos.Visible = true;
        }

        protected override int PageId { get { return 28; } }
    }
}
