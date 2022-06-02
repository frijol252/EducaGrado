using DAO;
using Model;
using System;
using System.Data;
using System.Data.SqlClient;

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
