using System;
using System.Collections;

namespace Foundation.BusinessAdmin
{
    public class CmsPage
    {
        public CmsPage(int id, int parentId, string menuTitle, string pageTitle, string url, bool inMenu, string perms)
        {
            this.id = id;
            this.parentId = parentId;
            this.menuTitle = menuTitle;
            this.pageTitle = pageTitle;
            this.url = url;
            this.inMenu = inMenu;
            this.perms = perms;
            this.level = calcLevel();
        }
        public int Id { get { return id; } }

        public int ParentId { get { return parentId; } }

        public string MenuTitle { get { return menuTitle; } }

        public string PageTitle { get { return pageTitle; } }

        public string Url { get { return url; } }

        public bool InMenu { get { return inMenu; } }

        public string Perms { get { return perms; } }

        public int Level { get { return level; } }

        public int[] GetPermissionCodes()
        {
            string[] permIds = perms.Split(new char[] { ',' });
            int[] codes = new int[permIds.Length];
            for (int i = 0; i < permIds.Length; i++)
                codes[i] = Convert.ToInt32(permIds[i]);
            return codes;
        }

        public ArrayList GetPermissionCodesStringArray()
        {
            string[] permIds = perms.Split(new char[] { ',' });
            ArrayList codes = new ArrayList();
            for (int i = 0; i < permIds.Length; i++)
                codes.Add(permIds[i]);
            return codes;
        }

        public bool IsOpenToAllUsers { get { return perms.Length == 0; } }

        private int calcLevel()
        {
            int level = 0;
            foreach (char c in url)
                if (c == '/')
                    level++;
            return level;
        }

        private int id;
        private int parentId;
        private string menuTitle;
        private string pageTitle;
        private string url;
        private bool inMenu;
        private string perms;
        private int level;
    }
}
