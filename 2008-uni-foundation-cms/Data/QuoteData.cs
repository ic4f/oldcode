using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class QuoteData : DataClass
    {
        #region constructor 
        public QuoteData() : base() { }
        #endregion

        #region Create
        public int Create(string text, string author, string admincomment, int modifiedby)
        {
            SqlCommand command = new SqlCommand("Quote_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@text", text));
            command.Parameters.Add(new SqlParameter("@author", author));
            command.Parameters.Add(new SqlParameter("@admincomment", admincomment));
            command.Parameters.Add(new SqlParameter("@modifiedby", modifiedby));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetRecords
        public DataTable GetRecords(string SortExp)
        {
            SqlCommand command = new SqlCommand("Quote_GetRecords", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetQuotesForDisplay
        public DataTable GetQuotesForDisplay()
        {
            SqlCommand command = new SqlCommand("Quote_GetQuotesForDisplay", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region Delete
        public int Delete(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecNonQuery("Quote_Delete", parameters);
        }
        #endregion
    }
}
