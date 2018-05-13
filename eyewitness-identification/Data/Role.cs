using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Ei.Data
{
    public class Role : DataClass
    {
        #region constructor
        public Role(int recordId) : base()
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

        #region string Name
        public string Name
        {
            get { return name; }
            set { name = value; }
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
            SqlCommand command = new SqlCommand("Role_Update", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Id", Id));

            if (Name != null)
                command.Parameters.Add(new SqlParameter("@Name", Name));
            else
                command.Parameters.Add(new SqlParameter("@Name", System.DBNull.Value));

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
            SqlCommand command = new SqlCommand("Role_Get", Connection);
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
                name = reader.GetString(1);

            if (reader[2] != System.DBNull.Value)
                created = reader.GetDateTime(2);

            if (reader[3] != System.DBNull.Value)
                modified = reader.GetDateTime(3);

            if (reader[4] != System.DBNull.Value)
                modifiedBy = reader.GetInt32(4);

            reader.Close();
            Connection.Close();
        }
        private int id;
        private string name;
        private DateTime created;
        private DateTime modified;
        private int modifiedBy;

        #endregion
    }
}