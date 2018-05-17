using System;
using System.Collections;
using System.Text;
using c = Core;

namespace Foundation.BusinessAdmin
{
    public class PublicMenuTreeItem : c.TreeItem
    {
        public static string RADIOBUTTON_NAME = "radmenu";

        public PublicMenuTreeItem(int id, c.TreeItem parent, int depth, string[] itemData) :
            base(id, parent, depth, itemData)
        {
            subtreeDepth = 1;
        }

        public string Text { get { return text; } }

        public string GetMenuForCmsBrowsing(int currentItemId, string link, bool linkNonParents)
        {
            return getMenuForCmsBrowsingHelper(currentItemId, false, link, linkNonParents);
        }

        public string GetMenuForCmsParentSelection(int currentItemId, int previousParentId, string link, bool linkNonParents, int maxDepth, int currSubtreeDepth)
        {
            return getMenuForCmsParentSelectionHelper(currentItemId, previousParentId, false, linkNonParents, false, maxDepth, currSubtreeDepth);
        }

        public string GetMenuForCmsMenuSelection(int currentItemId, int currentMenuParentId)
        {
            return getMenuForCmsMenuSelectionHelper(currentItemId, currentMenuParentId, false);
        }

        public int SubtreeDepth { get { return subtreeDepth; } }

        public void UpdateSubtreeDepth(int newDepth)
        {
            subtreeDepth = Math.Max(subtreeDepth, newDepth);
        }

        protected override void LoadData(string[] itemData)
        {
            text = itemData[0];
            rank = Convert.ToInt32(itemData[1]);
            pageId = Convert.ToInt32(itemData[2]);
        }

        protected override int IndentPixels { get { return 20; } }

        #region getMenuForCmsBrowsingHelper
        private string getMenuForCmsBrowsingHelper(int currentItemId, bool highlight, string link, bool linkNonParents)
        {
            string divId = PublicMenuTree.HTML_DIV_PREFIX + Id;
            PublicMenuTree parentTree = (PublicMenuTree)ParentTree;

            StringBuilder sb1 = new StringBuilder();

            if (Parent != null)
            {
                string hreflink = " href='" + link + "Id=" + Id + "'";
                if (!linkNonParents && Children.Count == 0)
                    hreflink = "";

                string imageId = PublicMenuTree.HTML_IMG_PREFIX + Id;

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
                        sb1.AppendFormat(" <span style=\"font-weight:bold;\"><a class=\"menutreelink_a\"{0}>{1}</a></span></div>", hreflink, Text);
                    else
                        sb1.AppendFormat(" <a class=\"menutreelink\"{0}><span style=\"font-weight:bold;margin-top:50px\">{1}</span></a></div>", hreflink, Text);
                }
                else
                {
                    if (currentItemId == Id)
                        sb1.AppendFormat(" <span style=\"font-weight:bold;\"><a class=\"menutreelink_a\"{0}>{1}</a></span></div>", hreflink, Text);
                    else
                        if (highlight)
                        sb1.AppendFormat(" <a class=\"menutreelink_a\"{0}>{1}</a></div>", hreflink, Text);
                    else
                        sb1.AppendFormat(" <a class=\"menutreelink\"{0}>{1}</a></div>", hreflink, Text);
                }
            }

