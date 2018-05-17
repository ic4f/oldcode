using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class PageCategoryData : DataClass
    {
        #region constructor 
        public PageCategoryData() : base() { }
        #endregion

        #region Create
        public int Create(string text)
        {
            SqlCommand command = new SqlCommand("PageCategory_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@text", text));
            command.Parameters.Add(new SqlParameter("@menuId", System.DBNull.Value));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region Create
        public int Create(string text, int menuId)
        {
            SqlCommand command = new SqlCommand("PageCategory_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@text", text));
            command.Parameters.Add(new SqlParameter("@menuId", menuId));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetList
        public DataTable GetList()
        {
            SqlCommand command = new SqlCommand("PageCategory_GetList", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetCategoriesWithMenus
        public DataTable GetCategoriesWithMenus()
        {
            SqlCommand command = new SqlCommand("PageCategory_GetCategoriesWithMenus", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region HasMenu
        public bool HasMenu(int pagecatId)
        {
            //			SqlCommand command = new SqlCommand("PageCategory_HasMenu", Connection);
            //			command.CommandType = CommandType.StoredProcedure;
            //			command.Parameters.Add(new SqlParameter("@pagecatId", pagecatId));
            //			Connection.Open();
            //			bool result = Convert.ToBoolean(command.ExecuteScalar());
            //			Connection.Close();
            //			return result;

            //commented out after killing the deparrtments feature - for convenience.
            return true;
        }
        #endregion
    }
}
