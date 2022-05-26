using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using Model;

namespace Implementation
{
    public class StudentImpl : StudentDao
    {
        public void Delete(Student t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Student t)
        {
            throw new NotImplementedException();
        }

        public DataTable Select()
        {
            throw new NotImplementedException();
        }

        public int Update(Student t)
        {
            throw new NotImplementedException();
        }
        public int Updates(byte[] asd)
        {
            string query = @"UPDATE Person SET photo = @photo WHERE PersonId = 1";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);

                cmd.Parameters.AddWithValue("@photo", Encoding.Default.GetString(asd)).SqlDbType = SqlDbType.VarChar;
                return DBImplementation.ExecuteBasicCommand(cmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
