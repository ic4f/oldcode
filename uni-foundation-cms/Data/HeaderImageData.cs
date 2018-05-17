using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class HeaderImageData : DataClass
    {
        #region constructor 
        public HeaderImageData() : base() { }
        #endregion

        #region Create
        public int Create(int locationcode, string description, int modifiedby)
        {
            SqlCommand command = new SqlCommand("HeaderImage_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@locationcode", locationcode));
            command.Parameters.Add(new SqlParameter("@description", description));
            command.Parameters.Add(new SqlParameter("@modifiedby", modifiedby));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetRecords
        public DataTable GetRecords(int locationcode, string SortExp)
        {
            SqlCommand command = new SqlCommand("HeaderImage_GetRecords", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@locationcode", locationcode));
            command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetImagesForDisplay
        public DataTable GetImagesForDisplay(int locationcode)
        {
            SqlCommand command = new SqlCommand("HeaderImage_GetImagesForDisplay", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@locationcode", locationcode));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region Delete
        public int Delete(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecNonQuery("HeaderImage_Delete", parameters);
        }
        #endregion

        #region GetDependentMenus
        public DataTable GetDependentMenus(int himageid)
        {
            SqlCommand command = new SqlCommand("HeaderImage_GetDependentMenus", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@himageid", himageid));
            return ExecDataSet(command).Tables[0];
        }
        #endregion
    }
}
