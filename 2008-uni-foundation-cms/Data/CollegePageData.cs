using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class CollegePageData : DataClass
    {
        #region constructor 
        public CollegePageData() : base() { }
        #endregion

        #region Create
        public int Create(int pageId)
        {
            SqlCommand command = new SqlCommand("CollegePage_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetList
        public DataTable GetList()
        {
            SqlCommand command = new SqlCommand("CollegePage_GetList", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetPublishedList
        public DataTable GetPublishedList()
        {
            SqlCommand command = new SqlCommand("CollegePage_GetPublishedList", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetCollegePages
        public DataTable GetCollegePages(string sortexp)
        {
            SqlCommand command = new SqlCommand("CollegePage_GetCollegePages", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@sortexp", sortexp));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetPublishedCollegePages
        public DataTable GetPublishedCollegePages()
        {
            SqlCommand command = new SqlCommand("CollegePage_GetPublishedCollegePages", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetCollegePagesWithQuery
        public DataTable GetCollegePagesWithQuery(string query)
        {
            SqlCommand command = new SqlCommand("CollegePage_GetCollegePagesWithQuery", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@query", query));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetAllCollegesByProgram
        public DataTable GetAllCollegesByProgram(int programpageid)
        {
            SqlCommand command = new SqlCommand("CollegePage_GetAllCollegesByProgram", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@programpageid", programpageid));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetIdByPageId
        public int GetIdByPageId(int pageId)
        {
            SqlCommand command = new SqlCommand("CollegePage_GetIdByPageId", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetDependentDepartments
        public DataSet GetDependentDepartments(int collegepageId)
        {
            SqlCommand command = new SqlCommand("CollegePage_GetDependentDepartments", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@collegepageId", collegepageId));
            return ExecDataSet(command);
        }
        #endregion

        #region Delete
        public int Delete(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecNonQuery("CollegePage_Delete", parameters);
        }
        #endregion
    }
}
