using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class PageTagData : DataClass
    {
        #region constructor 
        public PageTagData() : base() { }
        #endregion

        #region AddLink 
        public int AddLink(int pageId, int tagid)
        {
            SqlParameter[] parameters = {
                                                                        new SqlParameter("@pageId", pageId),
                                                                        new SqlParameter("@tagid", tagid)};

            return (int)ExecNonQuery("PageTag_AddLink", parameters);
        }
        #endregion

        #region DeleteByPage
        public int DeleteByPage(int pageId)
        {
            SqlParameter[] parameters = { new SqlParameter("@pageId", pageId) };
            return (int)ExecNonQuery("PageTag_DeleteByPage", parameters);
        }
        #endregion
    }
}
