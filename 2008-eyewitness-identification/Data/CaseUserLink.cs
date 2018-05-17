using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Ei.Data
{
    public class CaseUserLink : DataClass
    {
        #region constructor 
        public CaseUserLink() : base() { }
        #endregion

        #region Create 
        public void Create(int caseId, int userId)
        {
            SqlCommand command = new SqlCommand("CCaseUser_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ccaseId", caseId));
            command.Parameters.Add(new SqlParameter("@userId", userId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region DeleteByCase
        public void DeleteByCase(int caseId)
        {
            SqlCommand command = new SqlCommand("CCaseUser_DeleteByCase", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ccaseId", caseId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region DeleteByUser
        public void DeleteByUser(int userId)
        {
            SqlCommand command = new SqlCommand("CCaseUser_DeleteByUser", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@userId", userId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion
    }
}
