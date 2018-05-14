using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = IrProject.Core;

namespace IrProject.Data
{
    public class DocData : DataClass
    {
        #region constructor 
        public DocData() : base() { }
        #endregion

        #region Create
        public int Create(string url)
        {
            SqlCommand command = new SqlCommand("Doc_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@url", url));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region PageIdExists
        public bool PageIdExists(int id)
        {
            SqlCommand command = new SqlCommand("Doc_PageIdExists", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            Connection.Open();
            bool result = Convert.ToBoolean(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region PageUrlExists
        public bool PageUrlExists(string url)
        {
            SqlCommand command = new SqlCommand("Doc_PageUrlExists", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@url", url));
            Connection.Open();
            bool result = Convert.ToBoolean(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetIdByUrl
        public int GetIdByUrl(string url)
        {
            SqlCommand command = new SqlCommand("Doc_GetIdByUrl", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@url", url));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region UpdateTitle
        public void UpdateTitle(int id, string title)
        {
            SqlCommand command = new SqlCommand("Doc_UpdateTitle", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@title", title));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region UpdateTermCount
        public void UpdateTermCount(int id, int termcount)
        {
            SqlCommand command = new SqlCommand("Doc_UpdateTermCount", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@termcount", termcount));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region GetIds
        public DataTable GetIds()
        {
            SqlCommand command = new SqlCommand("Doc_GetIds", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetLinkCounts
        public DataTable GetLinkCounts()
        {
            SqlCommand command = new SqlCommand("Doc_GetLinkCounts", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetRecordsP
        public DataSet GetRecordsP(
            string whereClause,
            int PageSize,
            int PageNum,
            string SortExp)
        {
            SqlCommand command = new SqlCommand("Doc_GetRecordsP", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@whereClause", whereClause));
            command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
            command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
            return ExecDataSet(command);
        }
        #endregion

        #region GetAll
        public DataTable GetAll()
        {
            SqlCommand command = new SqlCommand("Doc_GetAll", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetCount
        public int GetCount()
        {
            SqlCommand command = new SqlCommand("Doc_GetCount", Connection);
            command.CommandType = CommandType.StoredProcedure;
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetDocData
        public DataTable GetDocData(int id)
        {
            SqlCommand command = new SqlCommand("Doc_GetDocData", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region UpdatePageRank
        public void UpdatePageRank(int id, float rank)
        {
            SqlCommand command = new SqlCommand("Doc_UpdatePageRank", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@rank", rank));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion
    }
}
