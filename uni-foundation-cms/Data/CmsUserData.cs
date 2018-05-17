using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class CmsUserData : DataClass
    {
        #region constructor 
        public CmsUserData() : base() { }
        #endregion

        #region ValidateUser 
        public int ValidateUser(string login, string password)
        {
            SqlCommand command = new SqlCommand("CmsUser_ValidateUser", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@login", login));
            command.Parameters.Add(new SqlParameter("@password", new c.EncryptionTool().Encrypt(password)));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetUserIdByLogin
        public int GetUserIdByLogin(string login)
        {
            SqlCommand command = new SqlCommand("CmsUser_GetUserIdByLogin", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@login", login));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetPermissionCodesByUser
        public ArrayList GetPermissionCodesByUser(int cmsUserId)
        {
            SqlCommand command = new SqlCommand("CmsUser_GetPermissionCodesByUser", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@cmsUserId", cmsUserId));
            return ExecFirstColumn(command);
        }
        #endregion

        #region Create
        public int Create(
            string Login,
            string Password,
            string FirstName,
            string Middle,
            string LastName,
            int ModifiedBy)
        {
            SqlCommand command = new SqlCommand("CmsUser_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Login", Login));
            command.Parameters.Add(new SqlParameter("@Password", (new Core.EncryptionTool()).Encrypt(Password)));
            command.Parameters.Add(new SqlParameter("@FirstName", FirstName));
            command.Parameters.Add(new SqlParameter("@Middle", Middle));
            command.Parameters.Add(new SqlParameter("@LastName", LastName));
            command.Parameters.Add(new SqlParameter("@ModifiedBy", ModifiedBy));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetRecords
        public DataTable GetRecords(string SortExp)
        {
            SqlCommand command = new SqlCommand("CmsUser_GetRecords", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@sortexp", SortExp));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region Remove 
        public void Remove(int cmsuserid)
        {
            SqlCommand command = new SqlCommand("CmsUser_Delete", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@cmsuserid", cmsuserid));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region Activate 
        public void Activate(int userid)
        {
            SqlCommand command = new SqlCommand("CmsUser_Activate", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@userid", userid));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region GetAllUsersByRole
        public DataTable GetAllUsersByRole(int roleId)
        {
            SqlCommand command = new SqlCommand("CmsUser_GetAllUsersByRole", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@roleId", roleId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetArchivedUsers
        public DataTable GetArchivedUsers(string SortExp)
        {
            SqlCommand command = new SqlCommand("CmsUser_GetArchivedUsers", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@sortexp", SortExp));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region LogUserSignIn
        public void LogUserSignIn(int userid)
        {
            SqlCommand command = new SqlCommand("CmsUser_LogUserSignIn", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@userid", userid));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region LogUserSignOut
        public void LogUserSignOut(int userid)
        {
            SqlCommand command = new SqlCommand("CmsUser_LogUserSignOut", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@userid", userid));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region GetUserLogs
        public DataTable GetUserLogs(int userid)
        {
            SqlCommand command = new SqlCommand("CmsUser_GetUserLogs", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@userid", userid));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetModificationsByUser
        public DataTable GetModificationsByUser(int userid)
        {
            SqlCommand command = new SqlCommand("CmsUser_GetModificationsByUser", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@userid", userid));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetAllUsersList
        public DataTable GetAllUsersList()
        {
            SqlCommand command = new SqlCommand("CmsUser_GetAllUsersList", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetIdByUserId
        public int GetIdByUserId(int userId)
        {
            SqlCommand command = new SqlCommand("CmsUser_GetIdByUserId", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@userId", userId));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion
    }
}
