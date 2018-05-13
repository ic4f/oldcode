using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace TwitterCrawler.Data
{
    public class TweetHashtagLinkData : DataClass
    {
        public TweetHashtagLinkData() : base() { }

        public void Create(int tweetId, int hashtagId)
        {
            SqlCommand command = new SqlCommand("TweetHashtagLink_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@tweetId", tweetId));
            command.Parameters.Add(new SqlParameter("@hashtagId", hashtagId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
    }
}
