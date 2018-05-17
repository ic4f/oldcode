using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;


namespace Ei.Data
{
    public class CmsUser : DataClass
    {
        #region constructor
        public CmsUser(int recordId) : base()
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

        #region int UserId
        public int UserId
        {
            get { return userId; }
        }
        #endregion

        #region string Login
        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        #endregion

        #region string Password
        public string Password
        {
            get { return (new Core.EncryptionTool()).Decrypt((byte[])password); }
            set { password = (new Core.EncryptionTool()).Encrypt(value); }
        }
        #endregion

        #region string Prefix
        public string Prefix
        {
            get { return prefix; }
            set { prefix = value; }
        }
        #endregion

        #region string FirstName
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        #endregion

        #region string Middle
        public string Middle
        {
            get { return middle; }
            set { middle = value; }
        }
        #endregion

        #region string LastName
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        #endregion

        #region string Suffix
        public string Suffix
        {
            get { return suffix; }
            set { suffix = value; }
        }
        #endregion

        #region string DisplayedName
        public string DisplayedName
        {
            get { return displayedName; }
            set { displayedName = value; }
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

        #region string ModifiedByName
        public string ModifiedByName
        {
            get { return modifiedByName; }
        }
        #endregion

        #region int Update()
        public int Update()
        {
            SqlCommand command = new SqlCommand("User_Update", Connection); //not a typo: use main user table only
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Id", UserId));

            if (Login != null)
                command.Parameters.Add(new SqlParameter("@Login", Login));
            else
                command.Parameters.Add(new SqlParameter("@Login", System.DBNull.Value));

            if (Password != null)
                command.Parameters.Add(new SqlParameter("@Password", (new Core.EncryptionTool()).Encrypt(Password)));
            else
                command.Parameters.Add(new SqlParameter("@Password", System.DBNull.Value));

            if (Prefix != null)
                command.Parameters.Add(new SqlParameter("@Prefix", Prefix));
            else
                command.Parameters.Add(new SqlParameter("@Prefix", System.DBNull.Value));

            if (FirstName != null)
                command.Parameters.Add(new SqlParameter("@FirstName", FirstName));
            else
                command.Parameters.Add(new SqlParameter("@FirstName", System.DBNull.Value));

            if (Middle != null)
                command.Parameters.Add(new SqlParameter("@Middle", Middle));
            else
                command.Parameters.Add(new SqlParameter("@Middle", System.DBNull.Value));

            if (LastName != null)
                command.Parameters.Add(new SqlParameter("@LastName", LastName));
            else
                command.Parameters.Add(new SqlParameter("@LastName", System.DBNull.Value));

            if (Suffix != null)
                command.Parameters.Add(new SqlParameter("@Suffix", Suffix));
            else
                command.Parameters.Add(new SqlParameter("@Suffix", System.DBNull.Value));

            if (DisplayedName != null)
                command.Parameters.Add(new SqlParameter("@DisplayedName", DisplayedName));
            else
                command.Parameters.Add(new SqlParameter("@DisplayedName", System.DBNull.Value));

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
            SqlCommand command = new SqlCommand("CmsUser_Get", Connection);
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
                userId = reader.GetInt32(1);

            if (reader[2] != System.DBNull.Value)
                login = reader.GetString(2);

            if (reader[3] != System.DBNull.Value)
                password = (byte[])reader.GetValue(3);

            if (reader[4] != System.DBNull.Value)
                prefix = reader.GetString(4);

            if (reader[5] != System.DBNull.Value)
                firstName = reader.GetString(5);

            if (reader[6] != System.DBNull.Value)
                middle = reader.GetString(6);

            if (reader[7] != System.DBNull.Value)
                lastName = reader.GetString(7);

            if (reader[8] != System.DBNull.Value)
                suffix = reader.GetString(8);

            if (reader[9] != System.DBNull.Value)
                displayedName = reader.GetString(9);

            if (reader[10] != System.DBNull.Value)
                created = reader.GetDateTime(10);

            if (reader[11] != System.DBNull.Value)
                modified = reader.GetDateTime(11);

            if (reader[12] != System.DBNull.Value)
                modifiedBy = reader.GetInt32(12);

            if (reader[13] != System.DBNull.Value)
                modifiedByName = reader.GetString(13);

            reader.Close();
            Connection.Close();
        }
        private int id;
        private int userId;
        private string login;
        private byte[] password;
        private string prefix;
        private string firstName;
        private string middle;
        private string lastName;
        private string suffix;
        private string displayedName;
        private DateTime created;
        private DateTime modified;
        private int modifiedBy;
        private string modifiedByName;

        #endregion
    }
}