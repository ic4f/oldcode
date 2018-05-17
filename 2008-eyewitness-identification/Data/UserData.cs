using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Ei.Data
{
    public class UserData : DataClass
    {
        #region constructor 
        public UserData() : base() { }
        #endregion

        #region GetAllUsersByCase
        public DataTable GetAllUsersByCase(int caseId)
        {
            SqlCommand command = new SqlCommand("User_GetAllUsersByCase", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ccaseId", caseId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion
    }
}
