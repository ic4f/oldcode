using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class ContentLabelData : DataClass
    {
        #region constructor 
        public ContentLabelData() : base() { }
        #endregion

        #region Create
        public int Create(string text, int modifiedby)
        {
            SqlCommand command = new SqlCommand("ContentLabel_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@text", text));
            command.Parameters.Add(new SqlParameter("@modifiedby", modifiedby));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetRecords
        public DataSet GetRecords()
        {
            SqlCommand command = new SqlCommand("ContentLabel_GetRecords", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command);
        }
        #endregion

        #region GetList
        public DataTable GetList()
        {
            SqlCommand command = new SqlCommand("ContentLabel_GetList", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetAllLabelsByFile
        public DataTable GetAllLabelsByFile(int fileId)
        {
            SqlCommand command = new SqlCommand("ContentLabel_GetAllLabelsByFile", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@fileId", fileId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetAllLabelsByPage
        public DataTable GetAllLabelsByPage(int pageId)
        {
            SqlCommand command = new SqlCommand("ContentLabel_GetAllLabelsByPage", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetAllLabelsByModule
        public DataTable GetAllLabelsByModule(int moduleId)
        {
            SqlCommand command = new SqlCommand("ContentLabel_GetAllLabelsByModule", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@moduleId", moduleId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region Delete
        public int Delete(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecNonQuery("ContentLabel_Delete", parameters);
        }
        #endregion

        #region UpdateRank
        public void UpdateRank(int id, int rank, int modifiedby)
        {
            SqlCommand command = new SqlCommand("ContentLabel_UpdateRank", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@rank", rank));
            command.Parameters.Add(new SqlParameter("@modifiedby", modifiedby));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion
    }
}
