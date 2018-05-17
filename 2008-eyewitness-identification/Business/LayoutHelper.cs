using System;
using System.Text;

namespace Ei.Business
{
    public class LayoutHelper
    {
        public LayoutHelper() { }

        public string GetLayout1(string title) { return GetLayout1("", "", "", "", title); }

        public string GetLayout1(string linkPrefix, string contextMenu, string utilsMenu, string mainMenu, string title)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div id=\"pagetitle\">{0}</div>", title);
            sb.Append("<div id=\"contextmenu\">");
            sb.Append(contextMenu);
            sb.Append("</div>");
            sb.Append("<div id=\"cmslogo\">");
            sb.Append("<img src=\"");
            sb.Append(linkPrefix);
            sb.Append(ConfigurationHelper.CmsLogoImage);
            sb.Append("\">");
            sb.Append("</div>");
            sb.Append("<div id=\"cmstitle\">");
            sb.Append(ConfigurationHelper.CmsTitle);
            sb.Append("</div>");
            sb.Append("<table class=\"main_container\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">");
            sb.Append("<tr valign=\"top\">");
            sb.Append("<td class=\"menu_container\">");
            if (mainMenu.Length > 0)
                sb.AppendFormat("<div id=\"mainmenu\">{0}{1}</div>", utilsMenu, mainMenu);
            sb.Append("</td>");
            sb.Append("<td class=\"content_container\">	");
            return sb.ToString();
        }

        public string GetLayout2()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            return sb.ToString();
        }
    }
}
