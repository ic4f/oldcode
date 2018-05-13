using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Ei.Data
{
    public class LineupViewData : DataClass
    {
        #region constructor 
        public LineupViewData() : base() { }
        #endregion

        #region Create
        public int Create(int lineupId, string firstname, string lastname, int administeredBy)
        {
            SqlCommand command = new SqlCommand("LineupView_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@lineupId", lineupId));
            command.Parameters.Add(new SqlParameter("@firstname", firstname));
            command.Parameters.Add(new SqlParameter("@lastname", lastname));
            command.Parameters.Add(new SqlParameter("@administeredBy", administeredBy));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region Complete 
        public void Complete(int id, string relevancenotes)
        {
            SqlCommand command = new SqlCommand("LineupView_Complete", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@relevancenotes", relevancenotes));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region GetCompletedLineupViewsByLineup
        public DataTable GetCompletedLineupViewsByLineup(int lineupId, string sortexp)
        {
            SqlCommand command = new SqlCommand("LineupView_GetCompletedLineupViewsByLineup", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@lineupId", lineupId));
            command.Parameters.Add(new SqlParameter("@sortexp", sortexp));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region Delete
        public int Delete(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecNonQuery("LineupView_Delete", parameters);
        }
        #endregion
    }
}
