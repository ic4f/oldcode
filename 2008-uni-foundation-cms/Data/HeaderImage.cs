using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;


namespace Foundation.Data
{
    public class HeaderImage : DataClass
    {
        #region constructor
        public HeaderImage(int recordId) : base()
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

        #region int LocationCode
        public int LocationCode
        {
            get { return locationCode; }
        }
        #endregion

        #region string Description
        public string Description
        {
            get { return description; }
        }
        #endregion

        #region DateTime Created
        public DateTime Created
        {
            get { return created; }
        }
        #endregion

        #region int CreatedBy
        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        #endregion

        #region private

        private void loadRecord(int recordId)
        {
            SqlCommand command = new SqlCommand("HeaderImage_Get", Connection);
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
                locationCode = reader.GetInt32(1);

            if (reader[2] != System.DBNull.Value)
                description = reader.GetString(2);

            if (reader[3] != System.DBNull.Value)
                created = reader.GetDateTime(3);

            if (reader[4] != System.DBNull.Value)
                createdBy = reader.GetInt32(4);

            reader.Close();
            Connection.Close();
        }
        private int id;
        private int locationCode;
        private string description;
        private DateTime created;
        private int createdBy;

        #endregion
    }
}