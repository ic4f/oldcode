using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Foundation.Data
{
    public class DepartmentPage : DataClass
    {
        #region constructor
        public DepartmentPage(int recordId) : base()
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

        #region int CollegePageId
        public int CollegePageId
        {
            get { return collegePageId; }
            set { collegePageId = value; }
        }
        #endregion

        #region int Update()
        public int Update()
        {
            SqlCommand command = new SqlCommand("DepartmentPage_Update", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Id", Id));

            command.Parameters.Add(new SqlParameter("@CollegePageId", CollegePageId));

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
            SqlCommand command = new SqlCommand("DepartmentPage_Get", Connection);
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
                collegePageId = reader.GetInt32(2);

            reader.Close();
            Connection.Close();
        }
        private int id;
        private int pageId;
        private int collegePageId;

        #endregion
    }
}