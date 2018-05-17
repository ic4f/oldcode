using System;

namespace Foundation.BusinessAdmin
{
    public class ConfigurationHelper
    {
        public static string AccessDeniedPage { get { return System.Configuration.ConfigurationSettings.AppSettings["AccessDeniedPage"]; } }
        public static string AdminWebsiteRoot { get { return System.Configuration.ConfigurationSettings.AppSettings["AdminWebsiteRoot"]; } }
        public static string AutoEmail { get { return System.Configuration.ConfigurationSettings.AppSettings["AutoEmail"]; } }
        public static string CmsLogoImage { get { return System.Configuration.ConfigurationSettings.AppSettings["CmsLogoImage"]; } }
        public static string CmsPageTitle { get { return System.Configuration.ConfigurationSettings.AppSettings["CmsPageTitle"]; } }
        public static string CmsTitle { get { return System.Configuration.ConfigurationSettings.AppSettings["CmsTitle"]; } }
        public static string ConnectionString { get { return System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"]; } }
        public static string DStoryImagesDirectoryUrl { get { return System.Configuration.ConfigurationSettings.AppSettings["DStoryImagesDirectoryUrl"]; } }
        public static string DStoryImageHeight { get { return System.Configuration.ConfigurationSettings.AppSettings["DStoryImageHeight"]; } }
        public static string DStoryImageWidth { get { return System.Configuration.ConfigurationSettings.AppSettings["DStoryImageWidth"]; } }
        public static string ErrorPage { get { return System.Configuration.ConfigurationSettings.AppSettings["ErrorPage"]; } }
        public static string FilesDirectoryUrl { get { return System.Configuration.ConfigurationSettings.AppSettings["FilesDirectoryUrl"]; } }
        public static bool HandleError { get { return Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["HandleError"]); } }
        public static int HeaderImageLeftWidth { get { return Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["HeaderImageLeftWidth"]); } }
        public static int HeaderImageLeftPhotoHeight { get { return Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["HeaderImageLeftPhotoHeight"]); } }
        public static int HeaderImageLeftLineHight { get { return Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["HeaderImageLeftLineHight"]); } }
        public static string HeaderImagesDirectoryUrlLeft { get { return System.Configuration.ConfigurationSettings.AppSettings["HeaderImagesDirectoryUrlLeft"]; } }
        public static string HeaderImagesDirectoryUrlRight { get { return System.Configuration.ConfigurationSettings.AppSettings["HeaderImagesDirectoryUrlRight"]; } }
        public static string InfoCookieName { get { return System.Configuration.ConfigurationSettings.AppSettings["InfoCookieName"]; } }
        public static string LoginPage { get { return System.Configuration.ConfigurationSettings.AppSettings["LoginPage"]; } }
        public static int MaxModuleHeight { get { return Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["MaxModuleHeight"]); } }
        public static string ModuleImagesDirectoryUrl { get { return System.Configuration.ConfigurationSettings.AppSettings["ModuleImagesDirectoryUrl"]; } }
        public static int ModuleWidth { get { return Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["ModuleWidth"]); } }
        public static string NewsImagesDirectoryUrl { get { return System.Configuration.ConfigurationSettings.AppSettings["NewsImagesDirectoryUrl"]; } }
        public static string NewsImageHeight { get { return System.Configuration.ConfigurationSettings.AppSettings["NewsImageHeight"]; } }
        public static string NewsImageWidth { get { return System.Configuration.ConfigurationSettings.AppSettings["NewsImageWidth"]; } }
        public static string PageFile { get { return System.Configuration.ConfigurationSettings.AppSettings["PageFile"]; } }
        public static string PermissionCategoryFile { get { return System.Configuration.ConfigurationSettings.AppSettings["PermissionCategoryFile"]; } }
        public static string PermissionFile { get { return System.Configuration.ConfigurationSettings.AppSettings["PermissionFile"]; } }
        public static int PublicMenuLevels { get { return Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["PublicMenuLevels"]); } }
        public static string PublicWebsiteRoot { get { return System.Configuration.ConfigurationSettings.AppSettings["PublicWebsiteRoot"]; } }
        public static int ThumbnailHeight { get { return Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["ThumbnailHeight"]); } }
        public static int ThumbnailWidth { get { return Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["ThumbnailWidth"]); } }

        private ConfigurationHelper() { }
    }
}

