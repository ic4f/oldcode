using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = IrProject.Core;
namespace IrProject.Data
{
    public class StopList : DataClass
    {
        private Hashtable sl;

        #region constructor 
        public StopList() : base() { load(); }
        #endregion

        public bool Contains(string term) { return sl.Contains(term); }

        private void load()
        {
            sl = new Hashtable();
            SqlCommand command = new SqlCommand("StopList_GetTerms", Connection);
            command.CommandType = CommandType.StoredProcedure;
            DataTable dt = ExecDataSet(command).Tables[0];
            foreach (DataRow dr in dt.Rows)
                sl.Add(dr[0].ToString(), true);
        }
    }
}
