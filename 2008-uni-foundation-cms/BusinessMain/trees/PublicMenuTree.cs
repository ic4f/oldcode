using System;
using System.Collections;
using System.Text;
using System.Data;
using c = Core;

namespace Foundation.BusinessMain
{
    public class PublicMenuTree : c.Tree
    {
        public static string HTML_DIV_PREFIX = "pub_menu_";
        public static string HTML_IMG_PREFIX = "pub_menuimage_";

        public static string[,] ConvertData(DataTable dt)
        {
            int count = dt.Rows.Count;
            string[,] menu = new string[count, 4];
            DataRow dr;
            for (int i = 0; i < count; i++)
            {
                dr = dt.Rows[i];
                menu[i, 0] = dr[0].ToString();  //id
                menu[i, 1] = dr[1].ToString();  //parentId
                menu[i, 2] = dr[2].ToString();  //text
                menu[i, 3] = dr[3].ToString();  //url
            }
            return menu;
        }

        public PublicMenuTree(string[,] treeData, int currentId, string linkPrefix) :
            base(treeData, currentId)
        {
            this.linkPrefix = linkPrefix;
        }

        public string LinkPrefix { get { return linkPrefix; } }

        public override c.TreeItem MakeTreeItem(int id, c.TreeItem parent, int level, string[] itemData)
        {
            return new PublicMenuTreeItem(id, parent, level, itemData);
        }

        public string GetMainMenuForDisplay()
        {
            PublicMenuTreeItem currentItem = (PublicMenuTreeItem)GetItem(CurrentId);
            return currentItem.GetMainMenuForDisplay();
        }

        public string GetMenu()
        {
            return ((PublicMenuTreeItem)Root).GetMenu(CurrentId);
        }

        private string linkPrefix;
    }
}