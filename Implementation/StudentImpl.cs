using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DAO;
using Model;

namespace Implementation
{
    public class StudentImpl : StudentDao
    {
        public void Delete(Student t)
        {
            throw new NotImplementedException();
        }
        public int DeleteStudent(int idPerson)
        {
            string query = @"UPDATE UserAccount SET status = 2 WHERE UserAccountId = (SELECT UserAccountId FROM Person WHERE PersonId = @PersonId)";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);

                cmd.Parameters.AddWithValue("@PersonId", idPerson);
                return DBImplementation.ExecuteBasicCommand(cmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int DeleteDis(int idPerson,int status)
        {
            string query = @"UPDATE UserAccount SET status = @status WHERE UserAccountId = (SELECT UserAccountId FROM Person WHERE PersonId = @PersonId)";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);

                cmd.Parameters.AddWithValue("@PersonId", idPerson);
                cmd.Parameters.AddWithValue("@status", status);
                return DBImplementation.ExecuteBasicCommand(cmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Person Get(int idperson)
        {
            
            string query = @"SELECT p.Names ,p.LastName ,p.SLastName,p.address ,p.CI ,p.CIExtension,p.DateBirth,p.photo ,p.email,p.latitude ,p.longitude ,p.phone,p.gender ,p.TownId ,s.RudeNumber, t.ProvinceId
                            FROM Person p 
                            INNER JOIN Student s ON s.StudentId =p.PersonId 
                            INNER JOIN Town t ON t.TownId = p.TownId
                            WHERE p.PersonId = 3";
            
            try
            {
                Person person = new Person();
                SqlCommand cmd = null;
                cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@PersonId", idperson);
                SqlDataReader dr = DBImplementation.ExecuteDataReaderCommand(cmd);
                while (dr.Read())
                {
                    person.Names = dr.GetString(0); person.LastName = dr.GetString(1); if (!dr.IsDBNull(2)) person.SecondLastName = dr.GetString(2);
                    person.Address = dr.GetString(3); person.Ci = dr.GetString(4); if (!dr.IsDBNull(5)) person.Ciextension = dr.GetString(5);
                    person.BirthDate = dr.GetDateTime(6); person.Photo = dr.GetValue(7) as byte[]; person.Email = dr.GetString(8); person.Latitude = Decimal.ToDouble(dr.GetDecimal(9));
                    person.Longitude = Decimal.ToDouble(dr.GetDecimal(10)); person.Phone = dr.GetString(11); person.Gender = dr.GetString(12); person.TownId = dr.GetInt32(13);
                    person.Extra = dr.GetString(14); person.SchoolId = dr.GetInt16(15);

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
        public int Insert(Student t)
        {
            throw new NotImplementedException();
        }
        public void InsertTransact(Student t,Person p)
        {
            string queryUser = @"INSERT INTO UserAccount (UserName,PasswordAccount,KeyPassword,VIPassword,RoleUserId)
                                VALUES (@UserName,@PasswordAccount,@KeyPassword,@VIPassword,@RoleUserId)";
            string queryPerson = @"INSERT INTO Person (UserAccountId ,TownId ,SchoolId ,Names ,LastName,
                                SLastName,address,CI,CIExtension,DateBirth,photo,email,latitude,longitude,phone,gender)
                                VALUES (@UserAccountId,@TownId,@SchoolId,@Names,@LastName,@SLastName,@address,@CI,@CIExtension,@DateBirth,
                                @photo,@email,@latitude,@longitude,@phone,@gender)";
            string queryStudent = @"INSERT INTO Student (StudentId,CourseId,RudeNumber)
                                    VALUES(@StudentId,@CourseId,@RudeNumber)";
            string queryGrade = @"INSERT INTO Grade (StudentId,ClassId,TypeGrade)
                                VALUES(@StudentId,@ClassId,@TypeGrade)";
            string querySection = @"INSERT INTO SectionGrade (GradeId,Score,TypeScore)
                                    VALUES(@GradeId,@Score,@TypeScore)";
            try
            {
                #region ATRIBUTOS
                int clasescount=0;
                int idUser = DBImplementation.GetIdentityFromTable("UserAccount");
                int idPerson = DBImplementation.GetIdentityFromTable("Person");
                int idgrade = DBImplementation.GetIdentityFromTable("Grade");
                int idgradeincrement = DBImplementation.GetIncementFromTable("Grade");
                int cantgrades = 0, canttest = 0, totalgrades, typeq = 1, totalextras;


                UserAccount userAccount = new UserAccount();
                userAccount = users(p.Names,p.LastName, idUser);
                userAccount.Password = Encriptar(userAccount.Passstring);
                userAccount.Key = Key;
                userAccount.VI = IV;

                    #region contarClases
                    DataTable dtclases = new DataTable();
                    ClassImpl classImpl = new ClassImpl();
                    dtclases = classImpl.Select(t.CourseId);
                    foreach (DataRow d in dtclases.Rows)
                    {
                        clasescount++;
                    }
                    #endregion

                    #region contarNotas
                    DataTable dtnotas = new DataTable();
                    ModalityImpl modalityImpl = new ModalityImpl();
                    dtnotas = modalityImpl.Select();
                    foreach (DataRow d in dtnotas.Rows)
                    {
                        cantgrades = int.Parse(d[0].ToString());
                        canttest = int.Parse(d[1].ToString());
                        typeq = int.Parse(d[2].ToString());
                    }
                    totalgrades = cantgrades + canttest;
                    totalgrades = totalgrades * typeq;
                    totalgrades += typeq;
                #endregion
                totalextras = totalgrades * clasescount;
                #endregion
                List<SqlCommand> cmds = DBImplementation.CreateNBasicCommands(3+totalextras);
                #region user insert
                cmds[0].CommandText = queryUser;
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserName ", userAccount.UserName));
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@PasswordAccount ", userAccount.Password));
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@KeyPassword ", userAccount.Key));
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@VIPassword ", userAccount.VI));
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@RoleUserId ", userAccount.RoleUserId));
                #endregion
                #region person insert
                cmds[1].CommandText = queryPerson;
                cmds[1].Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserAccountId ", idUser));
                cmds[1].Parameters.Add(new System.Data.SqlClient.SqlParameter("@TownId ", p.TownId));
                cmds[1].Parameters.Add(new System.Data.SqlClient.SqlParameter("@SchoolId ", Session.SessionSchoolId));
                cmds[1].Parameters.Add(new System.Data.SqlClient.SqlParameter("@Names ", p.Names));
                cmds[1].Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastName ", p.LastName));
                cmds[1].Parameters.Add(new System.Data.SqlClient.SqlParameter("@SLastName", p.SecondLastName ?? Convert.DBNull));
                cmds[1].Parameters.Add(new System.Data.SqlClient.SqlParameter("@address ", p.Address));
                cmds[1].Parameters.Add(new System.Data.SqlClient.SqlParameter("@CI ", p.Ci));
                cmds[1].Parameters.Add(new System.Data.SqlClient.SqlParameter("@CIExtension ", p.Ciextension ?? Convert.DBNull));
                cmds[1].Parameters.Add(new System.Data.SqlClient.SqlParameter("@DateBirth ", p.BirthDate));
                cmds[1].Parameters.Add(new System.Data.SqlClient.SqlParameter("@photo ", p.Photo));
                cmds[1].Parameters.Add(new System.Data.SqlClient.SqlParameter("@email ", p.Email));
                cmds[1].Parameters.Add(new System.Data.SqlClient.SqlParameter("@latitude ", p.Latitude));
                cmds[1].Parameters.Add(new System.Data.SqlClient.SqlParameter("@longitude ", p.Longitude));
                cmds[1].Parameters.Add(new System.Data.SqlClient.SqlParameter("@phone ", p.Phone));
                cmds[1].Parameters.Add(new System.Data.SqlClient.SqlParameter("@gender ", p.Gender));
                #endregion
                #region student insert 
                cmds[2].CommandText = queryStudent;
                cmds[2].Parameters.Add(new System.Data.SqlClient.SqlParameter("@StudentId ", idPerson));
                cmds[2].Parameters.Add(new System.Data.SqlClient.SqlParameter("@CourseId ", t.CourseId));
                cmds[2].Parameters.Add(new System.Data.SqlClient.SqlParameter("@RudeNumber ", t.RudeNume));
                #endregion
                #region GRADES
                int contadorcmd = 3;
                for (int j = 1; j <= typeq; j++)
                {
                    foreach (DataRow dataRow in dtclases.Rows)
                    {
                        cmds[contadorcmd].CommandText = queryGrade;
                        cmds[contadorcmd].Parameters.AddWithValue("@StudentId", idPerson);
                        cmds[contadorcmd].Parameters.AddWithValue("@ClassId", int.Parse(dataRow[0].ToString()));
                        cmds[contadorcmd].Parameters.AddWithValue("@TypeGrade", j);
                        contadorcmd++;
                        for (int k = 0; k < cantgrades; k++)
                        {
                            cmds[contadorcmd].CommandText = querySection;
                            cmds[contadorcmd].Parameters.AddWithValue("@GradeId", idgrade);
                            cmds[contadorcmd].Parameters.AddWithValue("@Score", 0.0);
                            cmds[contadorcmd].Parameters.AddWithValue("@TypeScore", 1);
                            contadorcmd++;
                        }
                        for (int l = 0; l < canttest; l++)
                        {
                            cmds[contadorcmd].CommandText = querySection;
                            cmds[contadorcmd].Parameters.AddWithValue("@GradeId", idgrade);
                            cmds[contadorcmd].Parameters.AddWithValue("@Score", 0.0);
                            cmds[contadorcmd].Parameters.AddWithValue("@TypeScore", 2);
                            contadorcmd++;
                        }

                    }
                    idgrade += idgradeincrement;
                }
                #endregion

                DBImplementation.ExecuteNBasicCommand(cmds);
                SendEmail(p.Email, userAccount.UserName, userAccount.Passstring);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not Insert Student({1}).", DateTime.Now, ex.Message));
            }
        }

        public void UpdateTransact(Person p)
        {
            string queryPerson = @"UPDATE Person SET Names = @Names, LastName = @LastName, SLastName = @SLastName,address =@address,
CI =@CI,CIExtension =@CIExtension,DateBirth =@DateBirth,photo =@photo,email =@email,latitude =@latitude,longitude =@longitude ,phone =@phone ,gender =@gender ,TownId =@TownId
WHERE PersonId =@PersonId";
            string queryStudent = @"UPDATE Student SET RudeNumber =@RudeNumber WHERE StudentId =@StudentId";
            try
            {
                
                List<SqlCommand> cmds = DBImplementation.CreateNBasicCommands(2);
                #region person insert
                cmds[0].CommandText = queryPerson;
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@PersonId ", p.PersonId));
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@TownId ", p.TownId));
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@Names ", p.Names));
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastName ", p.LastName));
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@SLastName", p.SecondLastName ?? Convert.DBNull));
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@address ", p.Address));
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@CI ", p.Ci));
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@CIExtension ", p.Ciextension ?? Convert.DBNull));
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@DateBirth ", p.BirthDate));
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@photo ", p.Photo));
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@email ", p.Email));
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@latitude ", p.Latitude));
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@longitude ", p.Longitude));
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@phone ", p.Phone));
                cmds[0].Parameters.Add(new System.Data.SqlClient.SqlParameter("@gender ", p.Gender));
                #endregion
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start student Insert.", DateTime.Now));
                #region student insert 
                cmds[1].CommandText = queryStudent;
                cmds[1].Parameters.Add(new System.Data.SqlClient.SqlParameter("@StudentId ", p.PersonId));
                cmds[1].Parameters.Add(new System.Data.SqlClient.SqlParameter("@RudeNumber ", p.Extra));
                #endregion

                DBImplementation.ExecuteNBasicCommand(cmds);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not Insert Student({1}).", DateTime.Now, ex.Message));
            }
        }

        public DataTable Select()
        {
            throw new NotImplementedException();
        }
        public DataTable Select(int idcourse)
        {


            string query = @"SELECT s.StudentId AS 'ID',CONCAT(p.Names,' ',p.LastName,' ',ISNULL(p.SLastName,'')) AS 'Name',
                            CONCAT(ISNULL(p.CIExtension,''),p.CI)  AS 'Ci', p.phone AS 'Phone', p.address AS 'DIR'
                            FROM Student s 
                            INNER JOIN Person p ON p.PersonId = s.StudentId
                            INNER JOIN UserAccount ua ON ua.UserAccountId = p.UserAccountId 
                            WHERE p.SchoolId = @SchoolId AND s.CourseId = @CourseId AND ua.status = 1";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                cmd.Parameters.AddWithValue("@CourseId", idcourse);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }
        public DataTable SelectLike(int idcourse,string like)
        {


            string query = @"SELECT ds.ID, ds.Name, ds.Ci,ds.Phone,ds.DIR 
                            FROM (SELECT s.StudentId AS 'ID',CONCAT(p.Names,' ',p.LastName,' ',ISNULL(p.SLastName,'')) AS 'Name',
                            CONCAT(ISNULL(p.CIExtension,''),p.CI)  AS 'Ci', p.phone AS 'Phone', p.address AS 'DIR'
                            FROM Student s 
                            INNER JOIN Person p ON p.PersonId = s.StudentId
                            INNER JOIN UserAccount ua ON ua.UserAccountId = p.UserAccountId 
                            WHERE p.SchoolId = @SchoolId AND s.CourseId = @CourseId AND ua.status = 1) ds 
                            WHERE ds.Name LIKE @like OR ds.Ci LIKE @like";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                cmd.Parameters.AddWithValue("@CourseId", idcourse);
                cmd.Parameters.AddWithValue("@like", "%"+like+"%");
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }
        public DataTable SelectDis(int idcourse)
        {


            string query = @"SELECT s.StudentId AS 'ID',CONCAT(p.Names,' ',p.LastName,' ',ISNULL(p.SLastName,'')) AS 'Name',
                            CONCAT(ISNULL(p.CIExtension,''),p.CI)  AS 'Ci', p.phone AS 'Phone', p.address AS 'DIR'
                            FROM Student s 
                            INNER JOIN Person p ON p.PersonId = s.StudentId
                            INNER JOIN UserAccount ua ON ua.UserAccountId = p.UserAccountId 
                            WHERE p.SchoolId = @SchoolId AND s.CourseId = @CourseId AND ua.status = 0";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                cmd.Parameters.AddWithValue("@CourseId", idcourse);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }
        public DataTable SelectDisLike(int idcourse, string like)
        {


            string query = @"SELECT ds.ID, ds.Name, ds.Ci,ds.Phone,ds.DIR 
                            FROM (SELECT s.StudentId AS 'ID',CONCAT(p.Names,' ',p.LastName,' ',ISNULL(p.SLastName,'')) AS 'Name',
                            CONCAT(ISNULL(p.CIExtension,''),p.CI)  AS 'Ci', p.phone AS 'Phone', p.address AS 'DIR'
                            FROM Student s 
                            INNER JOIN Person p ON p.PersonId = s.StudentId
                            INNER JOIN UserAccount ua ON ua.UserAccountId = p.UserAccountId 
                            WHERE p.SchoolId = @SchoolId AND s.CourseId = @CourseId AND ua.status = 0) ds 
                            WHERE ds.Name LIKE @like OR ds.Ci LIKE @like";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                cmd.Parameters.AddWithValue("@CourseId", idcourse);
                cmd.Parameters.AddWithValue("@like", "%" + like + "%");
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public int Update(Student t)
        {
            throw new NotImplementedException();
        }
        public int Updates(byte[] asd)
        {
            string query = @"UPDATE Person SET photo = @photo WHERE PersonId = 1";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);

                cmd.Parameters.AddWithValue("@photo", Encoding.Default.GetString(asd)).SqlDbType = SqlDbType.VarChar;
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

            username = "" + name.Substring(0, 1).ToLower() + last.Substring(0, 1).ToLower()+ DateTime.Now.Year.ToString() + id ;

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

        private void SendEmail(string email, string username, string password)
        {


            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(DBImplementation.usermail);
                mail.To.Add(email);
                mail.Subject = "Bienvenido a Educa";
                mail.Body = "Tu nombre de usuario registrado es: " + username + ", con contraseña: " + password
                    + "\nEn tu primer inicio de sesion se te dejara cambiar los parametros";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(DBImplementation.usermail, DBImplementation.passwordmail);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Register Mail send whitout problems" + Session.SessionCurrent.ToString()));
            }


        }

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
    }
}
