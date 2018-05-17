using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class PageModuleData : DataClass
    {
        #region constructor 
        public PageModuleData() : base() { }
        #endregion

        #region AddLink 
        public int AddLink(int pageId, int moduleid)
        {
            SqlParameter[] parameters = {
                                                                        new SqlParameter("@pageId", pageId),
                                                                        new SqlParameter("@moduleid", moduleid)};

            return (int)ExecNonQuery("PageModule_AddLink", parameters);
        }
        #endregion

        #region DeleteByPage
        public int DeleteByPage(int pageId)
        {
            SqlParameter[] parameters = { new SqlParameter("@pageId", pageId) };
            return (int)ExecNonQuery("PageModule_DeleteByPage", parameters);
        }
        #endregion

        #region DeleteByModule
        public int DeleteByModule(int moduleId)
        {
            SqlParameter[] parameters = { new SqlParameter("@moduleId", moduleId) };
            return (int)ExecNonQuery("PageModule_DeleteByModule", parameters);
        }
        #endregion
    }
}
