using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;

namespace WebControls
{
    public class DataCheckboxList : CheckBoxList
    {
        private string dataCheck;

        public string DataCheckField
        {
            get { return dataCheck; }
            set { dataCheck = value; }
        }

        public ArrayList GetSelectedValues()
        {
            ArrayList values = new ArrayList();
            foreach (ListItem i in Items)
                if (i.Selected)
                    values.Add(i.Value);
            return values;
        }

        public ArrayList GetNonSelectedValues()
        {
            ArrayList values = new ArrayList();
            foreach (ListItem i in Items)
                if (!i.Selected)
                    values.Add(i.Value);
            return values;
        }

        public bool ValuesSelected()
        {
            foreach (ListItem i in Items)
                if (!i.Selected)
                    return true;
            return false;
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            DataTable dt = DataSource as DataTable;
            if (dt != null)
                for (int i = 0; i < dt.Rows.Count; i++)
                    Items[i].Selected = Convert.ToBoolean(dt.Rows[i][DataCheckField]);
            else
            {
                IDataTable idt = DataSource as IDataTable;
                if (idt != null)
                    for (int i = 0; i < idt.Rows.Count; i++)
                    {
                        IDataRow idr = (IDataRow)idt.Rows[i];
                        Items[i].Selected = idr.Selected;
                    }
            }
        }
    }
}