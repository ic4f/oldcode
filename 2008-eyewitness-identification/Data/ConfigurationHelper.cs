using System;

namespace Ei.Data
{
    public class ConfigurationHelper
    {
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

        private ConfigurationHelper() { }
    }
}
