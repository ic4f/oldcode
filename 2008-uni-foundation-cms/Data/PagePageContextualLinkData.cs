using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class PagePageContextualLinkData : DataClass
    {
        #region constructor 
        public PagePageContextualLinkData() : base() { }
        #endregion

        #region AddLink
        public void AddLink(int fromPageId, int toPageId)
        {
            SqlCommand command = new SqlCommand("PagePageContextual_AddLink", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@fromPageId", fromPageId));
            command.Parameters.Add(new SqlParameter("@toPageId", toPageId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region DeleteByPage
        public int DeleteByPage(int pageid)
        {
            SqlParameter[] parameters = { new SqlParameter("@pageid", pageid) };
            return ExecNonQuery("PagePageContextual_DeleteByPage", parameters);
        }
        #endregion
    }
}
