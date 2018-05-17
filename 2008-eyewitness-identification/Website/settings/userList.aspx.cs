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
    public class userList : b.BasePage
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
            DataTable dtUsers = new d.CmsUserData().GetRecords(dgrUsers.SortExpression);
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
                HyperLink lnkRoles = (HyperLink)e.Item.Cells[4].Controls[0];
                HyperLink lnkCases = (HyperLink)e.Item.Cells[5].Controls[0];
                HyperLink lnkEdit = (HyperLink)e.Item.Cells[6].Controls[0];
                LinkButton lnkDelete = (LinkButton)e.Item.Cells[7].Controls[0];
                lnkRoles.CssClass = "gridlink";
                lnkCases.CssClass = "gridlink";
                lnkEdit.CssClass = "gridlink";
                lnkDelete.CssClass = "gridlinkalert";

                int cmsuserId = Convert.ToInt16(dgrUsers.DataKeys[e.Item.ItemIndex]);
                if (cmsuserId == d.CmsUserData.Administrator_CmsUser_Id)
                {
                    lnkRoles.Visible = false;
                    lnkDelete.Visible = false;
                }
                else
                {
                    string name = DataBinder.Eval(e.Item.DataItem, "DisplayedName").ToString().Replace("'", "");
                    lnkDelete.Attributes["onclick"] = "javascript:return confirm('Are you sure you want to remove " + name + " from CMS users?')";
                }
            }
        }

        private void dgrUsers_ItemCommand(object sender, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int id = Convert.ToInt32(dgrUsers.DataKeys[e.Item.ItemIndex]);
                new d.CmsUserData().Remove(id);
                BindData();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.dgrUsers.ItemCommand += new DataGridCommandEventHandler(dgrUsers_ItemCommand);
            this.dgrUsers.ItemDataBound += new DataGridItemEventHandler(dgrUsers_ItemDataBound);
            base.OnInit(e);
        }

        protected override int PageId { get { return 31; } }
    }
}
