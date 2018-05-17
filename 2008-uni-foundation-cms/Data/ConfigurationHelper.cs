using System;

namespace Foundation.Data
{
    public class ConfigurationHelper
    {
        public static string ConnectionString { get { return System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"]; } }

        private ConfigurationHelper() { }
    }
}
