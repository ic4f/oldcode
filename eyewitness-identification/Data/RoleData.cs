using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Ei.Data
{
    public class RoleData : DataClass
    {
        public static int Administrator_Role_Id = 1;

        #region constructor 
        public RoleData() : base() { }
        #endregion

        #region GetRecords
        public DataTable GetRecords(string sortexp)
        {
            SqlCommand command = new SqlCommand("Role_GetRecords", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@sortexp", sortexp));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetAllRolesByUser
        public DataTable GetAllRolesByUser(int cmsuserId)
        {
            SqlCommand command = new SqlCommand("Role_GetAllRolesByUser", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@cmsuserId", cmsuserId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region Create
        public int Create(string name, int modifiedby)
        {
            SqlCommand command = new SqlCommand("Role_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@name", name));
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
            return ExecNonQuery("Role_Delete", parameters);
        }
        #endregion

        #region AddPermission
        public void AddPermission(int roleId, int permissionId)
        {
            SqlCommand command = new SqlCommand("Role_AddPermission", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@roleId", roleId));
            command.Parameters.Add(new SqlParameter("@permissionId", permissionId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region DeletePermission
        public void DeletePermission(int roleId, int permissionId)
        {
            SqlCommand command = new SqlCommand("Role_DeletePermission", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@roleId", roleId));
            command.Parameters.Add(new SqlParameter("@permissionId", permissionId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region GetPermissions
        public ArrayList GetPermissions(int roleId)
        {
            SqlCommand command = new SqlCommand("Role_GetPermissions", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@roleId", roleId));
            return ExecFirstColumn(command);
        }
        #endregion
    }
}
