using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace TwitterCrawler.Data
{
    public class HahstagOverlapData : DataClass
    {
        public HahstagOverlapData() : base() { }

        public void Create(string followerId, string followeeId, int hashtagId, int followerUsageCount, int followeeUsageCount)
        {
            SqlCommand command = new SqlCommand("HashtagOverlap_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@followerId", followerId));
            command.Parameters.Add(new SqlParameter("@followeeId", followeeId));
            command.Parameters.Add(new SqlParameter("@hashtagId", hashtagId));
            command.Parameters.Add(new SqlParameter("@followerUsageCount", followerUsageCount));
            command.Parameters.Add(new SqlParameter("@followeeUsageCount", followeeUsageCount));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
    }
}
