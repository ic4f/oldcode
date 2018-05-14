using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace IrProject.Data
{
    public class DataClass : Core.DataClass
    {
        public DataClass() : base(System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"]) { }
    }
}
