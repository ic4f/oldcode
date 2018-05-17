using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Ei.Data
{
    public class LineupView : DataClass
    {
        #region constructor
        public LineupView(int recordId) : base()
        {
            loadRecord(recordId);
        }
        #endregion

        #region int Id
        public int Id
        {
            get { return id; }
        }
        #endregion

        #region int LineupId
        public int LineupId
        {
            get { return lineupId; }
        }
        #endregion

        #region string WitnessExternalRef
        public string WitnessExternalRef
        {
            get { return witnessExternalRef; }
        }
        #endregion

        #region string WitnessFirstName
        public string WitnessFirstName
        {
            get { return witnessFirstName; }
        }
        #endregion

        #region string WitnessLastName
        public string WitnessLastName
        {
            get { return witnessLastName; }
        }
        #endregion

        #region string RelevanceNotes
        public string RelevanceNotes
        {
            get { return relevanceNotes; }
        }
        #endregion

        #region bool IsCompleted
        public bool IsCompleted
        {
            get { return isCompleted; }
        }
        #endregion

        #region DateTime Administered
        public DateTime Administered
        {
            get { return administered; }
        }
        #endregion

        #region int AdministeredBy
        public int AdministeredBy
        {
            get { return administeredBy; }
        }
        #endregion

        #region string AdministeredByName
        public string AdministeredByName
        {
            get { return administeredByName; }
        }
        #endregion

        #region private

        private void loadRecord(int recordId)
        {
            SqlCommand command = new SqlCommand("LineupView_Get", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Id", recordId));

            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
                throw new Core.AppException("Record with id = " + recordId + " not found.");
            reader.Read();

            if (reader[0] != System.DBNull.Value)
                id = reader.GetInt32(0);

            if (reader[1] != System.DBNull.Value)
                lineupId = reader.GetInt32(1);

            if (reader[2] != System.DBNull.Value)
                witnessExternalRef = reader.GetString(2);

            if (reader[3] != System.DBNull.Value)
                witnessFirstName = reader.GetString(3);

            if (reader[4] != System.DBNull.Value)
                witnessLastName = reader.GetString(4);

            if (reader[5] != System.DBNull.Value)
                relevanceNotes = reader.GetString(5);

            if (reader[6] != System.DBNull.Value)
                isCompleted = reader.GetBoolean(6);

            if (reader[7] != System.DBNull.Value)
                administered = reader.GetDateTime(7);

            if (reader[8] != System.DBNull.Value)
                administeredBy = reader.GetInt32(8);

            if (reader[9] != System.DBNull.Value)
                administeredByName = reader.GetString(9);

            reader.Close();
            Connection.Close();
        }
        private int id;
        private int lineupId;
        private string witnessExternalRef;
        private string witnessFirstName;
        private string witnessLastName;
        private string relevanceNotes;
        private bool isCompleted;
        private DateTime administered;
        private int administeredBy;
        private string administeredByName;

        #endregion
    }
}