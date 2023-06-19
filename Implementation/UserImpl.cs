using DAO;
using Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
namespace Implementation
{
    public class UserImpl : UserAccountDao
    {
        public void Delete(UserAccount t)
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

        public string GetForgot(string username)
        {

            string query = @"SELECT p.email  FROM UserAccount ua 
                            INNER JOIN Person p ON p.UserAccountId = ua.UserAccountId 
                            WHERE ua.UserName = @UserName";

            try
            {
                string person="";
                SqlCommand cmd = null;
                cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@UserName", username);
                SqlDataReader dr = DBImplementation.ExecuteDataReaderCommand(cmd);
                while (dr.Read())
                {
                    person = dr.GetString(0); 
                }
                dr.Close();
                cmd.Connection.Close();
                return person;
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
        public int UpdateForgot(string username,string password)
        {
            string query = @"UPDATE UserAccount SET PasswordAccount=@PasswordAccount , KeyPassword=@KeyPassword ,VIPassword = @VIPassword
                            WHERE UserName = @UserName";
            try
            {
                UserAccount userAccount = new UserAccount();
                userAccount.UserName = username;
                userAccount.Password = Encriptar(password);
                userAccount.Key = Key;
                userAccount.VI = IV;


                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserName ", userAccount.UserName));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PasswordAccount ", userAccount.Password));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@KeyPassword ", userAccount.Key));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@VIPassword ", userAccount.VI));

                return DBImplementation.ExecuteBasicCommand(cmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #region usercreation

        Random rdn = new Random();
        public UserAccount users(string name, string last, int id)
        {
            UserAccount usuario;
            string username;
            string password;

            username = "" + name.Substring(0, 1).ToLower() + last.Substring(0, 1).ToLower() + DateTime.Now.Year.ToString() + id;

            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            password = "";

            for (int i = 0; i <= 4; i++)
            {
                password = password + caracteres.Substring(rdn.Next(1, 63), 1);
            }
            usuario = new UserAccount();
            usuario.UserName = username;
            usuario.Passstring = password;
            usuario.RoleUserId = 1;

            return usuario;
        }
        #endregion


        #region encriptar
        static byte[] Key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
            17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32};
        static byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        static byte[] encriptado;
        static AesManaged aes = new AesManaged();


        static byte[] Encriptar(string texto)
        {
            byte[] encriptando;
            Key = aes.Key;
            IV = aes.IV;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform encriptador = aes.CreateEncryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encriptador, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(texto);
                        encriptando = ms.ToArray();
                    }
                }
            }
            return encriptando;
        }
        #endregion

        public int SendEmail(string username)
        {


            try
            {
                Random rdm = new Random();
                int code = 0;
                code = rdm.Next(1000, 10000);
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(DBImplementation.usermail);
                mail.To.Add(GetForgot(username));
                mail.Subject = "Restablecimiento Contraseña";
                mail.Body = "El codigo para el restablecimiento de la contraseña es "+ code;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(DBImplementation.usermail, DBImplementation.passwordmail);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                return code;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Register Mail send whitout problems" +ex.Message));
                Random rdm = new Random();
                return rdm.Next(1000, 10000); 
            }


        }
        public int SendEmailClaim(string Message)
        {


            
                Random rdm = new Random();
                int code = 0;
                code = rdm.Next(1000, 10000);
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(DBImplementation.usermail);
            mail.To.Add(new MailAddress(DBImplementation.usermail));
            mail.Subject = "Reclamo de: "+Session.SessionCurrent;
                mail.Body = "" + Message;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(DBImplementation.usermail, DBImplementation.passwordmail);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                return code;
            


        }
    }
}
