using System;

namespace Core.Cms
{
    public class ConfigurationHelper
    {
        #region string PageFile
        public static string PageFile
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["PageFile"];
                if (s == null)
                    throw new Exception("PageFile value not found in config file");
                return s;
            }
        }
        #endregion

        #region string PermissionFile
        public static string PermissionFile
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["PermissionFile"];
                if (s == null)
                    throw new Exception("PermissionFile value not found in config file");
                return s;
            }
        }
        #endregion

        #region string PermissionCategoryFile
        public static string PermissionCategoryFile
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["PermissionCategoryFile"];
                if (s == null)
                    throw new Exception("PermissionCategoryFile value not found in config file");
                return s;
            }
        }
        #endregion

        private ConfigurationHelper() { }
    }
}
