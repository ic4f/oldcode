using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Ei.Data
{
    public class CaseData : DataClass
    {
        #region constructor 
        public CaseData() : base() { }
        #endregion

        #region GetRecords
        public DataTable GetRecords(string sortexp)
        {
            SqlCommand command = new SqlCommand("CCase_GetRecords", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@sortexp", sortexp));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetAllCasesByUser
        public DataTable GetAllCasesByUser(int userId)
        {
            SqlCommand command = new SqlCommand("CCase_GetAllCasesByUser", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@userId", userId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetCasesWithSuspectsByUser
        public DataTable GetCasesWithSuspectsByUser(int userId)
        {
            SqlCommand command = new SqlCommand("CCase_GetCasesWithSuspectsByUser", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@userId", userId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetCases
        public DataTable GetCases()
        {
            SqlCommand command = new SqlCommand("CCase_GetCases", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region Create
        public int Create(string number, string notes, int modifiedby)
        {
            SqlCommand command = new SqlCommand("CCase_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@externalref", number));
            command.Parameters.Add(new SqlParameter("@notes", notes));
            command.Parameters.Add(new SqlParameter("@modifiedBy", modifiedby));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region Delete
        public int Delete(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecNonQuery("CCase_Delete", parameters);
        }
        #endregion
    }
}
