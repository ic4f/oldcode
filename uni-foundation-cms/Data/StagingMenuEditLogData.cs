using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class StagingMenuEditLogData : DataClass
    {
        #region constructor 
        public StagingMenuEditLogData() : base() { }
        #endregion

        #region CreateLog
        public int CreateLog(string description, int userid)
        {
            SqlCommand command = new SqlCommand("StagingMenuEditLog_CreateLog", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@description", description));
            command.Parameters.Add(new SqlParameter("@userid", userid));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetLogs
        public DataTable GetLogs()
        {
            SqlCommand command = new SqlCommand("StagingMenuEditLog_GetLogs", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region ResetLogs
        public void ResetLogs()
        {
            ExecNonQuery("StagingMenuEditLog_ResetLogs");
        }
        #endregion

        #region ClearLogs
        public void ClearLogs()
        {
            ExecNonQuery("StagingMenuEditLog_ClearLogs");
        }
        #endregion
    }
}
