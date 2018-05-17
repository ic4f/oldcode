using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class DepartmentPageData : DataClass
    {
        #region constructor 
        public DepartmentPageData() : base() { }
        #endregion

        #region Create
        public int Create(int pageId, int collegepageid)
        {
            SqlCommand command = new SqlCommand("DepartmentPage_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            command.Parameters.Add(new SqlParameter("@collegepageid", collegepageid));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetList
        public DataTable GetList()
        {
            SqlCommand command = new SqlCommand("DepartmentPage_GetList", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetDepartmentPages
        public DataTable GetDepartmentPages(string sortexp)
        {
            SqlCommand command = new SqlCommand("DepartmentPage_GetDepartmentPages", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@sortexp", sortexp));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetDepartmentPagesWithQuery
        public DataTable GetDepartmentPagesWithQuery(string query)
        {
            SqlCommand command = new SqlCommand("DepartmentPage_GetDepartmentPagesWithQuery", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@query", query));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetIdByPageId
        public int GetIdByPageId(int pageId)
        {
            SqlCommand command = new SqlCommand("DepartmentPage_GetIdByPageId", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetAllDepartmentsByProgram
        public DataTable GetAllDepartmentsByProgram(int programpageid)
        {
            SqlCommand command = new SqlCommand("DepartmentPage_GetAllDepartmentsByProgram", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@programpageid", programpageid));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetPublishedDepartmentPages
        public DataTable GetPublishedDepartmentPages()
        {
            SqlCommand command = new SqlCommand("DepartmentPage_GetPublishedDepartmentPages", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetPublishedDepartmentPagesByCollege
        public DataTable GetPublishedDepartmentPagesByCollege(int collegeId)
        {
            SqlCommand command = new SqlCommand("DepartmentPage_GetPublishedDepartmentPagesByCollege", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@collegeId", collegeId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region Delete
        public int Delete(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecNonQuery("DepartmentPage_Delete", parameters);
        }
        #endregion
    }
}
