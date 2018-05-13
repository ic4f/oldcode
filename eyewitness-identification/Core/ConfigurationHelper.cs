using System;

namespace Core
{
    public class ConfigurationHelper
    {
        #region ErrorDetailsFile
        public static string ErrorDetailsFile
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["ErrorDetailsFile"];
                if (s == null)
                    throw new Exception("ErrorDetailsFile value not found in config file");
                return s;
            }
        }
        #endregion

        #region ErrorLogFile
        public static string ErrorLogFile
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["ErrorLogFile"];
                if (s == null)
                    throw new Exception("ErrorLogFile value not found in config file");
                return s;
            }
        }
        #endregion

        #region HandleError
        public static bool HandleError
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["HandleError"];
                if (s == null)
                    throw new Exception("HandleError value not found in config file");
                return Convert.ToBoolean(s);
            }
        }
        #endregion

        #region ErrorPage
        public static string ErrorPage
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["ErrorPage"];
                if (s == null)
                    throw new Exception("ErrorPage value not found in config file");
                return s;
            }
        }
        #endregion

        private ConfigurationHelper() { }
    }
}
