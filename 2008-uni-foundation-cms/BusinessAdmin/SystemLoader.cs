using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using dt = Foundation.Data;

namespace Foundation.BusinessAdmin
{
    public class SystemLoader
    {
        public SystemLoader(int userId)
        {
            this.userId = userId;
        }

        public void Load()
        {
            loadMenu();
            loadPageCategories();
            loadCustomPages();
            updateMenu();
        }

        private void loadMenu()
        {
            dt.StagingMenuData smd = new dt.StagingMenuData();
            homeMenuId = smd.CreateLocked(CmsPageData.NO_PARENT, "Home", -1);
            newsMenuId = smd.Create(CmsPageData.NO_PARENT, "News", 1);
            dstoryMenuId = smd.Create(CmsPageData.NO_PARENT, "Donor Stories", 2);
            givingMenuId = smd.Create(CmsPageData.NO_PARENT, "Giving Opportunities", 3);
            collegeMenuId = smd.Create(givingMenuId, "Colleges", 0);
            dptMenuId = smd.Create(givingMenuId, "Departments", 1);
            progMenuId = smd.Create(givingMenuId, "Programs", 2);
        }

        private void loadPageCategories()
        {
            dt.PageCategoryData pcd = new dt.PageCategoryData();
            standardCatId = pcd.Create("standard page");
            newsCatId = pcd.Create("news", newsMenuId);
            dstoryCatId = pcd.Create("donor story", dstoryMenuId);
            collegeCatId = pcd.Create("college description", collegeMenuId);
            dptCatId = pcd.Create("department description", dptMenuId);
            progCatId = pcd.Create("program description", progMenuId);
        }

        private void loadCustomPages()
        {
            dt.PageData pd = new dt.PageData();
            homePageId = pd.Create(homeMenuId, standardCatId, false, true, false, false, "", "UNI Foundation Homepage", "", "", true, false, false, false, false, true, ConfigurationHelper.PublicWebsiteRoot + "default.aspx?id=" + UrlHelper.PAGE_ID_PREFIX, "This is the website's homepage.", userId);
            newsListPageId = pd.Create(newsMenuId, standardCatId, false, true, false, false, "", "News Archive", "News Archive", "", true, false, false, false, false, true, ConfigurationHelper.PublicWebsiteRoot + "newsList.aspx?id=" + UrlHelper.PAGE_ID_PREFIX, "Displays a list of published news.", userId);
            dstoryListPageId = pd.Create(dstoryMenuId, standardCatId, false, true, false, false, "", "Donor Stories", "Donor Stories", "", true, false, false, false, false, true, ConfigurationHelper.PublicWebsiteRoot + "dstoryList.aspx?id=" + UrlHelper.PAGE_ID_PREFIX, "Displays a list of published donor stories.", userId);
            collegeListPageId = pd.Create(collegeMenuId, standardCatId, false, true, false, false, "", "Colleges", "Colleges", "", true, false, false, false, false, true, ConfigurationHelper.PublicWebsiteRoot + "collegeList.aspx?id=" + UrlHelper.PAGE_ID_PREFIX, "Displays a list of published college pages.", userId);
            dptListPageId = pd.Create(dptMenuId, standardCatId, false, true, false, false, "", "Departments", "Departments", "", true, false, false, false, false, true, ConfigurationHelper.PublicWebsiteRoot + "departmentList.aspx?id=" + UrlHelper.PAGE_ID_PREFIX, "Displays a list of published department pages.", userId);
            progListPageId = pd.Create(progMenuId, standardCatId, false, true, false, false, "", "Programs", "Programs", "", true, false, false, false, false, true, ConfigurationHelper.PublicWebsiteRoot + "programList.aspx?id=" + UrlHelper.PAGE_ID_PREFIX, "Displays a list of published program pages.", userId);
            givingPageId = pd.Create(givingMenuId, standardCatId, true, true, true, true, "", "Giving Opportunities", "Giving Opportunities", "", true, true, true, true, false, true, ConfigurationHelper.PublicWebsiteRoot + UrlHelper.PUBLIC_STANDARD_PAGE + "?id=" + UrlHelper.PAGE_ID_PREFIX, "", userId);
        }

        private void updateMenu()
        {
            dt.StagingMenuData smd = new dt.StagingMenuData();
            smd.UpdatePage(homeMenuId, homePageId);
            smd.UpdatePage(newsMenuId, newsListPageId);
            smd.UpdatePage(dstoryMenuId, dstoryListPageId);
            smd.UpdatePage(collegeMenuId, collegeListPageId);
            smd.UpdatePage(dptMenuId, dptListPageId);
            smd.UpdatePage(progMenuId, progListPageId);
            smd.UpdatePage(givingMenuId, givingPageId);
        }

        private void runSql(string sql)
        {
            SqlConnection connection = new SqlConnection(ConfigurationHelper.ConnectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            command.CommandType = CommandType.Text;
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        private int userId;

        private int homeMenuId;
        private int newsMenuId;
        private int dstoryMenuId;
        private int givingMenuId;
        private int collegeMenuId;
        private int dptMenuId;
        private int progMenuId;

        private int standardCatId;
        private int newsCatId;
        private int dstoryCatId;
        private int collegeCatId;
        private int dptCatId;
        private int progCatId;

        private int homePageId;
        private int newsListPageId;
        private int dstoryListPageId;
        private int givingPageId;
        private int collegeListPageId;
        private int dptListPageId;
        private int progListPageId;
    }
}
