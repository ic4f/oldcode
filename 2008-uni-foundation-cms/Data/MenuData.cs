using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class MenuData : DataClass
    {
        #region constructor 
        public MenuData() : base() { }
        #endregion

        #region GetOrdered
        public DataTable GetOrdered()
        {
            SqlCommand command = new SqlCommand("Menu_GetOrdered", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetHeaderImageId
        public int GetHeaderImageId(int menuId)
        {
            SqlCommand command = new SqlCommand("Menu_GetHeaderImageId", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@menuId", menuId));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

    }
}
