using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class ProgramDepartmentLink : DataClass
    {
        #region constructor 
        public ProgramDepartmentLink() : base() { }
        #endregion

        #region Create 
        public void Create(int programPageId, int departmentPageId)
        {
            SqlCommand command = new SqlCommand("ProgramDepartment_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@programPageId", programPageId));
            command.Parameters.Add(new SqlParameter("@departmentPageId", departmentPageId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region DeleteByProgram
        public void DeleteByProgram(int programPageId)
        {
            SqlCommand command = new SqlCommand("ProgramDepartment_DeleteByProgram", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@programPageId", programPageId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion
    }
}
