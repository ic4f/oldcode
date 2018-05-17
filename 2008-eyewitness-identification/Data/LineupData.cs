using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Ei.Data
{
    public class LineupData : DataClass
    {
        #region constructor 
        public LineupData() : base() { }
        #endregion

        #region Create
        public int Create(string description, string notes, int caseId, int suspectId, int modifiedby)
        {
            SqlCommand command = new SqlCommand("Lineup_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@description", description));
            command.Parameters.Add(new SqlParameter("@notes", notes));
            command.Parameters.Add(new SqlParameter("@ccaseId", caseId));
            command.Parameters.Add(new SqlParameter("@suspectId", suspectId));
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
            return ExecNonQuery("Lineup_Delete", parameters);
        }
        #endregion

        #region Lock 
        public void Lock(int id, int position, int modifiedby)
        {
            SqlCommand command = new SqlCommand("Lineup_Lock", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@position", position));
            command.Parameters.Add(new SqlParameter("@modifiedby", modifiedby));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region GetLockedLineupsByCase
        public DataTable GetLockedLineupsByCase(int caseId, string sortexp)
        {
            SqlCommand command = new SqlCommand("Lineup_GetLockedLineupsByCase", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ccaseId", caseId));
            command.Parameters.Add(new SqlParameter("@sortexp", sortexp));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetLockedLineupsBySuspect
        public DataTable GetLockedLineupsBySuspect(int suspectId)
        {
            SqlCommand command = new SqlCommand("Lineup_GetLockedLineupsBySuspect", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@suspectId", suspectId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion
    }
}
