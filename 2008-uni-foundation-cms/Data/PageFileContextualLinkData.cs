using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class PageFileContextualLinkData : DataClass
    {
        #region constructor 
        public PageFileContextualLinkData() : base() { }
        #endregion

        #region AddLink
        public void AddLink(int fromPageId, int toFileId)
        {
            SqlCommand command = new SqlCommand("PageFileContextual_AddLink", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@fromPageId", fromPageId));
            command.Parameters.Add(new SqlParameter("@toFileId", toFileId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region DeleteByPage
        public int DeleteByPage(int pageid)
        {
            SqlParameter[] parameters = { new SqlParameter("@pageid", pageid) };
            return ExecNonQuery("PageFileContextual_DeleteByPage", parameters);
        }
        #endregion
    }
}
