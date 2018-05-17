using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class ProgramPageData : DataClass
    {
        #region constructor 
        public ProgramPageData() : base() { }
        #endregion

        #region Create
        public int Create(int pageId, bool isFeatured)
        {
            SqlCommand command = new SqlCommand("ProgramPage_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            command.Parameters.Add(new SqlParameter("@isFeatured", isFeatured));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetProgramPages
        public DataTable GetProgramPages(string sortexp)
        {
            SqlCommand command = new SqlCommand("ProgramPage_GetProgramPages", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@sortexp", sortexp));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetProgramPagesWithQuery
        public DataTable GetProgramPagesWithQuery(string query)
        {
            SqlCommand command = new SqlCommand("ProgramPage_GetProgramPagesWithQuery", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@query", query));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetIdByPageId
        public int GetIdByPageId(int pageId)
        {
            SqlCommand command = new SqlCommand("ProgramPage_GetIdByPageId", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetPublishedProgramPages
        public DataTable GetPublishedProgramPages()
        {
            SqlCommand command = new SqlCommand("ProgramPage_GetPublishedProgramPages", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetPublishedProgramPagesByCollege
        public DataTable GetPublishedProgramPagesByCollege(int collegeId)
        {
            SqlCommand command = new SqlCommand("ProgramPage_GetPublishedProgramPagesByCollege", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@collegeId", collegeId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetPublishedProgramPagesByDepartment
        public DataTable GetPublishedProgramPagesByDepartment(int departmentId)
        {
            SqlCommand command = new SqlCommand("ProgramPage_GetPublishedProgramPagesByDepartment", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@departmentId", departmentId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region Delete
        public int Delete(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecNonQuery("ProgramPage_Delete", parameters);
        }
        #endregion

        #region GetProgramsForHomepage
        public DataTable GetProgramsForHomepage()
        {
            SqlCommand command = new SqlCommand("ProgramPage_GetProgramsForHomepage", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion
    }
}
