using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class ModuleData : DataClass
    {
        #region constructor 
        public ModuleData() : base() { }
        #endregion

        #region Create
        public int Create(
            string admintitle,
            string imageextension,
            string title,
            string body,
            string externallink,
            int pageid,
            bool isrequired,
            bool isarchived,
            int modifiedby)
        {
            SqlCommand command = new SqlCommand("Module_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;

            if (pageid == -1)
            {
                command.Parameters.Add(new SqlParameter("@pageid", System.DBNull.Value));
                command.Parameters.Add(new SqlParameter("@externallink", externallink));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@pageid", pageid));
                command.Parameters.Add(new SqlParameter("@externallink", System.DBNull.Value));
            }

            command.Parameters.Add(new SqlParameter("@admintitle", admintitle));
            command.Parameters.Add(new SqlParameter("@imageextension", imageextension));
            command.Parameters.Add(new SqlParameter("@title", title));
            command.Parameters.Add(new SqlParameter("@body", body));
            command.Parameters.Add(new SqlParameter("@isrequired", isrequired));
            command.Parameters.Add(new SqlParameter("@isarchived", isarchived));
            command.Parameters.Add(new SqlParameter("@modifiedby", modifiedby));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetModules
        public DataTable GetModules()
        {
            SqlCommand command = new SqlCommand("Module_GetModules", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetRecords
        public DataTable GetRecords()
        {
            SqlCommand command = new SqlCommand("Module_GetRecords", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetModulesByLabelId
        public DataTable GetModulesByLabelId(int labelId)
        {
            SqlCommand command = new SqlCommand("Module_GetModulesByLabelId", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@labelId", labelId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetForDisplay
        public DataTable GetForDisplay(int pageId)
        {
            SqlCommand command = new SqlCommand("Module_GetForDisplay", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetModulesForPageAssingment
        public DataTable GetModulesForPageAssingment()
        {
            SqlCommand command = new SqlCommand("Module_GetModulesForPageAssingment", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetModulesForPageAssingmentByPage
        public DataTable GetModulesForPageAssingmentByPage(int pageId)
        {
            SqlCommand command = new SqlCommand("Module_GetModulesForPageAssingmentByPage", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@pageId", pageId));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetTitles
        public DataTable GetTitles()
        {
            SqlCommand command = new SqlCommand("Module_GetTitles", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region Delete
        public int Delete(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecNonQuery("Module_Delete", parameters);
        }
        #endregion

        #region UpdateRank
        public void UpdateRank(int id, int rank, int modifiedby)
        {
            SqlCommand command = new SqlCommand("Module_UpdateRank", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@rank", rank));
            command.Parameters.Add(new SqlParameter("@modifiedby", modifiedby));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region GetList
        public DataTable GetList()
        {
            SqlCommand command = new SqlCommand("Module_GetList", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion
    }
}
