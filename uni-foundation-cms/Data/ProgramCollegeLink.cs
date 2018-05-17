using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Foundation.Data
{
    public class ProgramCollegeLink : DataClass
    {
        #region constructor 
        public ProgramCollegeLink() : base() { }
        #endregion

        #region Create 
        public void Create(int programPageId, int collegePageId)
        {
            SqlCommand command = new SqlCommand("ProgramCollege_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@programPageId", programPageId));
            command.Parameters.Add(new SqlParameter("@collegePageId", collegePageId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region DeleteByProgram
        public void DeleteByProgram(int programPageId)
        {
            SqlCommand command = new SqlCommand("ProgramCollege_DeleteByProgram", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@programPageId", programPageId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion
    }
}
