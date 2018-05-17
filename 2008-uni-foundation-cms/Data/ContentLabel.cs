using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;


namespace Foundation.Data
{
    public class ContentLabel : DataClass
    {
        #region constructor
        public ContentLabel(int recordId) : base()
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
            SqlCommand command = new SqlCommand("ContentLabel_Update", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Id", Id));
            if (Text != null)
                command.Parameters.Add(new SqlParameter("@Text", Text));
            else
                command.Parameters.Add(new SqlParameter("@Text", System.DBNull.Value));

            command.Parameters.Add(new SqlParameter("@Rank", Rank));
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
            SqlCommand command = new SqlCommand("ContentLabel_Get", Connection);
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
                text = reader.GetString(1);

            if (reader[2] != System.DBNull.Value)
                rank = reader.GetInt32(2);

            if (reader[3] != System.DBNull.Value)
                created = reader.GetDateTime(3);

            if (reader[4] != System.DBNull.Value)
                modified = reader.GetDateTime(4);

            if (reader[5] != System.DBNull.Value)
                modifiedBy = reader.GetInt32(5);

            reader.Close();
            Connection.Close();
        }
        private int id;
        private string text;
        private int rank;
        private DateTime created;
        private DateTime modified;
        private int modifiedBy;

        #endregion
    }
}