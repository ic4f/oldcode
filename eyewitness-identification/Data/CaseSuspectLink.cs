using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Ei.Data
{
    public class CaseSuspectLink : DataClass
    {
        #region constructor 
        public CaseSuspectLink() : base() { }
        #endregion

        #region Create 
        public void Create(int caseId, int suspectId)
        {
            SqlCommand command = new SqlCommand("CCaseSuspect_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ccaseId", caseId));
            command.Parameters.Add(new SqlParameter("@suspectId", suspectId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region DeleteByCase
        public void DeleteByCase(int caseId)
        {
            SqlCommand command = new SqlCommand("CCaseSuspect_DeleteByCase", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ccaseId", caseId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region DeleteByUser
        public void DeleteByUser(int suspectId)
        {
            SqlCommand command = new SqlCommand("CCaseSuspect_DeleteBySuspect", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@suspectId", suspectId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion
    }
}
