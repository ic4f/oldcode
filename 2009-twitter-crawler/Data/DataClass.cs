using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace TwitterCrawler.Data
{
    public class DataClass : Core.DataClass
    {
        public DataClass() : base(ConfigHelper.ConnectionString) { }

        public DataClass(string connection) : base(connection) { }
    }
}
