using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Ei.Data
{
    public class PhotoViewData : DataClass
    {
        #region constructor 
        public PhotoViewData() : base() { }
        #endregion

        #region CreatePhotoView
        public int CreatePhotoView(int lineupviewid, int photoid, int resultcode, string certainty)
        {
            SqlCommand command = new SqlCommand("PhotoView_CreatePhotoView", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@lineupviewid", lineupviewid));
            command.Parameters.Add(new SqlParameter("@photoid", photoid));
            command.Parameters.Add(new SqlParameter("@resultcode", resultcode));
            command.Parameters.Add(new SqlParameter("@certainty", certainty));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region CreateSuspectView
        public int CreateSuspectView(int lineupviewid, int resultcode, string certainty)
        {
            SqlCommand command = new SqlCommand("PhotoView_CreateSuspectView", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@lineupviewid", lineupviewid));
            command.Parameters.Add(new SqlParameter("@resultcode", resultcode));
            command.Parameters.Add(new SqlParameter("@certainty", certainty));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
        #endregion

        #region GetByLineupView
        public DataTable GetByLineupView(int lineupviewid)
        {
            SqlCommand command = new SqlCommand("PhotoView_GetByLineupView", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@lineupviewid", lineupviewid));
            return ExecDataSet(command).Tables[0];
        }
        #endregion
    }
}
