using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class FileContentLabelData : DataClass
    {
        #region constructor 
        public FileContentLabelData() : base() { }
        #endregion

        #region AddLink 
        public int AddLink(int fileId, int labelId)
        {
            SqlParameter[] parameters = {
                                                                        new SqlParameter("@fileId", fileId),
                                                                        new SqlParameter("@labelId", labelId)};

            return (int)ExecNonQuery("FileContentLabel_AddLink", parameters);
        }
        #endregion

        #region DeleteByFile 
        public int DeleteByFile(int fileId)
        {
            SqlParameter[] parameters = { new SqlParameter("@fileId", fileId) };
            return (int)ExecNonQuery("FileContentLabel_DeleteByFile", parameters);
        }
        #endregion
    }
}
