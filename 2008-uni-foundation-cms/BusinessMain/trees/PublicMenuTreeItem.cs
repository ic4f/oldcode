using System;
using System.Collections;
using System.Text;
using c = Core;

namespace Foundation.BusinessMain
{
    public class PublicMenuTreeItem : c.TreeItem
    {
        public static string RADIOBUTTON_NAME = "radmenu";

        public PublicMenuTreeItem(int id, c.TreeItem parent, int depth, string[] itemData) :
            base(id, parent, depth, itemData)
        { }

        public string Text { get { return text; } }

        public string Url { get { return url; } }

        public string GetMenu(int currentItemId)
        {
            return getMenuHelper(currentItemId, false);
        }

        protected override void LoadData(string[] itemData)
        {
            text = itemData[0];
            url = itemData[1];
        }

        protected override int IndentPixels { get { return 20; } }

        public string GetMainMenuForDisplay()
        {
            StringBuilder sb = new StringBuilder();

            PublicMenuTreeItem currMenu = this;
            int menuAccumulatedId = -2;
            string menuAccum = "";

            while (currMenu != null)
            {
                menuAccum = makeItemMenu(currMenu, menuAccumulatedId, menuAccum); // + counter2++;
                menuAccumulatedId = currMenu.Id;
                currMenu = (PublicMenuTreeItem)currMenu.Parent;
            }
            return menuAccum;
        }

        private string makeItemMenu(PublicMenuTreeItem currentItem, int menuAccumulatedId, string menuAccum)
        {
            StringBuilder sb1 = new StringBuilder();
            sb1.AppendFormat("\n<div class='menu{0}-div'>", currentItem.Depth);

            string menustatus = "";
            if (currentItem.Id == Id)
                menustatus = "_a";
            sb1.AppendFormat(" <a class='menu{0}-link{1}' href='{2}'>{3}</a></div>", currentItem.Depth, menustatus, currentItem.Url, currentItem.Text);

            foreach (PublicMenuTreeItem item in currentItem.Children)
            {
                if (item.Id == menuAccumulatedId)
                    sb1.Append(menuAccum);
                else
                {
                    sb1.AppendFormat("\n<div class='menu{0}-div'>", item.Depth);
                    menustatus = "";
                    if (currentItem.Id == item.Id)
                        menustatus = "_a";
                    sb1.AppendFormat(" <a class='menu{0}-link{1}' href='{2}'>{3}</a></div>", item.Depth, menustatus, item.url, item.Text);
                }
            }
            return sb1.ToString();
        }

        #region getMenuHelper
        private string getMenuHelper(int currentItemId, bool highlight)
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            if (Parent != null)
            {
                sb1.AppendFormat("\n<div class='menu{0}-div'>", Depth);

                string menustatus = "";
                if (currentItemId == Id)
                    menustatus = "_a";

                sb1.AppendFormat(" <a class='menu{0}-link{1}' href='{2}'>{3}</a></div>", Depth, menustatus, Url, Text);

                if (currentItemId == Id)
                {
                    if (Children.Count > 0)
                        foreach (PublicMenuTreeItem item in Children)
                            sb2.Append(item.getMenuHelper(currentItemId, true));
                }
            }
            else
            {
                foreach (PublicMenuTreeItem item in Children)
                    sb2.Append(item.getMenuHelper(currentItemId, true));
            }
            return sb1.ToString() + sb2.ToString();
        }
        #endregion

        private string text;
        private string url;
    }
}
