using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;


namespace Foundation.Data
{
    public class StagingMenu : DataClass
    {
        #region constructor
        public StagingMenu(int recordId) : base()
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
            set { parentId = value; }
        }
        #endregion

        #region string Text
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        #endregion

        #region int Rank
        public int Rank
        {
            get { return rank; }
            set { rank = value; }
        }
        #endregion

        #region int PageId
        public int PageId
        {
            get { return pageId; }
            set { pageId = value; }
        }
        #endregion

        #region int HeaderImageId
        public int HeaderImageId
        {
            get { return headerImageId; }
            set { headerImageId = value; }
        }
        #endregion

        #region private

        private void loadRecord(int recordId)
        {
            SqlCommand command = new SqlCommand("StagingMenu_Get", Connection);
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
                text = reader.GetString(2);

            if (reader[3] != System.DBNull.Value)
                rank = reader.GetInt32(3);

            if (reader[4] != System.DBNull.Value)
                pageId = reader.GetInt32(4);

            if (reader[5] != System.DBNull.Value)
                headerImageId = reader.GetInt32(5);

            reader.Close();
            Connection.Close();
        }
        private int id;
        private int parentId;
        private string text;
        private int rank;
        private int pageId;
        private int headerImageId;

        #endregion
    }
}