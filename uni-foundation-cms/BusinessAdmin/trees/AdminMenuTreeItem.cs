using System;
using System.Collections;
using System.Text;
using c = Core;

namespace Foundation.BusinessAdmin
{
    public class AdminMenuTreeItem : c.TreeItem
    {
        public AdminMenuTreeItem(int id, c.TreeItem parent, int depth, string[] itemData) :
            base(id, parent, depth, itemData)
        { }

        public string Text { get { return text; } }

        public string TargetUrl { get { return targetUrl; } }

        public string GetMenu(int currentItemId)
        {
            return getMenuHelper(currentItemId, false);
        }

        protected override void LoadData(string[] itemData)
        {
            text = itemData[0];
            targetUrl = itemData[1];
        }

        protected override int IndentPixels { get { return 20; } }

        private string getMenuHelper(int currentItemId, bool highlight)
        {
            string divId = AdminMenuTree.HTML_DIV_PREFIX + Id;
            AdminMenuTree parentTree = (AdminMenuTree)ParentTree;

            StringBuilder sb1 = new StringBuilder();

            if (Parent != null)
            {
                string link = " href='" + parentTree.LinkPrefix + TargetUrl + "'";
                string imageId = AdminMenuTree.HTML_IMG_PREFIX + Id;

                string margin = "2";
                if (Parent.Id == -1) margin = "12";
                sb1.AppendFormat("\n<div style=\"margin-left:{0}px;margin-top:{1}px;\">", GetIndent(), margin);

                if (Children.Count > 0)
                {
                    sb1.AppendFormat("<a href=\"#\" onclick=\"javascript:expand('{0}', '{1}', '{2}');\">", divId, imageId, parentTree.LinkPrefix);
                    if (!ParentTree.ItemIsParent(Id))
                        sb1.AppendFormat("<img id=\"{0}\" src=\"{1}_system/images/layout/plus.gif\" border=\"0\"></a>", imageId, parentTree.LinkPrefix);
                    else
                        sb1.AppendFormat("<img id=\"{0}\" src=\"{1}_system/images/layout/minus.gif\" border=\"0\"></a>", imageId, parentTree.LinkPrefix);
                }
                else
                    sb1.AppendFormat("<img src=\"{0}_system/images/layout/none.gif\">", parentTree.LinkPrefix);


                if (Parent.Id == c.Tree.ROOT_ID)
                {
                    if (currentItemId == Id)
                        sb1.AppendFormat(" <span style=\"font-weight:bold;\"><a class=\"menutreelink_a\"{0}>{1}</a></span></div>", link, Text);
                    else
                        sb1.AppendFormat(" <a class=\"menutreelink\"{0}><span style=\"font-weight:bold;margin-top:50px\">{1}</span></a></div>", link, Text);
                }
                else
                {
                    if (currentItemId == Id)
                        sb1.AppendFormat(" <span style=\"font-weight:bold;\"><a class=\"menutreelink_a\"{0}>{1}</a></span></div>", link, Text);
                    else
                        if (highlight)
                        sb1.AppendFormat(" <a class=\"menutreelink_a\"{0}>{1}</a></div>", link, Text);
                    else
                        sb1.AppendFormat(" <a class=\"menutreelink\"{0}>{1}</a></div>", link, Text);
                }
            }

            StringBuilder sb2 = new StringBuilder();
            if (Children.Count > 0)
            {
                if (currentItemId == Id)
                    foreach (AdminMenuTreeItem item in Children)
                        sb2.Append(item.getMenuHelper(currentItemId, true));
                else
                    foreach (AdminMenuTreeItem item in Children)
                        sb2.Append(item.getMenuHelper(currentItemId, false));

                if (Parent != null)
                {
                    if (!ParentTree.ItemIsParent(Id))
                        sb2.Insert(0, "\n\t<div id=\"" + divId + "\" style=\"display:none;\">");
                    else
                        sb2.Insert(0, "\n\t<div id=\"" + divId + "\">");
                    sb2.Append("</div>");
                }
            }
            return sb1.ToString() + sb2.ToString();
        }

        private string text;
        private string targetUrl;
    }
}
