using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class StagingMenuData : DataClass
    {
        #region constructor 
        public StagingMenuData() : base() { }
        #endregion

        #region Create
        public int Create(int parentid, string text, int rank)
        {
            SqlCommand command = new SqlCommand("StagingMenu_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@parentid", parentid));
            command.Parameters.Add(new SqlParameter("@text", text));
            command.Parameters.Add(new SqlParameter("@rank", rank));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region CreateLocked
        public int CreateLocked(int parentid, string text, int rank)
        {
            SqlCommand command = new SqlCommand("StagingMenu_CreateLocked", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@parentid", parentid));
            command.Parameters.Add(new SqlParameter("@text", text));
            command.Parameters.Add(new SqlParameter("@rank", rank));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region Delete
        public int Delete(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecNonQuery("StagingMenu_Delete", parameters);
        }
        #endregion

        #region UpdateText
        public void UpdateText(int id, string text)
        {
            SqlCommand command = new SqlCommand("StagingMenu_UpdateText", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@text", text));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region UpdateRank
        public void UpdateRank(int id, int rank)
        {
            SqlCommand command = new SqlCommand("StagingMenu_UpdateRank", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@rank", rank));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region UpdateParent
        public void UpdateParent(int id, int parentid)
        {
            SqlCommand command = new SqlCommand("StagingMenu_UpdateParent", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@parentid", parentid));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region UpdatePage
        public void UpdatePage(int id, int pageid)
        {
            SqlCommand command = new SqlCommand("StagingMenu_UpdatePage", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@pageid", pageid));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region UpdateHeaderImage
        public void UpdateHeaderImage(int id, int headerimageid)
        {
            SqlCommand command = new SqlCommand("StagingMenu_UpdateHeaderImage", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@headerimageid", headerimageid));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region WriteStagingToMenu
        public void WriteStagingToMenu()
        {
            SqlCommand command = new SqlCommand("StagingMenu_WriteStagingToMenu", Connection);
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region GetMenuByParent
        public DataSet GetMenuByParent(int parentid)
        {
            SqlCommand command = new SqlCommand("StagingMenu_GetMenuByParent", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@parentid", parentid));
            return ExecDataSet(command);
        }
        #endregion

        #region GetOrdered
        public DataSet GetOrdered()
        {
            SqlCommand command = new SqlCommand("StagingMenu_GetOrdered", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command);
        }
        #endregion

        #region IsPublishable
        public bool IsPublishable()
        {
            SqlCommand command = new SqlCommand("StagingMenu_IsPublishable", Connection);
            command.CommandType = CommandType.StoredProcedure;
            Connection.Open();
            bool result = Convert.ToBoolean(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

    }
}
