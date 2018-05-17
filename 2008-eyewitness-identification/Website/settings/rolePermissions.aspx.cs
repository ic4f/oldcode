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
using cc = Core.Cms;

namespace Ei.Website.settings
{
    public class rolePermissions : b.BasePage
    {
        protected Repeater rptTopCats;
        protected Repeater rptCats;
        protected Panel pnlPermissions;
        protected Literal ltrPermissionTitle;
        protected Panel pnlConfirm;
        protected Button btnSave;
        protected Button btnCancel;

        private int catId;
        private int roleId;
        private cc.PermissionData permissionData;
        private d.RoleData roleData;
        private ArrayList arrExistingPermIds;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }

        private void BindData()
        {
            arrExistingPermIds = roleData.GetPermissions(roleId); //must reload
            rptTopCats.DataSource = permissionData.TopCategories;
            rptTopCats.DataBind();

            if (catId > 0)
            {
                d.Role role = new d.Role(roleId);
                cc.PermissionCategory cat = permissionData.GetCategory(catId);
                pnlPermissions.Visible = true;
                ltrPermissionTitle.Text = "Select <b>" + cat.Name + "</b> permissions for the <b>" + role.Name + "</b> role:";
                rptCats.DataSource = permissionData.GetCategories(catId);
                rptCats.DataBind();
            }
            else
                pnlPermissions.Visible = false;
        }

        private int getRoleId()
        {
            if (Request["RoleId"] != "")
                return Convert.ToInt32(Request["RoleId"]);
            return -1;
        }

        private int getCatId()
        {
            if (Request["CatId"] != "")
                return Convert.ToInt32(Request["CatId"]);
            return -1;
        }

        private void rptTopCats_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                cc.PermissionCategory cat = (cc.PermissionCategory)e.Item.DataItem;
                HyperLink lnkTopCat = (HyperLink)e.Item.FindControl("lnkTopCat");
                lnkTopCat.Text = cat.Name;
                lnkTopCat.NavigateUrl = "rolePermissions.aspx?RoleId=" + roleId + "&CatId=" + cat.Id;
                if (cat.Id == catId)
                    lnkTopCat.Style.Add("font-weight", "bold");
            }
        }

        private void rptCats_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                cc.PermissionCategory cat = (cc.PermissionCategory)e.Item.DataItem;
                Label lblCat = (Label)e.Item.FindControl("lblCat");
                lblCat.Text = cat.Name;
                ArrayList arrPerms = permissionData.GetPermissions(cat.Id);
                Panel pnlPerms = (Panel)e.Item.FindControl("pnlPerms");
                LiteralControl ltrNone = new LiteralControl();
                pnlPerms.Controls.Add(ltrNone);
                string selected = "";
                bool exists = false;

                foreach (cc.Permission perm in arrPerms)
                {
                    if (arrExistingPermIds.Contains(perm.Id))
                    {
                        selected = "checked";
                        exists = true;
                    }
                    else
                        selected = "";

                    string radio = "<input type='radio' name='radPerm_" + cat.Id + "' value='" + perm.Id + "' " + selected + ">";
                    pnlPerms.Controls.Add(new LiteralControl(radio + perm.Name + "<br>"));
                }
                if (!exists)
                    selected = "checked";

                ltrNone.Text = "<input type='radio' name='radPerm_" + cat.Id + "' value='0' " + selected + ">none<br>";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ArrayList arrCats = permissionData.GetCategories(catId);
            foreach (cc.PermissionCategory cat in arrCats)
            {
                ArrayList arrPerms = permissionData.GetPermissions(cat.Id);
                foreach (cc.Permission perm in arrPerms)
                    roleData.DeletePermission(roleId, perm.Id);

                int permId = Convert.ToInt32(Page.Request["radPerm_" + cat.Id]);
                if (permId > 0)
                    roleData.AddPermission(roleId, permId);
            }
            pnlConfirm.Visible = true;
            BindData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlConfirm.Visible = false;
            BindData();
        }

        protected override int PageId { get { return 41; } }

        protected override void OnInit(EventArgs e)
        {
            roleId = getRoleId();
            catId = getCatId();
            permissionData = new cc.PermissionData();
            roleData = new d.RoleData();
            arrExistingPermIds = roleData.GetPermissions(roleId);
            this.rptTopCats.ItemDataBound += new RepeaterItemEventHandler(rptTopCats_ItemDataBound);
            this.rptCats.ItemDataBound += new RepeaterItemEventHandler(rptCats_ItemDataBound);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            base.OnInit(e);
        }
    }
}
