using System;

namespace Core
{
    public class ConfigurationHelper
    {
        public static string ErrorDetailsFile { get { return System.Configuration.ConfigurationSettings.AppSettings["ErrorDetailsFile"]; } }
        public static string ErrorLogFile { get { return System.Configuration.ConfigurationSettings.AppSettings["ErrorLogFile"]; } }

        private ConfigurationHelper() { }
    }
}
