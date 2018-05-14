using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Param = IrProject.Core.Parameter;

namespace IrProject.Data
{
    public class Doc : DataClass
    {
        #region constructor
        public Doc(int recordId) : base()
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

        #region string Url
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        #endregion

        #region string Title
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        #endregion

        #region private

        private void loadRecord(int recordId)
        {
            SqlCommand command = new SqlCommand("Doc_Get", Connection);
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
                url = reader.GetString(1);

            if (reader[2] != System.DBNull.Value)
                title = reader.GetString(2);

            reader.Close();
            Connection.Close();
        }
        private int id;
        private string url;
        private string title;

        #endregion
    }
}