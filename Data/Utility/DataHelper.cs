using ActividadPractica.Data.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadPractica.Data.Utility
{
    public class DataHelper
    {
        private static DataHelper _instance;
        private SqlConnection _connection;

        private DataHelper()
        {
            _connection = new SqlConnection(Properties.Resources.CadenaConexionLocal);
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }
        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }

        public DataTable ExecuteSPQuery(string sp, params SqlParameter[] parametros)
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                using (var cmd = new SqlCommand(sp, _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parametros != null && parametros.Length > 0)
                        cmd.Parameters.AddRange(parametros);

                    dt.Load(cmd.ExecuteReader());
                }
            }
            catch (SqlException ex)
            {
                dt = null;
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }


        public int ExecuteSPNonQuery(string sp, List<Parameter> parameters)
        {
            int filasAfectadas = 0;

            try
            {
                _connection.Open();
                using (var cmd = new SqlCommand(sp, _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (var p in parameters)
                    {
                        cmd.Parameters.AddWithValue(p.Name, p.Value ?? DBNull.Value);
                    }

                    filasAfectadas = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                filasAfectadas = -1;
            }
            finally
            {
                _connection.Close();
            }

            return filasAfectadas;
        }

        public DataTable ExecuteSPQuery(string sp)
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;
                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {
                dt = null;
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }

    }
}
