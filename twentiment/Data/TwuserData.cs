using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace TwitterCrawler.Data
{
    public class TwuserData : DataClass
    {
        public TwuserData() : base() { }

        public void Create(string id, string name, string location, string bio, int following, int followers, int listed, int tweets)
        {
            SqlCommand command = new SqlCommand("Twuser_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@name", name));
            command.Parameters.Add(new SqlParameter("@location", location));
            command.Parameters.Add(new SqlParameter("@bio", bio));
            command.Parameters.Add(new SqlParameter("@following", following));
            command.Parameters.Add(new SqlParameter("@followers", followers));
            command.Parameters.Add(new SqlParameter("@listed", listed));
            command.Parameters.Add(new SqlParameter("@tweets", tweets));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }

        public bool Exists(string id)
        {
            SqlCommand command = new SqlCommand("Twuser_Exists", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            Connection.Open();
            command.ExecuteNonQuery();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return (result == 1);
        }

        public bool FolloweesAdded(string id)
        {
            SqlCommand command = new SqlCommand("Twuser_FolloweesAdded", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            Connection.Open();
            command.ExecuteNonQuery();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return (result == 1);
        }

        public void UpdateCounts(string id, int followers, int listed, int tweets)
        {
            SqlCommand command = new SqlCommand("Twuser_UpdateCounts", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@followers", followers));
            command.Parameters.Add(new SqlParameter("@listed", listed));
            command.Parameters.Add(new SqlParameter("@tweets", tweets));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }

        public DataTable GetSeedIds()
        {
            SqlCommand command = new SqlCommand("Twuser_GetSeedIds", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }

        public DataTable GetFollowers(int minFollowees)
        {
            SqlCommand command = new SqlCommand("Twuser_GetFollowers", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@minFollowees", minFollowees));
            return ExecDataSet(command).Tables[0];
        }

        public DataTable GetFollowees(string id)
        {
            SqlCommand command = new SqlCommand("Twuser_GetFollowees", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            return ExecDataSet(command).Tables[0];
        }

        public DataTable GetProcessedFollowees(string followerId)
        {
            SqlCommand command = new SqlCommand("Twuser_GetProcessedFollowees", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@followerId", followerId));
            return ExecDataSet(command).Tables[0];
        }

        public void MarkFolloweesAdded(string id)
        {
            SqlCommand command = new SqlCommand("Twuser_MarkFolloweesAdded", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
    }
}
