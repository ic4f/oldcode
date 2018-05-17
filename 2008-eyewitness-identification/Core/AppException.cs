using System;
using System.Text;
using System.IO;
using System.Web;

namespace Core
{
    public class AppException : System.Exception
    {
        public AppException() { LogError(this); }

        public AppException(string message) { LogError(this, message); }

        public AppException(string message, Exception myInnerException)
        {
            LogError(this, message);
            if (InnerException != null)
                LogError(myInnerException);
        }

        public static void LogError(Exception ex) { LogError(ex, ""); }

        public static void LogError(Exception ex, string message)
        {
            int gmtOffset = DateTime.Compare(DateTime.Now, DateTime.UtcNow);
            string gmtPrefix = "";
            if (gmtOffset > 0)
                gmtPrefix = "+";

            StringBuilder sb = new StringBuilder();

            if (message != "")
                sb.Append(message + "\n");

            sb.AppendFormat("{0}.{1}.{2} @ {3}:{4}:{5} (GMT {6}.{7})",
                DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                DateTime.Now.Minute, DateTime.Now.Second, gmtPrefix, gmtOffset);

            string errorFile = ConfigurationHelper.ErrorLogFile;
            string detailsFile = ConfigurationHelper.ErrorDetailsFile;

            try
            {
                StreamWriter writer1 = new StreamWriter(errorFile, true);
                writer1.WriteLine("## {0} ## {1} ##", sb.ToString(), ex.Message);
                writer1.WriteLine();
                writer1.Close();

                StreamWriter writer2 = new StreamWriter(detailsFile, false);
                writer2.WriteLine("## {0} ## {1} ##", sb.ToString(), ex.Message);
                writer2.WriteLine();
                writer2.WriteLine(ex.StackTrace);
                writer2.WriteLine();
                writer2.Close();
            }
            catch (Exception) { }
        }
    }
}