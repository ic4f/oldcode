using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class PermissionData : DataClass
    {
        #region constructor 
        public PermissionData() : base() { }
        #endregion

        #region GetTopCategories
        public DataTable GetTopCategories()
        {
            SqlCommand command = new SqlCommand("PermissionCategory_GetTopCategories", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetCategoriesByParent
        public DataTable GetCategoriesByParent(int catId)
        {
            SqlCommand command = new SqlCommand("PermissionCategory_GetCategoriesByParent", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@catId", catId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetPermissionsByCategory
        public DataTable GetPermissionsByCategory(int catId)
        {
            SqlCommand command = new SqlCommand("Permission_GetPermissionsByCategory", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@catId", catId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion
    }
}
