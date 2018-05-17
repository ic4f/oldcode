using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class PageContentLabelData : DataClass
    {
        #region constructor 
        public PageContentLabelData() : base() { }
        #endregion

        #region AddLink 
        public int AddLink(int pageId, int labelId)
        {
            SqlParameter[] parameters = {
                                                                        new SqlParameter("@pageId", pageId),
                                                                        new SqlParameter("@labelId", labelId)};

            return (int)ExecNonQuery("PageContentLabel_AddLink", parameters);
        }
        #endregion

        #region DeleteByPage
        public int DeleteByPage(int pageId)
        {
            SqlParameter[] parameters = { new SqlParameter("@pageId", pageId) };
            return (int)ExecNonQuery("PageContentLabel_DeleteByPage", parameters);
        }
        #endregion
    }
}
