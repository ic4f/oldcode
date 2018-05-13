using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace TwitterCrawler.Data
{
    public class HahstagData : DataClass
    {
        public HahstagData() : base() { }

        public int Create(string tag)
        {
            SqlCommand command = new SqlCommand("Hashtag_Create", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@tag", tag));
            Connection.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            Connection.Close();
            return result;
        }
    }
}