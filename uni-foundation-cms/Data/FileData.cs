using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class FileData : DataClass
    {
        #region constructor 
        public FileData() : base() { }
        #endregion

        #region Create
        public int Create(
            string Extension,
            string Description,
            string Comment,
            int ModifiedBy)
        {
            SqlCommand command = new SqlCommand("File_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Extension", Extension));
            command.Parameters.Add(new SqlParameter("@Description", Description));
            command.Parameters.Add(new SqlParameter("@Comment", Comment));
            command.Parameters.Add(new SqlParameter("@ModifiedBy", ModifiedBy));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetNonImageFiles
        public DataSet GetNonImageFiles(string SortExp)
        {
            SqlCommand command = new SqlCommand("File_GetNonImageFiles", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
            return ExecDataSet(command);
        }
        #endregion

        #region GetNonImageFilesWithQuery
        public DataTable GetNonImageFilesWithQuery(string query)
        {
            SqlCommand command = new SqlCommand("File_GetNonImageFilesWithQuery", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@query", query));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetImages
        public DataSet GetImages(string SortExp)
        {
            SqlCommand command = new SqlCommand("File_GetImages", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
            return ExecDataSet(command);
        }
        #endregion

        #region GetImagesWithQuery
        public DataTable GetImagesWithQuery(string query)
        {
            SqlCommand command = new SqlCommand("File_GetImagesWithQuery", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@query", query));
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region Delete
        public int Delete(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecNonQuery("File_Delete", parameters);
        }
        #endregion

        #region GetInboundPageLinks
        public DataSet GetInboundPageLinks(int fileId)
        {
            SqlCommand command = new SqlCommand("File_GetInboundPageLinks", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@fileId", fileId));
            return ExecDataSet(command);
        }
        #endregion
    }
}
