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
    public class PersonImpl : PersonDao
    {
        public void Delete(Person t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Person t)
        {
            throw new NotImplementedException();
        }

        public DataTable Select()
        {
            string query = @"SELECT photo from Person p";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }
        public Person SelectPerson(int idPerson)
        {
            string query = @"SELECT photo from Person p WHERE p.PersonId = @PersonId";
            try
            {
                Person p = new Person();
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@PersonId", idPerson);
                SqlDataReader dt = DBImplementation.ExecuteDataReaderCommand(cmd);
                if (dt.Read())

                {
                    p.Photo= dt.GetValue(0) as byte[];
                }
                cmd.Connection.Close();
                return p;
            }
            catch (Exception ex) { throw ex; }
        }

        public int Update(Person t)
        {
            throw new NotImplementedException();
        }
    }
}
