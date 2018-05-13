using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Ei.Data
{
    public class Lineup : DataClass
    {
        #region constructor
        public Lineup(int recordId) : base()
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

        #region string Description
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        #endregion

        #region string Notes
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }
        #endregion

        #region int CaseId
        public int CaseId
        {
            get { return caseId; }
        }
        #endregion

        #region int SuspectId
        public int SuspectId
        {
            get { return suspectId; }
        }
        #endregion

        #region int SuspectPhotoPosition
        public int SuspectPhotoPosition
        {
            get { return suspectPhotoPosition; }
            set { suspectPhotoPosition = value; }
        }
        #endregion

        #region bool IsLocked
        public bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }
        #endregion

        #region DateTime Created
        public DateTime Created
        {
            get { return created; }
        }
        #endregion

        #region DateTime Modified
        public DateTime Modified
        {
            get { return modified; }
        }
        #endregion

        #region int ModifiedBy
        public int ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }
        #endregion

        #region private

        private void loadRecord(int recordId)
        {
            SqlCommand command = new SqlCommand("Lineup_Get", Connection);
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
                description = reader.GetString(1);

            if (reader[2] != System.DBNull.Value)
                notes = reader.GetString(2);

            if (reader[3] != System.DBNull.Value)
                caseId = reader.GetInt32(3);

            if (reader[4] != System.DBNull.Value)
                suspectId = reader.GetInt32(4);

            if (reader[5] != System.DBNull.Value)
                suspectPhotoPosition = reader.GetInt32(5);

            if (reader[6] != System.DBNull.Value)
                isLocked = reader.GetBoolean(6);

            if (reader[7] != System.DBNull.Value)
                created = reader.GetDateTime(7);

            if (reader[8] != System.DBNull.Value)
                modified = reader.GetDateTime(8);

            if (reader[9] != System.DBNull.Value)
                modifiedBy = reader.GetInt32(9);

            reader.Close();
            Connection.Close();
        }
        private int id;
        private string description;
        private string notes;
        private int caseId;
        private int suspectId;
        private int suspectPhotoPosition;
        private bool isLocked;
        private DateTime created;
        private DateTime modified;
        private int modifiedBy;

        #endregion
    }
}