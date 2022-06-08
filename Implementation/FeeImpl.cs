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
    public class FeeImpl : FeeDao
    {
        public void Delete(Fee t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Fee t)
        {
            throw new NotImplementedException();
        }

        public DataTable Select()
        {
            string query = @"SELECT DISTINCT p.PersonId AS 'ID',CONCAT(p.Names,' ', p.LastName) AS 'Name',COUNT(f.FeeId) AS 'NO' FROM Fee f
INNER JOIN Student s ON f.StudentId =s.StudentId 
INNER JOIN Person p ON p.PersonId =s.StudentId 
WHERE p.SchoolId =@SchoolId AND f.status =1
GROUP BY p.PersonId,p.Names, p.LastName";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }
        public DataTable SelectLike(string like)
        {
            string query = @"SELECT DISTINCT p.PersonId ,CONCAT(p.Names,' ', p.LastName) ,COUNT(f.FeeId)  FROM Fee f
INNER JOIN Student s ON f.StudentId =s.StudentId 
INNER JOIN Person p ON p.PersonId =s.StudentId 
WHERE p.SchoolId =1 AND f.status =1 AND CONCAT(p.Names,' ', p.LastName) LIKE @like
GROUP BY p.PersonId,p.Names, p.LastName";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                cmd.Parameters.AddWithValue("@like", "%"+like+"%");
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }
        public DataTable SelectByStudent(int id)
        {
            string query = @"SELECT f.FeeId AS 'ID','-' AS 'Selected', (f.Amount-f.Balance) 'Monto',f.Balance AS 'Saldo',
                        Deadline AS 'FeachaLimite'
                        FROM Fee f 
                        INNER JOIN Student s ON f.StudentId =s.StudentId 
                        WHERE s.StudentId =@StudentId AND f.status =1";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@StudentId", id);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public int Update(Fee t)
        {
            throw new NotImplementedException();
        }
    }
}
