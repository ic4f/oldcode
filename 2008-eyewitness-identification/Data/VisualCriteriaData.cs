using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using c = Core;

namespace Ei.Data
{
    public class VisualCriteriaData : DataClass
    {
        #region constructor 
        public VisualCriteriaData() : base() { }
        #endregion

        #region GetRaceList
        public DataTable GetRaceList()
        {
            SqlCommand command = new SqlCommand("Race_GetList", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetHairColorList
        public DataTable GetHairColorList()
        {
            SqlCommand command = new SqlCommand("HairColor_GetList", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetAgeRangeList
        public DataTable GetAgeRangeList()
        {
            SqlCommand command = new SqlCommand("AgeRange_GetList", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion

        #region GetWeightRangeList
        public DataTable GetWeightRangeList()
        {
            SqlCommand command = new SqlCommand("WeightRange_GetList", Connection);
            command.CommandType = CommandType.StoredProcedure;
            return ExecDataSet(command).Tables[0];
        }
        #endregion
    }
}
