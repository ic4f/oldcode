using System;
using System.Collections;
using System.Text;
using System.Data;
using c = Core;

namespace Foundation.BusinessAdmin
{
    public class PublicMenuTree : c.Tree
    {
        public static string HTML_DIV_PREFIX = "pub_menu_";
        public static string HTML_IMG_PREFIX = "pub_menuimage_";

        public static string[,] ConvertData(DataTable dt)
        {
            int count = dt.Rows.Count;
            string[,] menu = new string[count, 5];
            DataRow dr;
            for (int i = 0; i < count; i++)
            {
                dr = dt.Rows[i];
                menu[i, 0] = dr[0].ToString();  //id
                menu[i, 1] = dr[1].ToString();  //parentId
                menu[i, 2] = dr[2].ToString();  //text
                menu[i, 3] = dr[3].ToString();  //rank
                menu[i, 4] = dr[4].ToString();  //pageId
            }
            return menu;
        }

        public PublicMenuTree(string[,] treeData, int currentId, string linkPrefix) :
            base(treeData, currentId)
        {
            this.linkPrefix = linkPrefix;
            maxDepth = ConfigurationHelper.PublicMenuLevels - 1;
            currSubtreeDepth = 1;
            loadSubtreeDepths();
        }

        public string LinkPrefix { get { return linkPrefix; } }

        public override c.TreeItem MakeTreeItem(int id, c.TreeItem parent, int level, string[] itemData)
        {
            return new PublicMenuTreeItem(id, parent, level, itemData);
        }

        public string GetMenuForCmsBrowsing()
        {
            return ((PublicMenuTreeItem)Root).GetMenuForCmsBrowsing(CurrentId, "menus.aspx?", true);
        }

        public string GetMenuForCmsParentSelection(int toEditId, int previousParentId)
        {
            return ((PublicMenuTreeItem)Root).GetMenuForCmsParentSelection(
                CurrentId, previousParentId, "menuChangeParent.aspx?ToEditId=" + toEditId + "&", false, maxDepth, currSubtreeDepth);
        }

        public string GetMenuForCmsMenuSelection()
        {
            PublicMenuTreeItem item = (PublicMenuTreeItem)GetItem(CurrentId);
            int parentId = c.Tree.ROOT_ID;
            if (item.Parent != null)
                parentId = item.Parent.Id;
            return ((PublicMenuTreeItem)Root).GetMenuForCmsMenuSelection(CurrentId, parentId);
        }

        public int CurrSubtreeDepth { get { return currSubtreeDepth; } }

        private void loadSubtreeDepths()
        {
            IEnumerator en = Items.GetEnumerator();
            PublicMenuTreeItem currItem;
            while (en.MoveNext())
            {
                int subtreedepth = 1;
                currItem = (PublicMenuTreeItem)((DictionaryEntry)en.Current).Value;
                while (currItem != null)
                {
                    currItem.UpdateSubtreeDepth(subtreedepth++);

                    if (currItem.Id == CurrentId)
                        currSubtreeDepth = currItem.SubtreeDepth;

                    currItem = (PublicMenuTreeItem)currItem.Parent;
                }
            }
        }

        private string linkPrefix;
        private int currSubtreeDepth;
        private int maxDepth;
    }
}