using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;


namespace Foundation.Data
{
    public class PageCategory : DataClass
    {
        #region constructor
        public PageCategory(int recordId) : base()
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

        #region int MenuId
        public int MenuId
        {
            get { return menuId; }
            set { menuId = value; }
        }
        #endregion

        #region private

        private void loadRecord(int recordId)
        {
            SqlCommand command = new SqlCommand("PageCategory_Get", Connection);
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
                menuId = reader.GetInt32(2);

            reader.Close();
            Connection.Close();
        }
        private int id;
        private string text;
        private int menuId;

        #endregion
    }
}