using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class FileTypeData : DataClass
    {
        #region constructor 
        public FileTypeData() : base() { }
        #endregion

        #region GetRecords
        public DataSet GetRecords(string SortExp)
        {
            SqlCommand command = new SqlCommand("FileType_GetRecords", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
            return ExecDataSet(command);
        }
        #endregion

        #region GetCurrentNonImageTypeList
        public DataTable GetCurrentNonImageTypeList()
        {
            SqlCommand command = new SqlCommand("FileType_GetCurrentNonImageTypeList", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetImageTypeList
        public DataTable GetImageTypeList()
        {
            SqlCommand command = new SqlCommand("FileType_GetImageTypeList", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region IsImage
        public bool IsImage(string Extension)
        {
            SqlCommand command = new SqlCommand("FileType_IsImage", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Extension", Extension));
            Connection.Open();
            bool result = Convert.ToBoolean(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion
    }
}
