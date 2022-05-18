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
    public class UserImpl : UserAccountDao
    {
        public int Delete(UserAccount t)
        {
            throw new NotImplementedException();
        }

        public DataTable GET(string userName)
        {
            DataTable res = new DataTable();
            string query = @"SELECT UserAccountId , UserName,PasswordAccount, KeyPassword, VIPassword, status  from UserAccount 
                            WHERE UserName = @UserName";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@userName", userName);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int Insert(UserAccount t)
        {
            string query = @"INSERT INTO UserAccount  (UserName , PasswordAccount , KeyPassword ,VIPassword, RoleUserId)
                            VALUES(@UserName, @PasswordAccount, @KeyPassword, @VIPassword, @RoleUserId)";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);

                cmd.Parameters.AddWithValue("@userName", t.UserName);
                cmd.Parameters.AddWithValue("@PasswordAccount", Encoding.Default.GetString(t.Password)).SqlDbType = SqlDbType.VarChar;
                cmd.Parameters.AddWithValue("@KeyPassword", Encoding.Default.GetString(t.Key)).SqlDbType = SqlDbType.VarChar;
                cmd.Parameters.AddWithValue("@VIPassword", Encoding.Default.GetString(t.VI)).SqlDbType = SqlDbType.VarChar;
                cmd.Parameters.AddWithValue("@RoleUserId", t.RoleUserId).SqlDbType = SqlDbType.SmallInt;

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

        public int Update(UserAccount t)
        {
            throw new NotImplementedException();
        }
    }
}
