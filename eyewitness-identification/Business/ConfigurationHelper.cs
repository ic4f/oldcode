using System;

namespace Ei.Business
{
    public class ConfigurationHelper
    {
        #region AccessDeniedPage
        public static string AccessDeniedPage
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["AccessDeniedPage"];
                if (s == null)
                    throw new Exception("AccessDeniedPage value not found in config file");
                return s;
            }
        }
        #endregion

        #region CmsLogoImage
        public static string CmsLogoImage
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["CmsLogoImage"];
                if (s == null)
                    throw new Exception("CmsLogoImage value not found in config file");
                return s;
            }
        }
        #endregion

        #region CmsPageTitle
        public static string CmsPageTitle
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["CmsPageTitle"];
                if (s == null)
                    throw new Exception("CmsPageTitle value not found in config file");
                return s;
            }
        }
        #endregion

        #region CmsTitle
        public static string CmsTitle
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["CmsTitle"];
                if (s == null)
                    throw new Exception("CmsTitle value not found in config file");
                return s;
            }
        }
        #endregion

        #region string ConnectionString
        public static string ConnectionString
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
                if (s == null)
                    throw new Exception("ConnectionString value not found in config file");
                return s;
            }
        }
        #endregion

        #region InfoCookieName
        public static string InfoCookieName
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["InfoCookieName"];
                if (s == null)
                    throw new Exception("InfoCookieName value not found in config file");
                return s;
            }
        }
        #endregion

        #region LoginPage
        public static string LoginPage
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["LoginPage"];
                if (s == null)
                    throw new Exception("LoginPage value not found in config file");
                return s;
            }
        }
        #endregion

        #region PhotosDirectoryUrl
        public static string PhotosDirectoryUrl
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["PhotosDirectoryUrl"];
                if (s == null)
                    throw new Exception("PhotosDirectoryUrl value not found in config file");
                return s;
            }
        }
        #endregion

        #region SuspectsDirectoryUrl
        public static string SuspectsDirectoryUrl
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["SuspectsDirectoryUrl"];
                if (s == null)
                    throw new Exception("SuspectsDirectoryUrl value not found in config file");
                return s;
            }
        }
        #endregion

        #region WebRoot
        public static string WebRoot
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["WebRoot"];
                if (s == null)
                    throw new Exception("WebRoot value not found in config file");
                return s;
            }
        }
        #endregion

        private ConfigurationHelper() { }
    }
}

