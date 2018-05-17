using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Core
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
            DataTable dt = (DataTable)DataSource;
            for (int i = 0; i < dt.Rows.Count; i++)
                Items[i].Selected = Convert.ToBoolean(dt.Rows[i][DataCheckField]);
        }
    }
}