using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;


namespace Foundation.Data
{
    public class File : DataClass
    {
        #region constructor
        public File(int recordId) : base()
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

        #region string Extension
        public string Extension
        {
            get { return extension; }
            set { extension = value; }
        }
        #endregion

        #region int Size
        public int Size
        {
            get { return filesize; }
            set { filesize = value; }
        }
        #endregion

        #region int ImageWidth
        public int ImageWidth
        {
            get { return imageWidth; }
            set { imageWidth = value; }
        }
        #endregion

        #region int ImageHeight
        public int ImageHeight
        {
            get { return imageHeight; }
            set { imageHeight = value; }
        }
        #endregion

        #region string Description
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        #endregion

        #region string Comment
        public string Comment
        {
            get { return admincomment; }
            set { admincomment = value; }
        }
        #endregion

        #region DateTime Uploaded
        public DateTime Uploaded
        {
            get { return created; }
        }
        #endregion

        #region DateTime Modified
        public DateTime Modified
        {
            get { return modified; }
        }
        #endregion

        #region int ModifiedBy
        public int ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }
        #endregion

        #region int Update()
        public int Update()
        {
            SqlCommand command = new SqlCommand("File_Update", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Id", Id));

            command.Parameters.Add(new SqlParameter("@Size", Size));
            command.Parameters.Add(new SqlParameter("@ImageWidth", ImageWidth));
            command.Parameters.Add(new SqlParameter("@ImageHeight", ImageHeight));

            if (Description != null)
                command.Parameters.Add(new SqlParameter("@Description", Description));
            else
                command.Parameters.Add(new SqlParameter("@Description", System.DBNull.Value));

            if (Comment != null)
                command.Parameters.Add(new SqlParameter("@Comment", Comment));
            else
                command.Parameters.Add(new SqlParameter("@Comment", System.DBNull.Value));

            command.Parameters.Add(new SqlParameter("@ModifiedBy", ModifiedBy));
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
            SqlCommand command = new SqlCommand("File_Get", Connection);
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
                extension = reader.GetString(1);

            if (reader[2] != System.DBNull.Value)
                filesize = reader.GetInt32(2);

            if (reader[3] != System.DBNull.Value)
                imageWidth = reader.GetInt32(3);

            if (reader[4] != System.DBNull.Value)
                imageHeight = reader.GetInt32(4);

            if (reader[5] != System.DBNull.Value)
                description = reader.GetString(5);

            if (reader[6] != System.DBNull.Value)
                admincomment = reader.GetString(6);

            if (reader[7] != System.DBNull.Value)
                created = reader.GetDateTime(7);

            if (reader[8] != System.DBNull.Value)
                modified = reader.GetDateTime(8);

            if (reader[9] != System.DBNull.Value)
                modifiedBy = reader.GetInt32(9);

            reader.Close();
            Connection.Close();
        }
        private int id;
        private string extension;
        private int filesize;
        private int imageWidth;
        private int imageHeight;
        private string description;
        private string admincomment;
        private DateTime created;
        private DateTime modified;
        private int modifiedBy;

        #endregion
    }
}