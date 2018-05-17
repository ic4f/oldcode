using System;
using System.Text;
using ch = Foundation.BusinessMain.ConfigurationHelper;

namespace Foundation.BusinessMain
{
    public class UrlHelper
    {
        public const string FILE_ID_PREFIX = "lotw36f";
        public const string IMAGE_ID_PREFIX = "lotw36i";
        public const string PAGE_ID_PREFIX = "lotw36p";
        public const string PUBLIC_COLLEGE_PAGE = "college.aspx";
        public const string PUBLIC_DEPARTMENT_PAGE = "department.aspx";
        public const string PUBLIC_DSTORY_PAGE = "dstory.aspx";
        public const string PUBLIC_NEWS_PAGE = "news.aspx";
        public const string PUBLIC_PROGRAM_PAGE = "program.aspx";
        public const string PUBLIC_STANDARD_PAGE = "page.aspx";
        public const int HOMEPAGE_ID = 1;

        private UrlHelper() { }

        public static int GetPageId(string idstring)
        {
            try { return Convert.ToInt32(idstring.Substring(PAGE_ID_PREFIX.Length)); }
            catch (Exception) { return -1; }
        }

        public static string GetHeaderImageUrlLeft(int headerId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}{2}.jpg", ch.PublicWebsiteRoot, ch.HeaderImagesDirectoryUrlLeft, headerId);
            return sb.ToString();
        }

        public static string GetDefaultHeaderImageUrlLeft()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}", ch.PublicWebsiteRoot, ch.HeaderImageDefaultUrlLeft);
            return sb.ToString();
        }

        public static string GetHeaderImageUrlRight(int headerId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}{2}.jpg", ch.PublicWebsiteRoot, ch.HeaderImagesDirectoryUrlRight, headerId);
            return sb.ToString();
        }

        public static string GetDefaultHeaderImageUrlRight()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}", ch.PublicWebsiteRoot, ch.HeaderImageDefaultUrlRight);
            return sb.ToString();
        }

        public static string GetModuleImageUrl(int moduleId, string imageextension)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}{2}{3}", ch.PublicWebsiteRoot, ch.ModuleImagesDirectoryUrl, moduleId, imageextension);
            return sb.ToString();
        }

        public static string GetNewsImageUrl(int newsId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}{2}.jpg", ch.PublicWebsiteRoot, ch.NewsImagesDirectoryUrl, newsId);
            return sb.ToString();
        }

        public static string GetDStoryImageUrl(int dstoryId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}{2}.jpg", ch.PublicWebsiteRoot, ch.DStoryImagesDirectoryUrl, dstoryId);
            return sb.ToString();
        }

        public static string GetPageNotFoundUrl()
        {
            return ch.PublicWebsiteRoot + ch.Page_NotFound;
        }

        public static string GetPageErrorUrl()
        {
            return ch.PublicWebsiteRoot + ch.Page_Error;
        }

        public static string BuildCustomPagePartialUrl(string filename)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}?id={2}", ch.PublicWebsiteRoot, filename, PAGE_ID_PREFIX);
            return sb.ToString();
        }
    }
}
