using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace TwitterCrawler.Data
{
    public class TwuserHashtagLinkData : DataClass
    {
        public TwuserHashtagLinkData() : base() { }

        public void Create(string twuserId, int hashtagId)
        {
            SqlCommand command = new SqlCommand("TwuserHashtagLink_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@twuserId", twuserId));
            command.Parameters.Add(new SqlParameter("@hashtagId", hashtagId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }

        public DataTable GetHashtags(string twuserId)
        {
            SqlCommand command = new SqlCommand("TwuserHashtagLink_GetHashtags", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@twuserId", twuserId));
            return ExecDataSet(command).Tables[0];
        }
    }
}
