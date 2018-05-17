using System;
using System.Collections;
using System.Text;
using System.Data;
using c = Core;

namespace Core.Cms
{
    public class AdminMenuTree : c.Tree
    {
        public static string HTML_DIV_PREFIX = "admin_menu_";
        public static string HTML_IMG_PREFIX = "admin_menuimage_";

        public AdminMenuTree(string[,] treeData, int currentId, string linkPrefix) :
            base(treeData, currentId)
        {
            this.linkPrefix = linkPrefix;
        }

        public string LinkPrefix { get { return linkPrefix; } }

        public override c.TreeItem MakeTreeItem(int id, c.TreeItem parent, int level, string[] itemData)
        {
            return new AdminMenuTreeItem(id, parent, level, itemData);
        }

        public string GetMenu()
        {
            return ((AdminMenuTreeItem)Root).GetMenu(CurrentId);
        }

        private string linkPrefix;
    }
}