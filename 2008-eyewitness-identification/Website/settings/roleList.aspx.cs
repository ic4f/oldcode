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
    public class roleList : b.BasePage
    {
        protected c.MySortGrid dgrRoles;
        protected Panel pnlGrid;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            dgrRoles.BindingMethod = new c.BindingDelegate(BindData);
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            DataTable dtRoles = new d.RoleData().GetRecords(dgrRoles.SortExpression);
            if (dtRoles.Rows.Count > 0)
            {
                pnlGrid.Visible = true;
                dgrRoles.Prefix = LinkPrefix;
                dgrRoles.DataSource = dtRoles;
                dgrRoles.DataBind();
            }
            else
                pnlGrid.Visible = false;
        }

        private void dgrRoles_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                HyperLink lnkPerms = (HyperLink)e.Item.Cells[4].Controls[0];
                HyperLink lnkUsers = (HyperLink)e.Item.Cells[5].Controls[0];
                HyperLink lnkEdit = (HyperLink)e.Item.Cells[6].Controls[0];
                LinkButton lnkDelete = (LinkButton)e.Item.Cells[7].Controls[0];
                lnkPerms.CssClass = "gridlink";
                lnkUsers.CssClass = "gridlink";
                lnkEdit.CssClass = "gridlink";
                lnkDelete.CssClass = "gridlinkalert";

                int roleId = Convert.ToInt16(dgrRoles.DataKeys[e.Item.ItemIndex]);
                if (roleId == d.RoleData.Administrator_Role_Id)
                {
                    lnkPerms.Visible = false;
                    lnkUsers.Visible = false;
                    lnkEdit.Visible = false;
                    lnkDelete.Visible = false;
                }
                else
                {
                    string role = DataBinder.Eval(e.Item.DataItem, "Name").ToString().Replace("'", "");
                    lnkDelete.Attributes["onclick"] = "javascript:return confirm('Are you sure you want to delete the " + role + " role?')";
                }
            }
        }

        private void dgrRoles_ItemCommand(object sender, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int id = Convert.ToInt32(dgrRoles.DataKeys[e.Item.ItemIndex]);
                new d.RoleData().Delete(id);
                BindData();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.dgrRoles.ItemCommand += new DataGridCommandEventHandler(dgrRoles_ItemCommand);
            this.dgrRoles.ItemDataBound += new DataGridItemEventHandler(dgrRoles_ItemDataBound);
            base.OnInit(e);
        }

        protected override int PageId { get { return 38; } }
    }
}
