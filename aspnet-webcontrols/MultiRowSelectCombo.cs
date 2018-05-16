using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebControls
{
    public class MultiRowSelectCombo : Combo
    {
        public static string JS_CONFIRM_UPDATE = "ComboConfirmUpdate";
        public static string JS_SELECTION_CHANGED = "ComboSelectionChanged";
        public static string JS_DISABLE_UPDATE_RESET = "ComboDisableUpdateReset";

        #region public methods and events

        public void LoadUpdates(ArrayList toAdd, ArrayList toDelete)
        {
            string temp = Page.Request[FieldIdList];
            string[] idList = temp.Split(new Char[] { ',' });
            for (int i = 0; i < idList.Length; i++)
            {
                int id = Convert.ToInt32(idList[i]);
                if (Page.Request[prefixRowCheckboxId + i] == "on")
                    toAdd.Add(id);
                else
                    toDelete.Add(id);
            }
        }

        public event CommandEventHandler UpdateCommand;

        public event CommandEventHandler ResetCommand;

        public event CommandEventHandler AddAllCommand;

        public event CommandEventHandler RemoveAllCommand;

        public BoundColumn AddSelectField(IDataField field)
        {
            BoundColumn c = AddField(field, "center");
            c.ItemStyle.Width = new Unit(60);
            selectColumnIndex = LastAddedColumnIndex;
            return c;
        }

        public bool SortBySelectColumn
        {
            set { sortBySelect = value; }
            get { return sortBySelect; }
        }

        #endregion

        #region protected variables (controls)

        protected Button btnUpdate;
        protected Button btnReset;
        protected Button btnAddAll;
        protected Button btnRemoveAll;

        #endregion

        #region protected methods

        protected void button_UpdateCommand(object sender, CommandEventArgs e) { UpdateCommand(sender, e); }

        protected void button_ResetCommand(object sender, CommandEventArgs e) { ResetCommand(sender, e); }

        protected void button_AddAllCommand(object sender, CommandEventArgs e) { AddAllCommand(sender, e); }

        protected void button_RemoveAllCommand(object sender, CommandEventArgs e) { RemoveAllCommand(sender, e); }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            sortBySelect = true;

            prefixRowCheckboxId = this.ID + "_selection_";

            btnUpdate = new Button();
            btnUpdate.ID = GetJsUpdateButtonId();
            btnUpdate.Text = "Update";
            btnUpdate.Command += new CommandEventHandler(button_UpdateCommand);
            btnUpdate.Attributes.Add("onClick", "javascript:return " + JS_CONFIRM_UPDATE + "(0);");
            btnUpdate.Enabled = false;

            btnReset = new Button();
            btnReset.ID = GetJsResetButtonId();
            btnReset.Text = "Reset";
            btnReset.Command += new CommandEventHandler(button_ResetCommand);
            btnReset.Attributes.Add("onClick", "javascript:return " + JS_CONFIRM_UPDATE + "(1);");
            btnReset.Enabled = false;

            btnAddAll = new Button();
            btnAddAll.ID = GetJsAddAllButtonId();
            btnAddAll.Text = "Add All";
            btnAddAll.Command += new CommandEventHandler(button_AddAllCommand);
            btnAddAll.Attributes.Add("onClick", "javascript:return " + JS_CONFIRM_UPDATE + "(2);");

            btnRemoveAll = new Button();
            btnRemoveAll.ID = GetJsRemoveAllButtonId();
            btnRemoveAll.Text = "Remove All";
            btnRemoveAll.Command += new CommandEventHandler(button_RemoveAllCommand);
            btnRemoveAll.Attributes.Add("onClick", "javascript:return " + JS_CONFIRM_UPDATE + "(3);");

            AddTableCommandButton(btnUpdate);
            AddTableCommandButton(btnReset);
            AddSpacer(7);
            AddTableCommandButton(btnAddAll);
            AddTableCommandButton(btnRemoveAll);
        }

        protected override void BindGridItem(DataGridItemEventArgs e, int rowIndex)
        {
            base.BindGridItem(e, rowIndex);
            string boxChecked = "";
            if (e.Item.Cells[selectColumnIndex].Text == "True")
                boxChecked = "checked";

            e.Item.Cells[selectColumnIndex].Text = "<input name=\"" + prefixRowCheckboxId + rowIndex + "\" type=\"checkbox\" " + boxChecked + " onKeyDown=\"" + getEnterKeyBlockJs() + ";\" onClick=\"" + JS_SELECTION_CHANGED + "(" + e.Item.ItemIndex + ");\">";
        }

        private string getEnterKeyBlockJs()
        {
            return "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {return false;}} ";
        }

        protected override string makeHeadeLink(int columnIndex, TableCellCollection cells)
        {
            if (!sortBySelect && columnIndex == selectColumnIndex)
                return cells[columnIndex].Text;
            else
                return base.makeHeadeLink(columnIndex, cells);
        }

        protected override string GetJsUpdateButtonId() { return "btnUpdate"; }

        protected override string GetJsResetButtonId() { return "btnReset"; }

        protected override string GetJsAddAllButtonId() { return "btnAddAll"; }

        protected override string GetJsRemoveAllButtonId() { return "btnRemoveAll"; }

        #endregion

        #region private variables

        private int selectColumnIndex;
        private bool sortBySelect;
        private string prefixRowCheckboxId;

        #endregion
    }
}
