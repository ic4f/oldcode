using System;
using System.Text;
using ch = Foundation.BusinessAdmin.ConfigurationHelper;

namespace Foundation.BusinessAdmin
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

        private UrlHelper() { }

        public static string AdminHomepage
        {
            get
            {
                return ConfigurationHelper.AdminWebsiteRoot + CmsPageData.ADMIN_HOMEPAGE_URL;
            }
        }

        public static string BuildPagePartialUrlByPageCategory(int pageCategoryId)
        {
            StringBuilder sb = new StringBuilder();
            string pageName = "";
            switch (pageCategoryId)
            {
                case PageCategoryCode.StandardPage:
                    pageName = PUBLIC_STANDARD_PAGE;
                    break;
                case PageCategoryCode.College:
                    pageName = PUBLIC_COLLEGE_PAGE;
                    break;
                case PageCategoryCode.Department:
                    pageName = PUBLIC_DEPARTMENT_PAGE;
                    break;
                case PageCategoryCode.DonorStory:
                    pageName = PUBLIC_DSTORY_PAGE;
                    break;
                case PageCategoryCode.News:
                    pageName = PUBLIC_NEWS_PAGE;
                    break;
                case PageCategoryCode.Program:
                    pageName = PUBLIC_PROGRAM_PAGE;
                    break;
            }
            sb.AppendFormat("{0}{1}?id={2}", ch.PublicWebsiteRoot, pageName, PAGE_ID_PREFIX);
            return sb.ToString();
        }

        public static string BuildCustomPagePartialUrl(string filename)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}?id={2}", ch.PublicWebsiteRoot, filename, PAGE_ID_PREFIX);
            return sb.ToString();
        }

        public static string GetPageFileNameByUrl(string url)
        {
            url = url.Replace(ch.PublicWebsiteRoot, "");
            int length = url.Length - url.Length + url.IndexOf("?id=");
            return url.Substring(0, length);
        }

        public static string GetFileUrl(int fileId, string extension)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}{2}{3}{4}", ch.PublicWebsiteRoot, ch.FilesDirectoryUrl, FILE_ID_PREFIX, fileId, extension);
            return sb.ToString();
        }

        public static string GetFileName(int fileId, string extension)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}{2}", FILE_ID_PREFIX, fileId, extension);
            return sb.ToString();
        }

        public static string GetImageUrl(int imageId, string extension)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}{2}{3}{4}", ch.PublicWebsiteRoot, ch.FilesDirectoryUrl, IMAGE_ID_PREFIX, imageId, extension);
            return sb.ToString();
        }

        public static string GetImageThumbnailUrl(int imageId, string extension)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}{2}{3}s{4}", ch.PublicWebsiteRoot, ch.FilesDirectoryUrl, IMAGE_ID_PREFIX, imageId, extension);
            return sb.ToString();
        }

        public static string GetImageName(int imageId, string extension)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}{2}", IMAGE_ID_PREFIX, imageId, extension);
            return sb.ToString();
        }

        public static string GetDStoryImageUrl(int dstoryId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}{2}.jpg", ch.PublicWebsiteRoot, ch.DStoryImagesDirectoryUrl, dstoryId);
            return sb.ToString();
        }

        public static string GetHeaderImageUrlLeft(int headerId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}{2}.jpg", ch.PublicWebsiteRoot, ch.HeaderImagesDirectoryUrlLeft, headerId);
            return sb.ToString();
        }

        public static string GetHeaderImageUrlRight(int headerId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}{2}.jpg", ch.PublicWebsiteRoot, ch.HeaderImagesDirectoryUrlRight, headerId);
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
    }
}
