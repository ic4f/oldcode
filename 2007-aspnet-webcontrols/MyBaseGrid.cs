using System;
using System.Web.UI.WebControls;

namespace WebControls
{
    public class MyBaseGrid : DataGrid
    {
        public const string IMAGE_ASC = "../_gridhelper/arrow_up.gif";
        public const string IMAGE_DESC = "../_gridhelper/arrow_down.gif";
        public const string JS_MOUSEOVER = "GridRowMouseover";

        private void MyGrid_ItemDataBound(Object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                e.Item.Attributes.Add("id", e.Item.UniqueID);
                e.Item.Attributes.Add("OnMouseOver", JS_MOUSEOVER + "(\"" + e.Item.UniqueID + "\", true);");
                e.Item.Attributes.Add("OnMouseOut", JS_MOUSEOVER + "(\"" + e.Item.UniqueID + "\", false);");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.ItemDataBound += new DataGridItemEventHandler(MyGrid_ItemDataBound);
            this.AutoGenerateColumns = false;
            this.HeaderStyle.CssClass = "gridTableHeader";
            this.CellPadding = 3;
            base.OnInit(e);
        }
    }
}
