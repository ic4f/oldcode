using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Core
{
    public class DataClass
    {
        public DataClass(string connString) { connection = new SqlConnection(connString); }

        //use sparingly!
        public object ExecSqlStringScalar(string sql)
        {
            SqlCommand command = new SqlCommand(sql, connection);
            command.CommandType = CommandType.Text;
            connection.Open();
            object result = command.ExecuteScalar();
            connection.Close();
            return result;
        }

        //use sparingly!
        public DataSet ExecSqlStringDataSet(string sql)
        {
            SqlCommand command = new SqlCommand(sql, connection);
            command.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(ds);
            connection.Close();
            return ds;
        }

        //returns single value
        protected object ExecScalar(string procedure, IDataParameter[] parameters)
        {
            SqlCommand command = BuildCommand(procedure, parameters);
            connection.Open();
            object result = command.ExecuteScalar();
            connection.Close();
            return result;
        }

        //returns # of rows affected
        protected int ExecNonQuery(string procedure, IDataParameter[] parameters)
        {
            SqlCommand command = BuildCommand(procedure, parameters);
            connection.Open();
            int result = Convert.ToInt32(command.ExecuteNonQuery());
            connection.Close();
            return result;
        }

        protected int ExecNonQuery(string procedure)
        {
            SqlCommand command = new SqlCommand(procedure, connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            int result = Convert.ToInt32(command.ExecuteNonQuery());
            connection.Close();
            return result;
        }

        protected DataSet ExecDataSet(SqlCommand command)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(ds);
            connection.Close();
            return ds;
        }

        protected ArrayList ExecFirstColumn(SqlCommand command)
        {
            ArrayList result = new ArrayList();
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                result.Add(reader.GetValue(0));
            connection.Close();
            return result;
        }

        protected ArrayList ExecSingleRow(string procedure, IDataParameter[] parameters)
        {
            SqlCommand command = BuildCommand(procedure, parameters);
            ArrayList result = new ArrayList();
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            for (int i = 0; i < reader.FieldCount; i++)
                result.Add(reader.GetValue(i));
            connection.Close();
            return result;
        }

        protected ArrayList ExecFirstColumn(string procedure, IDataParameter[] parameters)
        {
            SqlCommand command = BuildCommand(procedure, parameters);
            ArrayList result = new ArrayList();
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                result.Add(reader.GetValue(0));
            connection.Close();
            return result;
        }

        public DataSet ExecDataSet(string procedure, IDataParameter[] parameters)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = BuildCommand(procedure, parameters);
            adapter.Fill(ds);
            connection.Close();
            return ds;
        }

        protected DataSet ExecDataSet(DataSet ds, string procedure, IDataParameter[] parameters)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = BuildCommand(procedure, parameters);
            adapter.Fill(ds);
            connection.Close();
            return ds;
        }

        protected DataSet ExecDataSet(string procedure, IDataParameter[] parameters, string table)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = BuildCommand(procedure, parameters);
            adapter.Fill(ds, table);
            connection.Close();
            return ds;
        }

        protected DataSet ExecDataSet(
            string procedure, IDataParameter[] parameters, int startRecord, int maxRecords, string table)
        {
            DataSet ds = new DataSet();

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = BuildCommand(procedure, parameters);
            adapter.Fill(ds, startRecord, maxRecords, table);
            connection.Close();
            return ds;
        }

        protected SqlDataReader ExecReader(string procedure, IDataParameter[] parameters)
        {
            SqlCommand command = BuildCommand(procedure, parameters);
            connection.Open();
            return command.ExecuteReader();
            //client code MUST close reader and connection!!!
        }

        protected SqlConnection Connection { get { return connection; } }

        private SqlCommand BuildCommand(string procedure, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(procedure, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter p in parameters)
                command.Parameters.Add(p);
            return command;
        }

        private SqlConnection connection;
    }
}
