using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = IrProject.Core;

namespace IrProject.Data
{
    public class LinkData : DataClass
    {
        #region constructor 
        public LinkData() : base() { }
        #endregion

        #region Create
        public int Create(string parenturl, string childurl)
        {
            SqlCommand command = new SqlCommand("Link_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@parenturl", parenturl));
            command.Parameters.Add(new SqlParameter("@childurl", childurl));
            Connection.Open();
            command.ExecuteScalar();
            Connection.Close();
            return 1; ;
        }
        #endregion

        #region UpdateText
        public void UpdateText(int fromid, int toid, string text)
        {
            SqlCommand command = new SqlCommand("Link_UpdateText", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@fromid", fromid));
            command.Parameters.Add(new SqlParameter("@toid", toid));
            command.Parameters.Add(new SqlParameter("@text", text));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region GetRecordsByToId
        public DataTable GetRecordsByToId(int toid)
        {
            SqlCommand command = new SqlCommand("Link_GetRecordsByToId", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@docid", toid));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetInboundLinksP
        public DataSet GetInboundLinksP(
            int docid,
            int PageSize,
            int PageNum,
            string SortExp)
        {
            SqlCommand command = new SqlCommand("Link_GetInboundLinksP", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@docid", docid));
            command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
            command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
            return ExecDataSet(command);
        }
        #endregion

        #region GetOutboundLinksP
        public DataSet GetOutboundLinksP(
            int docid,
            int PageSize,
            int PageNum,
            string SortExp)
        {
            SqlCommand command = new SqlCommand("Link_GetOutboundLinksP", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@docid", docid));
            command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
            command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
            return ExecDataSet(command);
        }
        #endregion

        #region GetLinksSortByTo
        public DataTable GetLinksSortByTo()
        {
            SqlCommand command = new SqlCommand("Link_GetLinksSortByTo", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetLinksSortByFrom
        public DataTable GetLinksSortByFrom()
        {
            SqlCommand command = new SqlCommand("Link_GetLinksSortByFrom", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetAll
        public DataTable GetAll()
        {
            SqlCommand command = new SqlCommand("Link_GetAll", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion
    }
}
