using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = IrProject.Core;
namespace IrProject.Data
{
    public class SearchData : DataClass
    {
        #region constructor 
        public SearchData() : base() { }
        #endregion

        #region GetSearchResults
        public DataSet GetSearchResults(string queryterms, string queryweights, float w)
        {
            SqlCommand command = new SqlCommand("Searching_GetSearchResults", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@queryterms", queryterms));
            command.Parameters.Add(new SqlParameter("@queryweights", queryweights));
            command.Parameters.Add(new SqlParameter("@w", w));
            return ExecDataSet(command);
        }
        #endregion

        #region GetSearchResults_w
        public DataSet GetSearchResults_w(string queryterms, string queryweights, float w)
        {
            SqlCommand command = new SqlCommand("Searching_GetSearchResults_w", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@queryterms", queryterms));
            command.Parameters.Add(new SqlParameter("@queryweights", queryweights));
            command.Parameters.Add(new SqlParameter("@w", w));
            return ExecDataSet(command);
        }
        #endregion

        #region GetSearchResults_a
        public DataSet GetSearchResults_a(string queryterms, string queryweights, float w)
        {
            SqlCommand command = new SqlCommand("Searching_GetSearchResults_a", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@queryterms", queryterms));
            command.Parameters.Add(new SqlParameter("@queryweights", queryweights));
            command.Parameters.Add(new SqlParameter("@w", w));
            return ExecDataSet(command);
        }
        #endregion

        #region GetSearchResults_wa
        public DataSet GetSearchResults_wa(string queryterms, string queryweights, float w)
        {
            SqlCommand command = new SqlCommand("Searching_GetSearchResults_wa", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@queryterms", queryterms));
            command.Parameters.Add(new SqlParameter("@queryweights", queryweights));
            command.Parameters.Add(new SqlParameter("@w", w));
            return ExecDataSet(command);
        }
        #endregion

        #region GetSimilarityResults
        public DataSet GetSimilarityResults(string queryterms, string queryweights, float w)
        {
            SqlCommand command = new SqlCommand("Searching_GetSimilarityResults", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@queryterms", queryterms));
            command.Parameters.Add(new SqlParameter("@queryweights", queryweights));
            command.Parameters.Add(new SqlParameter("@w", w));
            return ExecDataSet(command);
        }
        #endregion

        #region GetSimilarityResults_w
        public DataSet GetSimilarityResults_w(string queryterms, string queryweights, float w)
        {
            SqlCommand command = new SqlCommand("Searching_GetSimilarityResults_w", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@queryterms", queryterms));
            command.Parameters.Add(new SqlParameter("@queryweights", queryweights));
            command.Parameters.Add(new SqlParameter("@w", w));
            return ExecDataSet(command);
        }
        #endregion

        #region GetSimilarityResults_a
        public DataSet GetSimilarityResults_a(string queryterms, string queryweights, float w)
        {
            SqlCommand command = new SqlCommand("Searching_GetSimilarityResults_a", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@queryterms", queryterms));
            command.Parameters.Add(new SqlParameter("@queryweights", queryweights));
            command.Parameters.Add(new SqlParameter("@w", w));
            return ExecDataSet(command);
        }
        #endregion

        #region GetSimilarityResults_wa
        public DataSet GetSimilarityResults_wa(string queryterms, string queryweights, float w)
        {
            SqlCommand command = new SqlCommand("Searching_GetSimilarityResults_wa", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@queryterms", queryterms));
            command.Parameters.Add(new SqlParameter("@queryweights", queryweights));
            command.Parameters.Add(new SqlParameter("@w", w));
            return ExecDataSet(command);
        }
        #endregion

        #region GetTermsByDocId
        public DataSet GetTermsByDocIdP(
            int docid,
            int PageSize,
            int PageNum,
            string SortExp)
        {
            SqlCommand command = new SqlCommand("Searching_GetTermsByDocIdP", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@docid", docid));
            command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
            command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
            return ExecDataSet(command);
        }
        #endregion
    }
}
