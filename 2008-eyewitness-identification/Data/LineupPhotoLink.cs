using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Ei.Data
{
    public class LineupPhotoLink : DataClass
    {
        #region constructor 
        public LineupPhotoLink() : base() { }
        #endregion

        #region Create 
        public void Create(int lineupId, int photoId)
        {
            SqlCommand command = new SqlCommand("LineupPhoto_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@lineupId", lineupId));
            command.Parameters.Add(new SqlParameter("@photoId", photoId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region DeleteByCase
        public void DeleteByCase(int lineupId)
        {
            SqlCommand command = new SqlCommand("LineupPhoto_DeleteByLineup", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@lineupId", lineupId));
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion
    }
}
