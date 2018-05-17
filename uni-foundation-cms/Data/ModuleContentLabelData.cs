using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class ModuleContentLabelData : DataClass
    {
        #region constructor 
        public ModuleContentLabelData() : base() { }
        #endregion

        #region AddLink 
        public int AddLink(int moduleId, int labelId)
        {
            SqlParameter[] parameters = {
                                                                        new SqlParameter("@moduleId", moduleId),
                                                                        new SqlParameter("@labelId", labelId)};

            return (int)ExecNonQuery("ModuleContentLabel_AddLink", parameters);
        }
        #endregion

        #region DeleteByModule
        public int DeleteByModule(int moduleId)
        {
            SqlParameter[] parameters = { new SqlParameter("@moduleId", moduleId) };
            return (int)ExecNonQuery("ModuleContentLabel_DeleteByModule", parameters);
        }
        #endregion
    }
}
