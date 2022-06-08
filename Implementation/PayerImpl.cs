using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class PayerImpl : PayerDao
    {
        public void Delete(Payer t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Payer t)
        {
            string query = @"INSERT INTO Payer (NIT,BusinessName)
                            VALUES (@NIT,@BusinessName)";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);

                cmd.Parameters.AddWithValue("@NIT", t.Nit);
                cmd.Parameters.AddWithValue("@BusinessName", t.BusinessName);

                return DBImplementation.ExecuteBasicCommand(cmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable Select()
        {
            throw new NotImplementedException();
        }
        public DataTable Select(string nit)
        {
            string query = @"SELECT IdPayer,BusinessName FROM Payer WHERE NIT = @NIT ";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@NIT", nit);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public int Update(Payer t)
        {
            throw new NotImplementedException();
        }
    }
}
