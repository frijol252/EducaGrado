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
    public class CityImpl : CityDao
    {
        public void Delete(City t)
        {
            throw new NotImplementedException();
        }

        public int Insert(City t)
        {
            throw new NotImplementedException();
        }

        public DataTable Select()
        {
            string query = @"SELECT CityId AS 'ID', Name AS 'Name' FROM City c ";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public int Update(City t)
        {
            throw new NotImplementedException();
        }
    }
}
