using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Foundation.Data
{
    public class PermissionCategory : DataClass
    {
        #region constructor
        public PermissionCategory(int recordId) : base()
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

        #region int ParentId
        public int ParentId
        {
            get { return parentId; }
        }
        #endregion

        #region string Name
        public string Name
        {
            get { return name; }
        }
        #endregion

        #region int Rank
        public int Rank
        {
            get { return rank; }
        }
        #endregion

        #region private

        private void loadRecord(int recordId)
        {
            SqlCommand command = new SqlCommand("PermissionCategory_Get", Connection);
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
                parentId = reader.GetInt32(1);

            if (reader[2] != System.DBNull.Value)
                name = reader.GetString(2);

            if (reader[3] != System.DBNull.Value)
                rank = reader.GetInt32(3);

            reader.Close();
            Connection.Close();
        }
        private int id;
        private int parentId;
        private string name;
        private int rank;

        #endregion
    }
}