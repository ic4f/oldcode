using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class TagData : DataClass
    {
        #region constructor 
        public TagData() : base() { }
        #endregion

        #region Create
        public int Create(string text, int modifiedby)
        {
            SqlCommand command = new SqlCommand("Tag_Create", Connection);
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
        public DataSet GetRecords(string sortExp)
        {
            SqlCommand command = new SqlCommand("Tag_GetRecords", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@sortExp", sortExp));
            return ExecDataSet(command);
        }
        #endregion

        #region GetList
        public DataTable GetList()
        {
            SqlCommand command = new SqlCommand("Tag_GetList", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetAllTagsByPage
        public DataTable GetAllTagsByPage(int pageId)
        {
            SqlCommand command = new SqlCommand("Tag_GetAllTagsByPage", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetTagsByPage
        public DataTable GetTagsByPage(int pageId)
        {
            SqlCommand command = new SqlCommand("Tag_GetTagsByPage", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region Delete
        public int Delete(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecNonQuery("Tag_Delete", parameters);
        }
        #endregion
    }
}
