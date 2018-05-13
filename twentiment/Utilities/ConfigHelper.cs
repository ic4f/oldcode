using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterCrawler.Utilities
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

        public static string Crawler_UserAgent
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["Crawler_UserAgent"];
                if (s == null)
                    throw new Exception("Crawler_UserAgent value not found in config file");
                return s;
            }
        }

        public static string Crawler_Header_From
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["Crawler_Header_From"];
                if (s == null)
                    throw new Exception("Crawler_Header_From value not found in config file");
                return s;
            }
        }

        public static int Queue_MaxCount
        {
            get
            {
                string s = System.Configuration.ConfigurationSettings.AppSettings["Queue_MaxCount"];
                if (s == null)
                    throw new Exception("Queue_MaxCount value not found in config file");
                return Convert.ToInt32(s);
            }
        }

        private ConfigHelper() { }
    }
}