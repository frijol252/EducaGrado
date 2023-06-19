using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Implementation
{
    public class DBImplementation
    {
        public static string usermail = "educateam.suport@gmail.com";
        public static string passwordmail = "vrllkubjymonwish";
        public static string connectionString = "data source = 127.0.0.1; initial catalog = EducaGrado; user id = sa; password = Univalle2019";
        public static string pathImages = @"../educaimages/";

        #region Comands
        public static SqlCommand CreateBasicComand()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            return cmd;
        }

        public static SqlCommand CreateBasicComand(string query)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Connection = connection;
            return cmd;
        }
        #endregion

        #region Comands eject
        public static int GetIdentityFromTable(string table)
        {
            int res = -1;
            string query = "SELECT ISNULL(IDENT_CURRENT('" + table + "'),0) + IDENT_INCR('" + table + "')";
            try
            {
                SqlCommand cmd = CreateBasicComand(query);
                res = int.Parse(ExecuteScalarCommand(cmd));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
        public static int GetIncementFromTable(string table)
        {
            int res = -1;
            string query = "SELECT IDENT_INCR('" + table + "')";
            try
            {
                SqlCommand cmd = CreateBasicComand(query);
                res = int.Parse(ExecuteScalarCommand(cmd));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
        public static string ExecuteScalarCommand(SqlCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                return cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error: {1}.", DateTime.Now, ex.Message));
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public static List<SqlCommand> CreateNBasicCommands(int n)
        {
            List<SqlCommand> res = new List<SqlCommand>();
            SqlConnection connection = new SqlConnection(connectionString);
            for (int i = 0; i < n; i++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                res.Add(cmd);
            }
            return res;
        }



        public static void ExecuteNBasicCommand(List<SqlCommand> cmds)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd1 = cmds[0];
            try
            {
                cmd1.Connection.Open();
                transaction = cmd1.Connection.BeginTransaction();
                foreach (SqlCommand cmd in cmds)
                {
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                cmd1.Connection.Close();
            }
        }


        public static int ExecuteBasicCommand(SqlCommand cmd)
        {
            int res = -1;
            try
            {
                cmd.Connection.Open();
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return res;
        }

        //metodo Select
        public static DataTable ExecuteDataTableCommand(SqlCommand cmd)
        {
            DataTable res = new DataTable();
            try
            {
                cmd.Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(res);
                adapter.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }

            return res;
        }

        //reader
        public static SqlDataReader ExecuteDataReaderCommand(SqlCommand cmd)
        {
            SqlDataReader res = null;
            try
            {
                cmd.Connection.Open();
                res = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //acá no se cierra la conexión. se cierra una vez llamado al metodo
            return res;
        }
        #endregion


    }
}
