using DAO;
using Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Implementation
{
    public class GradeImpl : GradeDao
    {
        public void Delete(Grade t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Grade t)
        {
            throw new NotImplementedException();
        }

        public DataTable Select()
        {
            string query = @"EXEC SelectGradesStudent @UserAccountId";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@UserAccountId", Session.SessionID);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public int Update(Grade t)
        {
            throw new NotImplementedException();
        }
    }
}
