using DAO;
using Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Implementation
{
    public class ProvinceImpl : ProvinceDao
    {
        public void Delete(Province t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Province t)
        {
            throw new NotImplementedException();
        }

        public DataTable Select()
        {

            throw new NotImplementedException();
        }
        public DataTable Select(int idcity)
        {
            string query = @"SELECT p.ProvinceId AS 'ID', p.provinceName AS 'Name' FROM Province p
                            INNER JOIN City c ON c.CityId = p.CityId 
                            WHERE p.CityId =@CityId";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@CityId", idcity);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }
        public int Update(Province t)
        {
            throw new NotImplementedException();
        }
    }
}
