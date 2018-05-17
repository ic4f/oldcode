using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class CmsUserRoleLink : DataClass
    {
        #region constructor 
        public CmsUserRoleLink() : base() { }
        #endregion

        #region Create 
        public void Create(int cmsUserId, int roleId)
        {
            SqlCommand command = new SqlCommand("CmsUserRole_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@cmsUserId", cmsUserId));
            command.Parameters.Add(new SqlParameter("@roleId", roleId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region DeleteByUser
        public void DeleteByUser(int cmsUserId)
        {
            SqlCommand command = new SqlCommand("CmsUserRole_DeleteByUser", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@cmsUserId", cmsUserId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region DeleteByRole
        public void DeleteByRole(int roleId)
        {
            SqlCommand command = new SqlCommand("CmsUserRole_DeleteByRole", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@roleId", roleId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion
    }
}
