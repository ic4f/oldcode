using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;


namespace Foundation.Data
{
    public class Module : DataClass
    {
        #region constructor
        public Module(int recordId) : base()
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

        #region string AdminTitle
        public string AdminTitle
        {
            get { return adminTitle; }
            set { adminTitle = value; }
        }
        #endregion

        #region string ImageExtension
        public string ImageExtension
        {
            get { return imageExtension; }
            set { imageExtension = value; }
        }
        #endregion

        #region string Title
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        #endregion

        #region string Body
        public string Body
        {
            get { return body; }
            set { body = value; }
        }
        #endregion

        #region string ExternalLink
        public string ExternalLink
        {
            get { return externalLink; }
            set { externalLink = value; }
        }
        #endregion

        #region int PageId
        public int PageId
        {
            get { return pageId; }
            set { pageId = value; }
        }
        #endregion

        #region int Rank
        public int Rank
        {
            get { return rank; }
            set { rank = value; }
        }
        #endregion

        #region bool IsRequired
        public bool IsRequired
        {
            get { return isRequired; }
            set { isRequired = value; }
        }
        #endregion

        #region bool IsArchived
        public bool IsArchived
        {
            get { return isArchived; }
            set { isArchived = value; }
        }
        #endregion

        #region DateTime Created
        public DateTime Created
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
            SqlCommand command = new SqlCommand("Module_Update", Connection);
            command.CommandType = CommandType.StoredProcedure;

            if (PageId == -1)
            {
                command.Parameters.Add(new SqlParameter("@PageId", System.DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ExternalLink", ExternalLink));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@PageId", PageId));
                command.Parameters.Add(new SqlParameter("@ExternalLink", System.DBNull.Value));
            }

            command.Parameters.Add(new SqlParameter("@Id", Id));

            if (AdminTitle != null)
                command.Parameters.Add(new SqlParameter("@AdminTitle", AdminTitle));
            else
                command.Parameters.Add(new SqlParameter("@AdminTitle", System.DBNull.Value));

            if (ImageExtension != null)
                command.Parameters.Add(new SqlParameter("@ImageExtension", ImageExtension));
            else
                command.Parameters.Add(new SqlParameter("@ImageExtension", System.DBNull.Value));

            if (Title != null)
                command.Parameters.Add(new SqlParameter("@Title", Title));
            else
                command.Parameters.Add(new SqlParameter("@Title", System.DBNull.Value));

            if (Body != null)
                command.Parameters.Add(new SqlParameter("@Body", Body));
            else
                command.Parameters.Add(new SqlParameter("@Body", System.DBNull.Value));

            command.Parameters.Add(new SqlParameter("@IsRequired", IsRequired));
            command.Parameters.Add(new SqlParameter("@IsArchived", IsArchived));
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
            SqlCommand command = new SqlCommand("Module_Get", Connection);
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
                adminTitle = reader.GetString(1);

            if (reader[2] != System.DBNull.Value)
                imageExtension = reader.GetString(2);

            if (reader[3] != System.DBNull.Value)
                title = reader.GetString(3);

            if (reader[4] != System.DBNull.Value)
                body = reader.GetString(4);

            if (reader[5] != System.DBNull.Value)
                externalLink = reader.GetString(5);

            if (reader[6] != System.DBNull.Value)
                pageId = reader.GetInt32(6);

            if (reader[7] != System.DBNull.Value)
                rank = reader.GetInt32(7);

            if (reader[8] != System.DBNull.Value)
                isRequired = reader.GetBoolean(8);

            if (reader[9] != System.DBNull.Value)
                isArchived = reader.GetBoolean(9);

            if (reader[10] != System.DBNull.Value)
                created = reader.GetDateTime(10);

            if (reader[11] != System.DBNull.Value)
                modified = reader.GetDateTime(11);

            if (reader[12] != System.DBNull.Value)
                modifiedBy = reader.GetInt32(12);

            reader.Close();
            Connection.Close();
        }
        private int id;
        private string adminTitle;
        private string imageExtension;
        private string title;
        private string body;
        private string externalLink;
        private int pageId;
        private int rank;
        private bool isRequired;
        private bool isArchived;
        private DateTime created;
        private DateTime modified;
        private int modifiedBy;

        #endregion
    }
}