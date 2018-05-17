using System;

namespace Foundation.Maintenance
{
    public class ConfigurationHelper
    {
        public static string PageTargetFile { get { return System.Configuration.ConfigurationSettings.AppSettings["PageTargetFile"]; } }
        public static string PageSourceFile { get { return System.Configuration.ConfigurationSettings.AppSettings["PageSourceFile"]; } }
        public static string PermissionTargetFile { get { return System.Configuration.ConfigurationSettings.AppSettings["PermissionTargetFile"]; } }
        public static string PermissionSourceFile { get { return System.Configuration.ConfigurationSettings.AppSettings["PermissionSourceFile"]; } }
        public static string PermissionCategoryTargetFile { get { return System.Configuration.ConfigurationSettings.AppSettings["PermissionCategoryTargetFile"]; } }

        private ConfigurationHelper() { }
    }
}
