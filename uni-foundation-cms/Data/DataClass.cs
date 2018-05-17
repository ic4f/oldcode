using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Foundation.Data
{
    public class DataClass : Core.DataClass
    {
        public DataClass() : base(ConfigurationHelper.ConnectionString) { }
    }
}
