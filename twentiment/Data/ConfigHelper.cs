using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterCrawler.Data
{
    public class ConfigHelper
    {
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

        private ConfigHelper() { }
    }
}