            StringBuilder sb2 = new StringBuilder();
            if (Children.Count > 0)
            {
                if (currentItemId == Id)
                    foreach (PublicMenuTreeItem item in Children)
                        sb2.Append(item.getMenuForCmsBrowsingHelper(currentItemId, true, link, linkNonParents));
                else
                    foreach (PublicMenuTreeItem item in Children)
                        sb2.Append(item.getMenuForCmsBrowsingHelper(currentItemId, false, link, linkNonParents));

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
        #endregion

        #region getMenuForCmsParentSelectionHelper
        private string getMenuForCmsParentSelectionHelper(
            int currentItemId, int previousParentId, bool highlight, bool linkNonParents, bool disableRadio, int maxDepth, int currSubtreeDepth)
        {
            string divId = PublicMenuTree.HTML_DIV_PREFIX + Id;
            PublicMenuTree parentTree = (PublicMenuTree)ParentTree;

            string radio = "";
            if (Id == previousParentId)
                radio = "<input type='radio' style='margin-bottom:-3px;' checked='checked' name='" + RADIOBUTTON_NAME + "' value='" + Id + "'>";
            else if (disableRadio || (Id == currentItemId) || ((Depth + currSubtreeDepth) > maxDepth))
            {
                disableRadio = true;
                radio = "<input disabled='disabled' type='radio' style='margin-bottom:-3px;' name='" + RADIOBUTTON_NAME + "' value='" + Id + "'>";
            }
            else
                radio = "<input type='radio' style='margin-bottom:-3px;' name='" + RADIOBUTTON_NAME + "' value='" + Id + "'>";


            StringBuilder sb1 = new StringBuilder();

            if (Parent != null)
            {
                string imageId = PublicMenuTree.HTML_IMG_PREFIX + Id;

                string margin = "2";
                if (Parent.Id == -1) margin = "12";
                sb1.AppendFormat("\n<div style=\"width:100%;margin-left:{0}px;margin-top:{1}px;\">", GetIndent(), margin);

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
                        sb1.AppendFormat(" <span style=\"font-weight:bold;\">{1}<a class=\"menutreelink_a\">{0}</a></span></div>", Text, radio);
                    else
                        sb1.AppendFormat(" {1}<a class=\"menutreelink\"><span style=\"font-weight:bold;margin-top:50px\">{0}</span></a></div>", Text, radio);
                }
                else
                {
                    if (currentItemId == Id)
                        sb1.AppendFormat(" <span style=\"font-weight:bold;\">{1}<a class=\"menutreelink_a\">{0}</a></span></div>", Text, radio);
                    else
                        if (highlight)
                        sb1.AppendFormat(" {1}<a class=\"menutreelink_a\">{0}</a></div>", Text, radio);
                    else
                        sb1.AppendFormat(" {1}<a class=\"menutreelink\">{0}</a></div>", Text, radio);
                }
            }

            StringBuilder sb2 = new StringBuilder();
            if (Children.Count > 0)
            {
                if (currentItemId == Id)
                    foreach (PublicMenuTreeItem item in Children)
                        sb2.Append(item.getMenuForCmsParentSelectionHelper(currentItemId, previousParentId, true, linkNonParents, disableRadio, maxDepth, currSubtreeDepth));
                else
                    foreach (PublicMenuTreeItem item in Children)
                        sb2.Append(item.getMenuForCmsParentSelectionHelper(currentItemId, previousParentId, false, linkNonParents, disableRadio, maxDepth, currSubtreeDepth));

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
        #endregion

        #region getMenuForCmsParentSelectionHelperbackup
        private string getMenuForCmsParentSelectionHelperbackup(int currentItemId, int previousParentId, bool highlight, string link, bool linkNonParents)
        {
            string divId = PublicMenuTree.HTML_DIV_PREFIX + Id;
            PublicMenuTree parentTree = (PublicMenuTree)ParentTree;

            string radio = "";
            if (Id == previousParentId)
                radio = "<input type='radio' style='margin-bottom:-3px;' checked='checked' name='" + RADIOBUTTON_NAME + "' value='" + Id + "'>";
            else
                radio = "<input type='radio' style='margin-bottom:-3px;' name='" + RADIOBUTTON_NAME + "' value='" + Id + "'>";


            StringBuilder sb1 = new StringBuilder();

            if (Parent != null)
            {
                string hreflink = " href='" + link + "Id=" + Id + "'";
                if (!linkNonParents && Children.Count == 0)
                    hreflink = "";

                string imageId = PublicMenuTree.HTML_IMG_PREFIX + Id;

                string margin = "2";
                if (Parent.Id == -1) margin = "12";
                sb1.AppendFormat("\n<div style=\"width:100%;margin-left:{0}px;margin-top:{1}px;\">", GetIndent(), margin);

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
                        sb1.AppendFormat(" <span style=\"font-weight:bold;\">{2}<a class=\"menutreelink_a\"{0}>{1}</a></span></div>", hreflink, Text, radio);
                    else
                        sb1.AppendFormat(" {2}<a class=\"menutreelink\"{0}><span style=\"font-weight:bold;margin-top:50px\">{1}</span></a></div>", hreflink, Text, radio);
                }
                else
                {
                    if (currentItemId == Id)
                        sb1.AppendFormat(" <span style=\"font-weight:bold;\">{2}<a class=\"menutreelink_a\"{0}>{1}</a></span></div>", hreflink, Text, radio);
                    else
                        if (highlight)
                        sb1.AppendFormat(" {2}<a class=\"menutreelink_a\"{0}>{1}</a></div>", hreflink, Text, radio);
                    else
                        sb1.AppendFormat(" {2}<a class=\"menutreelink\"{0}>{1}</a></div>", hreflink, Text, radio);
                }
            }

            StringBuilder sb2 = new StringBuilder();
            if (Children.Count > 0)
            {
                if (currentItemId == Id)
                    foreach (PublicMenuTreeItem item in Children)
                        sb2.Append(item.getMenuForCmsParentSelectionHelperbackup(currentItemId, previousParentId, true, link, linkNonParents));
                else
                    foreach (PublicMenuTreeItem item in Children)
                        sb2.Append(item.getMenuForCmsParentSelectionHelperbackup(currentItemId, previousParentId, false, link, linkNonParents));

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
        #endregion

        #region getMenuForCmsMenuSelectionHelper
        private string getMenuForCmsMenuSelectionHelper(int currentMenuId, int currentMenuParentId, bool highlight)
        {
            string divId = PublicMenuTree.HTML_DIV_PREFIX + Id;
            PublicMenuTree parentTree = (PublicMenuTree)ParentTree;

            string radio = "";
            if (Id == currentMenuId)
                radio = "<input type='radio' style='margin-bottom:-3px;' checked='checked' name='" + RADIOBUTTON_NAME + "' value='" + Id + "'>";
            else
                radio = "<input type='radio' style='margin-bottom:-3px;' name='" + RADIOBUTTON_NAME + "' value='" + Id + "'>";


            StringBuilder sb1 = new StringBuilder();

            if (Parent != null)
            {
                string imageId = PublicMenuTree.HTML_IMG_PREFIX + Id;

                string margin = "2";
                if (Parent.Id == -1) margin = "12";
                sb1.AppendFormat("\n<div style=\"width:100%;margin-left:{0}px;margin-top:{1}px;\">", GetIndent(), margin);

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
                    if (currentMenuId == Id)
                        sb1.AppendFormat(" <span style=\"color:#414141;font-weight:bold;\">{1}{0}</span></div>", Text, radio);
                    else
                        sb1.AppendFormat(" {1}<span style=\"color:#414141;font-weight:bold;margin-top:50px\">{0}</span></div>", Text, radio);
                }
                else
                {
                    if (currentMenuId == Id)
                        sb1.AppendFormat(" <span style=\"color:#414141;font-weight:bold;\">{1}{0}</span></div>", Text, radio);
                    else
                        if (highlight)
                        sb1.AppendFormat(" {1}{0}</div>", Text, radio);
                    else
                        sb1.AppendFormat(" {1}{0}</div>", Text, radio);
                }
            }

            StringBuilder sb2 = new StringBuilder();
            if (Children.Count > 0)
            {
                bool highlightnext = false;
                if (currentMenuId == Id)
                    highlightnext = true;

                foreach (PublicMenuTreeItem item in Children)
                    sb2.Append(item.getMenuForCmsMenuSelectionHelper(currentMenuId, currentMenuParentId, highlightnext));

                if (Parent != null)
                {
                    if (!ParentTree.ItemIsParent(Id))
                        //if (!ParentTree.ItemIsParent(Id) || currentMenuParentId != Id)  //changed this on 6/3/08
                        sb2.Insert(0, "\n\t<div id=\"" + divId + "\" style=\"display:none;\">");
                    else
                        sb2.Insert(0, "\n\t<div id=\"" + divId + "\">");
                    sb2.Append("</div>");
                }
            }
            return sb1.ToString() + sb2.ToString();
        }
        #endregion

        private string text;
        private int rank;
        private int pageId;
        private int subtreeDepth;
    }
}
