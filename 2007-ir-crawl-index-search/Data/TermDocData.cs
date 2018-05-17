using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = IrProject.Core;

namespace IrProject.Data
{
    public class TermDocData : DataClass
    {
        #region constructor 
        public TermDocData() : base() { }
        #endregion

        #region Create 
        public void Create(
            string term,
            int docId,
            int textCount,
            int boldCount,
            int headerCount,
            int anchorCount,
            int titleCount,
            int urlCount,
            int totalCount)
        {
            SqlCommand command = new SqlCommand("TermDoc_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@term", term));
            command.Parameters.Add(new SqlParameter("@docId", docId));
            command.Parameters.Add(new SqlParameter("@textCount", textCount));
            command.Parameters.Add(new SqlParameter("@boldCount", boldCount));
            command.Parameters.Add(new SqlParameter("@headerCount", headerCount));
            command.Parameters.Add(new SqlParameter("@anchorCount", anchorCount));
            command.Parameters.Add(new SqlParameter("@titleCount", titleCount));
            command.Parameters.Add(new SqlParameter("@urlCount", urlCount));
            command.Parameters.Add(new SqlParameter("@totalCount", totalCount));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region UpdateAnchorTextCount
        public void UpdateAnchorTextCount(string term, int docId, int count) //creates term record if new, sets anchortextcount to count
        {
            SqlCommand command = new SqlCommand("TermDoc_UpdateAnchorTextCount", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@term", term));
            command.Parameters.Add(new SqlParameter("@docId", docId));
            command.Parameters.Add(new SqlParameter("@count", count));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region GetRecords
        public DataTable GetRecords()
        {
            SqlCommand command = new SqlCommand("TermDoc_GetRecords", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region UpdateTermFreqs
        public void UpdateTermFreqs(string term, int docId, int tf, int tfa) //updates termfreq and termfreqexternalanchor
        {
            SqlCommand command = new SqlCommand("TermDoc_UpdateTermFreqs", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@term", term));
            command.Parameters.Add(new SqlParameter("@docId", docId));
            command.Parameters.Add(new SqlParameter("@tf", tf));
            command.Parameters.Add(new SqlParameter("@tfa", tfa));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region GetAll
        public DataTable GetAll()
        {
            SqlCommand command = new SqlCommand("TermDoc_GetAll", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetAll
        public DataTable GetAllTermWeights()
        {
            SqlCommand command = new SqlCommand("TermWeight_GetAll", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion
    }
}
