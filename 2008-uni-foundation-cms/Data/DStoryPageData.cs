using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class DStoryPageData : DataClass
    {
        #region constructor 
        public DStoryPageData() : base() { }
        #endregion

        #region Create
        public int Create(int pageId, bool hasImage, bool isFeatured, string donorDisplayedName, string summary)
        {
            SqlCommand command = new SqlCommand("DStoryPage_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            command.Parameters.Add(new SqlParameter("@hasImage", hasImage));
            command.Parameters.Add(new SqlParameter("@isFeatured", isFeatured));
            command.Parameters.Add(new SqlParameter("@donorDisplayedName", donorDisplayedName));
            command.Parameters.Add(new SqlParameter("@summary", summary));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetDStoryPages
        public DataTable GetDStoryPages(string sortexp)
        {
            SqlCommand command = new SqlCommand("DStoryPage_GetDStoryPages", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@sortexp", sortexp));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetDStoryPagesWithQuery
        public DataTable GetDStoryPagesWithQuery(string query)
        {
            SqlCommand command = new SqlCommand("DStoryPage_GetDStoryPagesWithQuery", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@query", query));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetIdByPageId
        public int GetIdByPageId(int pageId)
        {
            SqlCommand command = new SqlCommand("DStoryPage_GetIdByPageId", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetDStoriesForPublicDStoryList
        public DataTable GetDStoriesForPublicDStoryList()
        {
            SqlCommand command = new SqlCommand("DStoryPage_GetDStoriesForPublicDStoryList", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region Delete
        public int Delete(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecNonQuery("DStoryPage_Delete", parameters);
        }
        #endregion

        #region GetDStoriesForHomepage
        public DataTable GetDStoriesForHomepage()
        {
            SqlCommand command = new SqlCommand("DStoryPage_GetDStoriesForHomepage", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion
    }
}
