using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = IrProject.Core;

namespace IrProject.Data
{
    public class TermData : DataClass
    {
        #region constructor 
        public TermData() : base() { }
        #endregion

        #region UpdateCounts 
        public void UpdateCounts(string term, int totalcount) //creates term record if new, adds addcount to totalcount and increments doccount by 1
        {
            SqlCommand command = new SqlCommand("Term_UpdateCounts", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@term", term));
            command.Parameters.Add(new SqlParameter("@totalcount", totalcount));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region UpdateIdf 
        public void UpdateIdf(string term, float idf)
        {
            SqlCommand command = new SqlCommand("Term_UpdateIdf", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@term", term));
            command.Parameters.Add(new SqlParameter("@idf", idf));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region UpdateIdfA
        public void UpdateIdfA(string term, float idfa)
        {
            SqlCommand command = new SqlCommand("Term_UpdateIdfA", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@term", term));
            command.Parameters.Add(new SqlParameter("@idfa", idfa));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region GetDocCounts
        public DataTable GetDocCounts()
        {
            SqlCommand command = new SqlCommand("Term_GetDocCounts", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetDocCountsA
        public DataTable GetDocCountsA()
        {
            SqlCommand command = new SqlCommand("Term_GetDocCountsA", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetAll
        public DataTable GetAll()
        {
            SqlCommand command = new SqlCommand("Term_GetAll", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion
    }
}
