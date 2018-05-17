using System;

namespace Foundation.BusinessMain
{
    public class ConfigurationHelper
    {
        public static string AbsoluteUrlRoot { get { return System.Configuration.ConfigurationSettings.AppSettings["AbsoluteUrlRoot"]; } }
        public static string AdminEmail { get { return System.Configuration.ConfigurationSettings.AppSettings["AdminEmail"]; } }
        public static string ConnectionString { get { return System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"]; } }
        public static bool HandleError { get { return Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["HandleError"]); } }
        public static string PublicWebsiteRoot { get { return System.Configuration.ConfigurationSettings.AppSettings["PublicWebsiteRoot"]; } }
        internal static string DStoryImagesDirectoryUrl { get { return System.Configuration.ConfigurationSettings.AppSettings["DStoryImagesDirectoryUrl"]; } }
        internal static string EventImagesDirectoryUrl { get { return System.Configuration.ConfigurationSettings.AppSettings["EventImagesDirectoryUrl"]; } }
        internal static string HeaderImageDefaultUrlLeft { get { return System.Configuration.ConfigurationSettings.AppSettings["HeaderImageDefaultUrlLeft"]; } }
        internal static string HeaderImageDefaultUrlRight { get { return System.Configuration.ConfigurationSettings.AppSettings["HeaderImageDefaultUrlRight"]; } }
        internal static string HeaderImagesDirectoryUrlLeft { get { return System.Configuration.ConfigurationSettings.AppSettings["HeaderImagesDirectoryUrlLeft"]; } }
        internal static string HeaderImagesDirectoryUrlRight { get { return System.Configuration.ConfigurationSettings.AppSettings["HeaderImagesDirectoryUrlRight"]; } }
        internal static string ModuleImagesDirectoryUrl { get { return System.Configuration.ConfigurationSettings.AppSettings["ModuleImagesDirectoryUrl"]; } }
        internal static string NewsImagesDirectoryUrl { get { return System.Configuration.ConfigurationSettings.AppSettings["NewsImagesDirectoryUrl"]; } }
        public static string Page_NotFound { get { return System.Configuration.ConfigurationSettings.AppSettings["Page_NotFound"]; } }
        public static string Page_Error { get { return System.Configuration.ConfigurationSettings.AppSettings["Page_Error"]; } }

        private ConfigurationHelper() { }
    }
}
