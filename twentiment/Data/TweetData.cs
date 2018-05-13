using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace TwitterCrawler.Data
{
    public class TweetData : DataClass
    {
        public TweetData() : base() { }

        public int Create(string twuserId, string text, string time)
        {
            SqlCommand command = new SqlCommand("Tweet_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@twuserId", twuserId));
            command.Parameters.Add(new SqlParameter("@text", text));
            command.Parameters.Add(new SqlParameter("@time", time));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }

        public DataTable GetRecords()
        {
            SqlCommand command = new SqlCommand("Tweet_GetRecords", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }

        public DataTable GetHashTagTweeets()
        {
            SqlCommand command = new SqlCommand("Tweet_GetHashTagTweeets", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }

        public void UpdateData(int id, string nohtmltext)
        {
            SqlCommand command = new SqlCommand("Tweet_UpdateData", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@nohtmltext", nohtmltext));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
    }
}