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
    public class TownImpl : TownDao
    {
        public void Delete(Town t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Town t)
        {
            throw new NotImplementedException();
        }

        public DataTable Select()
        {
            throw new NotImplementedException();
        }

        public DataTable Select(int idprovince)
        {
            string query = @"SELECT t.TownId AS 'ID', t.townName AS 'Name'
                            FROM Town t 
                            INNER JOIN Province p ON p.ProvinceId =t.ProvinceId 
                            WHERE t.ProvinceId = @ProvinceId";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@ProvinceId", idprovince);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public int Update(Town t)
        {
            throw new NotImplementedException();
        }
    }
}
