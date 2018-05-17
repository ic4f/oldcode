using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class NewsPageData : DataClass
    {
        #region constructor 
        public NewsPageData() : base() { }
        #endregion

        #region Create
        public int Create(int pageId, bool hasImage, bool isOnHomepage, bool IsHighlighted, string summary)
        {
            SqlCommand command = new SqlCommand("NewsPage_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            command.Parameters.Add(new SqlParameter("@hasImage", hasImage));
            command.Parameters.Add(new SqlParameter("@isOnHomepage", isOnHomepage));
            command.Parameters.Add(new SqlParameter("@IsHighlighted", IsHighlighted));
            command.Parameters.Add(new SqlParameter("@summary", summary));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetNewsPages
        public DataTable GetNewsPages(string sortexp)
        {
            SqlCommand command = new SqlCommand("NewsPage_GetNewsPages", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@sortexp", sortexp));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetNewsPagesWithQuery
        public DataTable GetNewsPagesWithQuery(string query)
        {
            SqlCommand command = new SqlCommand("NewsPage_GetNewsPagesWithQuery", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@query", query));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetIdByPageId
        public int GetIdByPageId(int pageId)
        {
            SqlCommand command = new SqlCommand("NewsPage_GetIdByPageId", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetNewsForPublicNewsList
        public DataTable GetNewsForPublicNewsList()
        {
            SqlCommand command = new SqlCommand("NewsPage_GetNewsForPublicNewsList", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region Publish
        public void Publish(int id)
        {
            SqlCommand command = new SqlCommand("NewsPage_Publish", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region Delete
        public int Delete(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecNonQuery("NewsPage_Delete", parameters);
        }
        #endregion

        #region GetNewsForHomepage
        public DataTable GetNewsForHomepage()
        {
            SqlCommand command = new SqlCommand("NewsPage_GetNewsForHomepage", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion
    }
}
