using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Ei.Data
{
    public class PhotoData : DataClass
    {
        #region constructor 
        public PhotoData() : base() { }
        #endregion

        #region Create
        public int Create(string externalref)
        {
            SqlCommand command = new SqlCommand("Photo_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@externalref", externalref));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetUncategoriedNumber
        public int GetUncategoriedNumber()
        {
            SqlCommand command = new SqlCommand("Photo_GetUncategoriedNumber", Connection);
            command.CommandType = CommandType.StoredProcedure;
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetNextNUncategorized
        public DataTable GetNextNUncategorized(int n)
        {
            SqlCommand command = new SqlCommand("Photo_GetNextNUncategorized", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@n", n));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetPhotosWithQueryP
        public DataSet GetPhotosWithQueryP(string query, int PageSize, int PageNum)
        {
            SqlCommand command = new SqlCommand("Photo_GetPhotosWithQueryP", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@query", query));
            command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
            return ExecDataSet(command);
        }
        #endregion

        #region GetPhotosWithQuery
        public DataSet GetPhotosWithQuery(string query, string orderby, int toDisplay)
        {
            SqlCommand command = new SqlCommand("Photo_GetPhotosWithQuery", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@query", query));
            command.Parameters.Add(new SqlParameter("@orderby", orderby));
            command.Parameters.Add(new SqlParameter("@toDisplay", toDisplay));
            return ExecDataSet(command);
        }
        #endregion

        #region GetPhotosByLineup
        public DataTable GetPhotosByLineup(int lineupId)
        {
            SqlCommand command = new SqlCommand("Photo_GetPhotosByLineup", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@lineupId", lineupId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion
    }
}
