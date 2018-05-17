using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class CmsMenuData : DataClass
    {
        #region constructor 
        public CmsMenuData() : base() { }
        #endregion

        #region GetOrdered
        public DataSet GetOrdered()
        {
            SqlCommand command = new SqlCommand("CmsMenu_GetOrdered", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command);
        }
        #endregion
    }
}
