using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace TwitterCrawler.Data
{
    public class TwuserLinkData : DataClass
    {
        public TwuserLinkData() : base() { }

        public void Create(string followerId, string followeeId)
        {
            SqlCommand command = new SqlCommand("TwuserLink_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@followerId", followerId));
            command.Parameters.Add(new SqlParameter("@followeeId", followeeId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
    }
}
