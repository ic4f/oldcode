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
using c = Core;
using d = Ei.Data;

namespace Ei.Website.settings
{
    public class userArchive : b.BasePage
    {
        protected c.MySortGrid dgrUsers;
        protected Panel pnlGrid;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            dgrUsers.BindingMethod = new c.BindingDelegate(BindData);
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            DataTable dtUsers = new d.CmsUserData().GetArchivedUsers(dgrUsers.SortExpression);
            if (dtUsers.Rows.Count > 0)
            {
                pnlGrid.Visible = true;
                dgrUsers.Prefix = LinkPrefix;
                dgrUsers.DataSource = dtUsers;
                dgrUsers.DataBind();
            }
            else
                pnlGrid.Visible = false;
        }

        private void dgrUsers_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                LinkButton lnkActivate = (LinkButton)e.Item.Cells[5].Controls[0];
                lnkActivate.CssClass = "gridlinkalert";

                string name = DataBinder.Eval(e.Item.DataItem, "DisplayedName").ToString().Replace("'", "");
                lnkActivate.Attributes["onclick"] = "javascript:return confirm('Are you sure you want to activate " + name + " account?')";
            }
        }

        private void dgrUsers_ItemCommand(object sender, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Activate")
            {
                int id = Convert.ToInt32(dgrUsers.DataKeys[e.Item.ItemIndex]);
                new d.CmsUserData().Activate(id);
                Response.Redirect("userList.aspx");
            }
        }

        protected override int PageId { get { return 35; } }

        protected override void OnInit(EventArgs e)
        {
            this.dgrUsers.ItemCommand += new DataGridCommandEventHandler(dgrUsers_ItemCommand);
            this.dgrUsers.ItemDataBound += new DataGridItemEventHandler(dgrUsers_ItemDataBound);
            base.OnInit(e);
        }
    }
}
