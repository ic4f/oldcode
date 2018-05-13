using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Ei.Data
{
    public class Photo : DataClass
    {
        #region constructor
        public Photo(int recordId) : base()
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

        #region string ExternalReference
        public string ExternalReference
        {
            get { return externalReference; }
            set { externalReference = value; }
        }
        #endregion

        #region string Gender
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        #endregion

        #region int RaceId
        public int RaceId
        {
            get { return raceId; }
            set { raceId = value; }
        }
        #endregion

        #region int HairColorId
        public int HairColorId
        {
            get { return hairColorId; }
            set { hairColorId = value; }
        }
        #endregion

        #region int AgeRangeId
        public int AgeRangeId
        {
            get { return ageRangeId; }
            set { ageRangeId = value; }
        }
        #endregion

        #region int WeightRangeId
        public int WeightRangeId
        {
            get { return weightRangeId; }
            set { weightRangeId = value; }
        }
        #endregion

        #region bool IsCategorized
        public bool IsCategorized
        {
            get { return isCategorized; }
            set { isCategorized = value; }
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

        #region int Update()
        public int Update()
        {
            SqlCommand command = new SqlCommand("Photo_Update", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Id", Id));

            if (ExternalReference != null)
                command.Parameters.Add(new SqlParameter("@externalref", ExternalReference));
            else
                command.Parameters.Add(new SqlParameter("@externalref", System.DBNull.Value));

            command.Parameters.Add(new SqlParameter("@Gender", Gender));
            command.Parameters.Add(new SqlParameter("@RaceId", RaceId));
            command.Parameters.Add(new SqlParameter("@HairColorId", HairColorId));
            command.Parameters.Add(new SqlParameter("@AgeRangeId", AgeRangeId));
            command.Parameters.Add(new SqlParameter("@WeightRangeId", WeightRangeId));
            command.Parameters.Add(new SqlParameter("@IsCategorized", IsCategorized));
            command.Parameters.Add(new SqlParameter("@ModifiedBy", ModifiedBy));

            Connection.Open();
            int result = command.ExecuteNonQuery();
            Connection.Close();

            if (result >= 0)
                loadRecord(id); //some values might have been changed by the db code
            return result;
        }
        #endregion

        #region private

        private void loadRecord(int recordId)
        {
            SqlCommand command = new SqlCommand("Photo_Get", Connection);
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
                externalReference = reader.GetString(1);

            if (reader[2] != System.DBNull.Value)
                gender = reader.GetString(2);

            if (reader[3] != System.DBNull.Value)
                raceId = reader.GetInt32(3);

            if (reader[4] != System.DBNull.Value)
                hairColorId = reader.GetInt32(4);

            if (reader[5] != System.DBNull.Value)
                ageRangeId = reader.GetInt32(5);

            if (reader[6] != System.DBNull.Value)
                weightRangeId = reader.GetInt32(6);

            if (reader[7] != System.DBNull.Value)
                isCategorized = reader.GetBoolean(7);

            if (reader[8] != System.DBNull.Value)
                created = reader.GetDateTime(8);

            if (reader[9] != System.DBNull.Value)
                modified = reader.GetDateTime(9);

            if (reader[10] != System.DBNull.Value)
                modifiedBy = reader.GetInt32(10);

            reader.Close();
            Connection.Close();
        }
        private int id;
        private string externalReference;
        private string gender;
        private int raceId;
        private int hairColorId;
        private int ageRangeId;
        private int weightRangeId;
        private bool isCategorized;
        private DateTime created;
        private DateTime modified;
        private int modifiedBy;

        #endregion
    }
}