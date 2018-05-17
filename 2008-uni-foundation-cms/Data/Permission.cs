using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;


namespace Foundation.Data
{
    public class Permission : DataClass
    {
        #region constructor
        public Permission(int recordId) : base()
        {
            loadRecord(recordId);
        }
        #endregion

        #region int Id
        public int Id
        {
            get { return id; }
        }
        #endregion

        #region string Description
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        #endregion

        #region int Rank
        public int Rank
        {
            get { return rank; }
            set { rank = value; }
        }
        #endregion

        #region int Update()
        public int Update()
        {
            SqlCommand command = new SqlCommand("Permission_Update", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Id", Id));
            if (Description != null)
                command.Parameters.Add(new SqlParameter("@Description", Description));
            else
                command.Parameters.Add(new SqlParameter("@Description", System.DBNull.Value));

            command.Parameters.Add(new SqlParameter("@Rank", Rank));
            Connection.Open();
            int result = command.ExecuteNonQuery();
            Connection.Close();

            if (result >= 0)
                loadRecord(id); //some values might have been changed by the db code
            return result;
        }
        #endregion

        #region private

        private void loadRecord(int recordId)
        {
            SqlCommand command = new SqlCommand("Permission_Get", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Id", recordId));

            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
                throw new Core.AppException("Record with id = " + recordId + " not found.");
            reader.Read();

            if (reader[0] != System.DBNull.Value)
                id = reader.GetInt32(0);

            if (reader[1] != System.DBNull.Value)
                description = reader.GetString(1);

            if (reader[2] != System.DBNull.Value)
                rank = reader.GetInt32(2);

            reader.Close();
            Connection.Close();
        }
        private int id;
        private string description;
        private int rank;

        #endregion
    }
}