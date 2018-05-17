using System;
using System.Collections;
using System.Text;
using System.Data;
using dt = Foundation.Data;

namespace Foundation.BusinessMain
{
    public class MenuHelper
    {
        public static int NO_PARENT = -1;

        public MenuHelper()
        {
            menus = new Hashtable();
        }

        public string GetContextMenuHtml(string pageTitle, int menuId, DataTable dtMenu)
        {
            int id;
            foreach (DataRow dr in dtMenu.Rows)
            {
                id = Convert.ToInt32(dr[0]);
                MenuRecord mr = new MenuRecord(id, Convert.ToInt32(dr[1]), dr[2].ToString(), dr[3].ToString());
                menus.Add(id, mr);
            }

            StringBuilder sb = new StringBuilder();
            MenuRecord mr1;
            while (menuId != NO_PARENT)
            {
                mr1 = (MenuRecord)menus[menuId];
                sb.Insert(0, "<a href='" + mr1.Url + "' class='contextmenu-link'>" + mr1.Text + "</a>&nbsp;&nbsp;>&nbsp;");
                menuId = mr1.ParentId;
            }
            sb.AppendFormat("<a class='contextmenu-link'>{0}</a>", pageTitle);
            sb.Insert(0, "<a href='default.aspx' class='contextmenu-link'>Home</a>&nbsp;&nbsp;>&nbsp;");
            return sb.ToString();
        }

        public struct MenuRecord
        {
            public MenuRecord(int id, int parentId, string text, string url)
            {
                this.id = id;
                this.parentId = parentId;
                this.text = text;
                this.url = url;
            }

            public int Id { get { return id; } }
            public int ParentId { get { return parentId; } }
            public string Text { get { return text; } }
            public string Url { get { return url; } }

            private int id;
            private int parentId;
            private string text;
            private string url;
        }

        private Hashtable menus;
    }
}
