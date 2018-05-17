using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Foundation.Data
{
    public class NewsPage : DataClass
    {
        #region constructor
        public NewsPage(int recordId) : base()
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

        #region int PageId
        public int PageId
        {
            get { return pageId; }
        }
        #endregion

        #region bool HasImage
        public bool HasImage
        {
            get { return hasImage; }
            set { hasImage = value; }
        }
        #endregion

        #region bool IsOnHomepage
        public bool IsOnHomepage
        {
            get { return isOnHomepage; }
            set { isOnHomepage = value; }
        }
        #endregion

        #region bool IsHighlighted
        public bool IsHighlighted
        {
            get { return isHighlighted; }
            set { isHighlighted = value; }
        }
        #endregion

        #region string Summary
        public string Summary
        {
            get { return summary; }
            set { summary = value; }
        }
        #endregion

        #region DateTime DisplayedPublishedDate
        public DateTime DisplayedPublishedDate
        {
            get { return displayedPublishedDate; }
            set { displayedPublishedDate = value; }
        }
        #endregion

        #region int Update()
        public int Update()
        {
            SqlCommand command = new SqlCommand("NewsPage_Update", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Id", Id));
            command.Parameters.Add(new SqlParameter("@HasImage", HasImage));
            command.Parameters.Add(new SqlParameter("@IsOnHomepage", IsOnHomepage));
            command.Parameters.Add(new SqlParameter("@IsHighlighted", IsHighlighted));

            if (Summary != null)
                command.Parameters.Add(new SqlParameter("@Summary", Summary));
            else
                command.Parameters.Add(new SqlParameter("@Summary", System.DBNull.Value));

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
            SqlCommand command = new SqlCommand("NewsPage_Get", Connection);
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
                pageId = reader.GetInt32(1);

            if (reader[2] != System.DBNull.Value)
                hasImage = reader.GetBoolean(2);

            if (reader[3] != System.DBNull.Value)
                isOnHomepage = reader.GetBoolean(3);

            if (reader[4] != System.DBNull.Value)
                isHighlighted = reader.GetBoolean(4);

            if (reader[5] != System.DBNull.Value)
                summary = reader.GetString(5);

            if (reader[6] != System.DBNull.Value)
                displayedPublishedDate = reader.GetDateTime(6);

            reader.Close();
            Connection.Close();
        }
        private int id;
        private int pageId;
        private bool hasImage;
        private bool isOnHomepage;
        private bool isHighlighted;
        private string summary;
        private DateTime displayedPublishedDate;

        #endregion
    }
}