using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Ei.Data
{
    public class SuspectData : DataClass
    {
        #region constructor 
        public SuspectData() : base() { }
        #endregion

        #region GetRecords
        public DataTable GetRecords(string sortexp)
        {
            SqlCommand command = new SqlCommand("Suspect_GetRecords", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@sortexp", sortexp));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetAllSuspectsByCase
        public DataTable GetAllSuspectsByCase(int caseid)
        {
            SqlCommand command = new SqlCommand("Suspect_GetAllSuspectsByCase", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ccaseid", caseid));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetSuspectsByCase
        public DataTable GetSuspectsByCase(int caseid)
        {
            SqlCommand command = new SqlCommand("Suspect_GetSuspectsByCase", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ccaseid", caseid));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region Create
        public int Create(string externalref, string notes,
            string gender, int raceId, int hairColorId, int ageRangeId, int weightRangeId, int modifiedby)
        {
            SqlCommand command = new SqlCommand("Suspect_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@externalref", externalref));
            command.Parameters.Add(new SqlParameter("@notes", notes));
            command.Parameters.Add(new SqlParameter("@gender", gender));
            command.Parameters.Add(new SqlParameter("@raceId", raceId));
            command.Parameters.Add(new SqlParameter("@hairColorId", hairColorId));
            command.Parameters.Add(new SqlParameter("@ageRangeId", ageRangeId));
            command.Parameters.Add(new SqlParameter("@weightRangeId", weightRangeId));
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
            return ExecNonQuery("Suspect_Delete", parameters);
        }
        #endregion
    }
}
