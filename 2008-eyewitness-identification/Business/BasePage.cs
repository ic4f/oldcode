using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using c = Core;
using cc = Core.Cms;
using d = Ei.Data;

namespace Ei.Business
{
    public abstract class BasePage : System.Web.UI.Page
    {
        protected Literal ltrLayout1;
        protected Literal ltrLayout2;
        protected HtmlGenericControl ctlTitleTag;

        protected cc.SiteIdentity Identity { get { return identity; } }

        protected override void OnInit(EventArgs e)
        {
            principal = (cc.SitePrincipal)Context.User;
            identity = (cc.SiteIdentity)principal.Identity;
            pageData = new cc.CmsPageData(identity.Permissions);
            page = pageData.GetPage(PageId);
            checkPermissions();
            linkPrefix = makePrefix(page.Level);
            sbContextMenu = new StringBuilder();
            this.Load += new System.EventHandler(Page_Load);
            this.Error += new System.EventHandler(this.Page_Error);
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            Response.Cache.SetExpires(DateTime.Parse(DateTime.Now.ToString()));
            Response.Cache.SetCacheability(HttpCacheability.Private);

            if (!Page.IsPostBack)
            {
                AddContextMenu();
                string contextMenu = pageData.GetContextMenuHtml(page, linkPrefix, sbContextMenu.Length == 0) + sbContextMenu.ToString();
                string pageTitle = GetPageTitle(page.PageTitle);
                ctlTitleTag.InnerText = ConfigurationHelper.CmsPageTitle + pageTitle;
                LayoutHelper lh = new LayoutHelper();
                ltrLayout1.Text = lh.GetLayout1(linkPrefix, contextMenu, getUtilsMenu(), pageData.GetMainMenu(page, linkPrefix), pageTitle);
                ltrLayout2.Text = lh.GetLayout2();
            }
            base.OnLoad(e);
        }

        protected virtual string GetPageTitle(string defaultTitle)
        {
            return defaultTitle;
        }

        protected string LinkPrefix { get { return linkPrefix; } }

        protected bool HasPermission(int permissionCode) { return principal.HasPermission(permissionCode); }

        protected void Page_Error(object sender, System.EventArgs e)
        {
            c.ErrorHandler.HandleError(HttpContext.Current.Server.GetLastError());
        }

        protected virtual void AddContextMenu() { }

        protected void AddContextMenuItem(string link, string text)
        {
            sbContextMenu.AppendFormat("<a href=\"{0}{1}\" class=\"contextmenulink\">{2}</a>&nbsp;&nbsp;>&nbsp;&nbsp;",
                linkPrefix, link, text);
        }

        protected void AddContextMenuItem(string text)
        {
            sbContextMenu.AppendFormat("<a class=\"contextmenulink\">{0}</a>", text);
        }

        private string makePrefix(int level)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < level; i++)
                sb.Append("../");
            return sb.ToString();
        }

        private void checkPermissions()
        {
            if (page.IsOpenToAllUsers)
                return;
            int[] pagePerms = page.GetPermissionCodes();
            foreach (int id in pagePerms)
                if (principal.HasPermission(id)) //at least 1 permission for this page is enough
                    return;
            Response.Redirect(ConfigurationHelper.AccessDeniedPage);
        }

        private string getUtilsMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class=\"utilsmenu\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td align=\"center\">");
            sb.AppendFormat("<a class=\"utilslink\" href=\"{0}signout.aspx\">Sign-out</a>&nbsp;&nbsp;", linkPrefix);
            sb.Append("</td></tr></table>");
            return sb.ToString();
        }

        protected abstract void Page_Load(object sender, System.EventArgs e);

        protected abstract int PageId { get; }

        private cc.SitePrincipal principal;
        private cc.SiteIdentity identity;
        private string linkPrefix;
        private StringBuilder sbContextMenu;
        private cc.CmsPage page;
        private cc.CmsPageData pageData;
    }
}
