using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Foundation.BusinessAdmin
{
    public class CmsPageData
    {
        public static int ADMIN_HOMEPAGE_ID = 1;
        public static string ADMIN_HOMEPAGE_URL = "default.aspx";
        public static int NO_PARENT = -1;

        public CmsPageData(ArrayList userPermissions)
        {
            pages = new Hashtable();
            menuPages = new ArrayList();
            load(userPermissions);
        }

        public CmsPage GetPage(int pageId) { return (CmsPage)pages[pageId]; }

        public string GetMainMenu(CmsPage page, string linkPrefix)
        {
            if (page.InMenu)
                return new AdminMenuTree(getMenu(), page.Id, linkPrefix).GetMenu();
            else
                return new AdminMenuTree(getMenu(), page.ParentId, linkPrefix).GetMenu();
        }

        public string GetContextMenuHtml(CmsPage page, string linkPrefix, bool isFinal)
        {
            if (page.Url == ADMIN_HOMEPAGE_URL && page.ParentId == NO_PARENT)
                return "<a class=\"contextmenulink\">" + page.PageTitle + "</a>";

            int tempId = page.Id;
            StringBuilder sb = new StringBuilder();
            CmsPage p = null;
            while (tempId != NO_PARENT)
            {
                p = (CmsPage)pages[tempId];
                if (isFinal)
                {
                    sb.Insert(0, "<a class=\"contextmenulink\">" + p.PageTitle + "</a>");
                    isFinal = false;
                }
                else
                    sb.Insert(0, "<a href=\"" + linkPrefix + p.Url + "\" class=\"contextmenulink\">" + p.PageTitle + "</a>&nbsp;&nbsp;>&nbsp;&nbsp;");

                tempId = p.ParentId;
            }
            if (p.Url != ADMIN_HOMEPAGE_URL) //guards against inserting 2 home links for pages like "help" - whic hhave homw as their parent.
                sb.Insert(0, "<a href=\"" + linkPrefix + "default.aspx\" class=\"contextmenulink\">Home</a>&nbsp;&nbsp;>&nbsp;&nbsp;");

            return sb.ToString();
        }

        private string[,] getMenu()
        {
            int count = menuPages.Count;
            string[,] menu = new string[count, 4];
            CmsPage p;
            for (int i = 0; i < count; i++)
            {
                p = (CmsPage)menuPages[i];
                menu[i, 0] = p.Id.ToString();
                menu[i, 1] = p.ParentId.ToString();
                menu[i, 2] = p.MenuTitle;
                menu[i, 3] = p.Url;
            }
            return menu;
        }

        private void load(ArrayList userPermissions)
        {
            FileStream fs = new FileStream(ConfigurationHelper.PageFile, FileMode.Open);
            BinaryReader r = new BinaryReader(fs);
            int id;
            int parentId;
            bool inMenu;
            string menuTitle;
            string pageTitle;
            string url;
            string perms;
            CmsPage page;
            while (fs.Position < fs.Length)
            {
                id = r.ReadInt32();
                parentId = r.ReadInt32();
                inMenu = r.ReadBoolean();
                menuTitle = r.ReadString();
                pageTitle = r.ReadString();
                url = r.ReadString();
                perms = r.ReadString();
                page = new CmsPage(id, parentId, menuTitle, pageTitle, url, inMenu, perms);

                bool pageIsPermitted = false;
                if (page.IsOpenToAllUsers)
                    pageIsPermitted = true;
                else
                {
                    ArrayList pagePermissions = page.GetPermissionCodesStringArray();
                    foreach (string perm in pagePermissions)
                        if (userPermissions.Contains(perm))
                            pageIsPermitted = true;
                }
                if (pageIsPermitted)
                {
                    pages.Add(id, page);
                    if (inMenu)
                        menuPages.Add(page);
                }
            }
            r.Close();
            fs.Close();
        }

        private Hashtable pages;
        private ArrayList menuPages;
    }
}
