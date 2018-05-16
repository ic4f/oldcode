using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebControls
{
    public class RowSelectCombo : Combo
    {
        #region public methods
        public static string DeleteCommandName { get { return "Delete"; } }

        public Button AddRowCommandButton(string text, string commandName)
        {
            Button b = new Button();
            b.ID = "btn" + this.ID + commandName;
            b.Text = text;
            b.CommandName = commandName;
            if (commandName == DeleteCommandName)
                b.Attributes.Add("onClick", "javascript:return " + JS_CONFIRM_DELETE + "();");
            b.Command += new CommandEventHandler(button_Command);
            rowButtonHolder.Controls.Add(b);
            rowButtonHolder.Controls.Add(new LiteralControl("&nbsp;"));
            b.Enabled = false; //a button should be enabled ONLY by selecting a row!
            return b;
        }

        public Button AddRowCommandNewWindowButton(string text, string pageUrl, string pageName) //opens new window
        {
            Button b = new Button();
            b.ID = "btn" + this.ID + pageUrl;
            b.Text = text;

            if (!pageUrl.EndsWith("&"))
                pageUrl += "?";

            pageUrl += "Id=' + document.getElementById('" + HiddenFieldSelectedId + "').value";
            string openPage = "javascript:open('" + pageUrl + ", '','width=' + screen.width * 0.8 + ', height=' + screen.height * 0.8 + ', resizable, scrollbars'); return false";

            b.Attributes.Add("onClick", openPage);
            rowButtonHolder.Controls.Add(b);
            rowButtonHolder.Controls.Add(new LiteralControl("&nbsp;"));
            b.Enabled = false; //a button should be enabled ONLY by selecting a row! (replaces the old javascript call to disableButtons() )
            return b;
        }

        /// <summary>
        /// returns the value of the data key field of the selected row
        /// </summary>
        public object SelectedKey
        {
            get
            {
                string selectedId = Page.Request[HiddenFieldSelectedId];
                if (selectedId == "-1" || selectedId == null) return -1;
                else return selectedId;
            }
        }

        #endregion

        #region protected variables (controls)
        protected PlaceHolder rowButtonHolder;      //command buttons applicable to a selected record		
        #endregion

        #region protected methods
        protected override string InitJsRowActionButtonsArray(string nameOfJsButtonArray)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("\n\t\t\tvar {0} = new Array(", nameOfJsButtonArray);
            bool hasButtons = false;
            foreach (Control ctr in rowButtonHolder.Controls)
                if (ctr is Button)
                {
                    sb.AppendFormat("\"{0}\", ", ((Button)ctr).ID);
                    hasButtons = true;
                }

            if (hasButtons)
                sb.Remove(sb.Length - 2, 2);

            sb.Append(");");
            return sb.ToString();
        }

        protected override void BindGridItem(DataGridItemEventArgs e, int rowIndex)
        {
            base.BindGridItem(e, rowIndex);
            e.Item.Attributes.Add("onClick", JS_SELECT_ROW + "(" + rowIndex + ");");
        }

        protected override void AddControls(HtmlTextWriter writer)
        {
            rowButtonHolder.RenderControl(writer);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            rowButtonHolder = new PlaceHolder();
            Controls.Add(rowButtonHolder);
        }
        #endregion
    }
}
