using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;


namespace Foundation.Data
{
    public class FileType : DataClass
    {
        #region constructor
        public FileType(string ext) : base()
        {
            loadRecord(ext);
        }
        #endregion

        #region string Extension
        public string Extension
        {
            get { return extension; }
            set { extension = value; }
        }
        #endregion

        #region string Description
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        #endregion

        #region int Update()
        public int Update()
        {
            SqlCommand command = new SqlCommand("FileType_Update", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Extension", Extension));
            if (Description != null)
                command.Parameters.Add(new SqlParameter("@Description", Description));
            else
                command.Parameters.Add(new SqlParameter("@Description", System.DBNull.Value));
            Connection.Open();
            int result = command.ExecuteNonQuery();
            Connection.Close();

            if (result >= 0)
                loadRecord(Extension); //some values might have been changed by the db code
            return result;
        }
        #endregion

        #region private

        private void loadRecord(string ext)
        {
            SqlCommand command = new SqlCommand("FileType_Get", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Extension", ext));

            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
                throw new Core.AppException("Record with Extension = " + ext + " not found.");
            reader.Read();

            if (reader[0] != System.DBNull.Value)
                extension = reader.GetString(0);

            if (reader[1] != System.DBNull.Value)
                description = reader.GetString(1);

            reader.Close();
            Connection.Close();
        }
        private string extension;
        private string description;

        #endregion
    }
}