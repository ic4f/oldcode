using System;

namespace Foundation.ConsoleUtilities
{
    public class ConfigurationHelper
    {
        public static string ConnectionString { get { return System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"]; } }
        public static int ThumbnailHeight { get { return Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["ThumbnailHeight"]); } }
        public static int ThumbnailWidth { get { return Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["ThumbnailWidth"]); } }
        public static int ModuleWidth { get { return Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["ModuleWidth"]); } }
        public static int MaxModuleHeight { get { return Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["MaxModuleHeight"]); } }
        public static string PublicStandardPage { get { return System.Configuration.ConfigurationSettings.AppSettings["PublicStandardPage"]; } }
        public static string PageIdPrefix { get { return System.Configuration.ConfigurationSettings.AppSettings["PageIdPrefix"]; } }
        public static string FileIdPrefix { get { return System.Configuration.ConfigurationSettings.AppSettings["FileIdPrefix"]; } }
        public static string ImageIdPrefix { get { return System.Configuration.ConfigurationSettings.AppSettings["ImageIdPrefix"]; } }
        public static string PublicWebsiteRoot { get { return System.Configuration.ConfigurationSettings.AppSettings["PublicWebsiteRoot"]; } }

        private ConfigurationHelper() { }
    }
}