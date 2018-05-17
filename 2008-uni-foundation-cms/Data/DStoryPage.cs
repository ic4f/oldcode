using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Foundation.Data
{
    public class DStoryPage : DataClass
    {
        #region constructor
        public DStoryPage(int recordId) : base()
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

        #region bool IsFeatured
        public bool IsFeatured
        {
            get { return isFeatured; }
            set { isFeatured = value; }
        }
        #endregion

        #region string DonorDisplayedName
        public string DonorDisplayedName
        {
            get { return donorDisplayedName; }
            set { donorDisplayedName = value; }
        }
        #endregion

        #region string Summary
        public string Summary
        {
            get { return summary; }
            set { summary = value; }
        }
        #endregion

        #region int Update()
        public int Update()
        {
            SqlCommand command = new SqlCommand("DStoryPage_Update", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Id", Id));
            command.Parameters.Add(new SqlParameter("@HasImage", HasImage));
            command.Parameters.Add(new SqlParameter("@IsFeatured", IsFeatured));

            if (DonorDisplayedName != null)
                command.Parameters.Add(new SqlParameter("@DonorDisplayedName", DonorDisplayedName));
            else
                command.Parameters.Add(new SqlParameter("@DonorDisplayedName", System.DBNull.Value));

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
            SqlCommand command = new SqlCommand("DStoryPage_Get", Connection);
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
                isFeatured = reader.GetBoolean(3);

            if (reader[4] != System.DBNull.Value)
                donorDisplayedName = reader.GetString(4);

            if (reader[5] != System.DBNull.Value)
                summary = reader.GetString(5);

            reader.Close();
            Connection.Close();
        }
        private int id;
        private int pageId;
        private bool hasImage;
        private bool isFeatured;
        private string donorDisplayedName;
        private string summary;

        #endregion
    }
}