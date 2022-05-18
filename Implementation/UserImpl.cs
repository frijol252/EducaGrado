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
            string query = @"SELECT UA.UserAccountId , UA.UserName,UA.PasswordAccount, UA.KeyPassword, UA.VIPassword, UA.status,UA.revisionpass ,
                            (SELECT CONCAT(Names,' ',LastName,' ',ISNULL(SLastName,''))  FROM Person p WHERE p.UserAccountId=UA.UserAccountId) AS 'Full Name',
                            (SELECT p.PersonId  FROM Person p WHERE p.UserAccountId=UA.UserAccountId) AS 'Person Id',
                            (SELECT p.SchoolId  FROM Person p WHERE p.UserAccountId=UA.UserAccountId) AS 'School Id',
                            (SELECT s.Name  FROM Person p INNER JOIN School s ON s.SchoolId = p.SchoolId  WHERE p.UserAccountId=UA.UserAccountId) AS 'SchoolName',
                            (SELECT st.NameType  FROM Person p INNER JOIN School s ON s.SchoolId = p.SchoolId INNER JOIN SchoolType st ON st.SchoolTypeId = s.SchoolTypeId  
                            WHERE p.UserAccountId=UA.UserAccountId) AS 'School Tipo',
                            (SELECT st.numberCourses  FROM Person p INNER JOIN School s ON s.SchoolId = p.SchoolId INNER JOIN SchoolType st ON st.SchoolTypeId = s.SchoolTypeId  
                            WHERE p.UserAccountId=UA.UserAccountId) AS 'School Cursos',
                            (SELECT m.NumberGrades FROM Person p INNER JOIN School s ON s.SchoolId = p.SchoolId INNER JOIN Modality m ON m.ModalityId = s.ModalityId
                            WHERE p.UserAccountId=UA.UserAccountId) AS 'School Notas',
                            (SELECT m.NumberTest  FROM Person p INNER JOIN School s ON s.SchoolId = p.SchoolId INNER JOIN Modality m ON m.ModalityId = s.ModalityId
                            WHERE p.UserAccountId=UA.UserAccountId) AS 'School Examen',
                            (SELECT m.TypeQualify  FROM Person p INNER JOIN School s ON s.SchoolId = p.SchoolId INNER JOIN Modality m ON m.ModalityId = s.ModalityId  
                            WHERE p.UserAccountId=UA.UserAccountId) AS 'School Calificacion',
                            UA.RoleUserId 
                            from UserAccount UA
                            WHERE UA.UserName = @userName";
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
            string query = @"UPDATE UserAccount SET PasswordAccount=@PasswordAccount , KeyPassword=@KeyPassword ,VIPassword = @VIPassword, 
                            revisionpass = @revisionpass WHERE UserAccountId = @UserAccountId";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@PasswordAccount", Encoding.Default.GetString(t.Password)).SqlDbType = SqlDbType.VarChar;
                cmd.Parameters.AddWithValue("@KeyPassword", Encoding.Default.GetString(t.Key)).SqlDbType = SqlDbType.VarChar;
                cmd.Parameters.AddWithValue("@VIPassword", Encoding.Default.GetString(t.VI)).SqlDbType = SqlDbType.VarChar;
                cmd.Parameters.AddWithValue("@revisionpass", t.RevisionPass).SqlDbType = SqlDbType.TinyInt;
                cmd.Parameters.AddWithValue("@UserAccountId", t.UserID);

                return DBImplementation.ExecuteBasicCommand(cmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
