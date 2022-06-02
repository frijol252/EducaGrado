using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Implementation
{
    public class ClassImpl : ClassDao
    {
        public void Delete(Class t)
        {
            string queryMatter = @"UPDATE Class SET status = 0 WHERE ClassId = @ClassId"; ;
            try
            {
                List<SqlCommand> cmds = DBImplementation.CreateNBasicCommands(1);
                cmds[0].CommandText = queryMatter;
                cmds[0].Parameters.AddWithValue("@ClassId ", t.ClassId);

                DBImplementation.ExecuteNBasicCommand(cmds);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not Dele Class({1}).", DateTime.Now, ex.Message));
            }
        }

        public int Insert(Class t)
        {
            throw new NotImplementedException();
        }

        public void Inserttransact(List<Class> t)
        {
            string queryClass = @"INSERT INTO Class (CourseId,idMatter)
                                VALUES (@CourseId,@idMatter)";
            string querySchedule = @"INSERT INTO ScheduleClass (ClassId,ScheduleId,dayClass)
                                     VALUES (@ClassId,@ScheduleId,@dayClass)";
            string queryGrade = @"INSERT INTO Grade (StudentId,ClassId,TypeGrade)
                                VALUES(@StudentId,@ClassId,@TypeGrade)";
            string querySection = @"INSERT INTO SectionGrade (GradeId,Score,TypeScore)
                                    VALUES(@GradeId,@Score,@TypeScore)";
            try
            {

                #region armado
                int idclass = DBImplementation.GetIdentityFromTable("Class");
                int idgrade = DBImplementation.GetIdentityFromTable("Grade");
                int idgradeincrement = DBImplementation.GetIncementFromTable("Grade");
                int cantgrades = 0, canttest = 0, totalgrades, typeq = 1, totalextras;
                #region contarStudents
                int students = 0;
                DataTable dt = new DataTable();
                dt = SelectStudents(t[0].CourseId);
                foreach (DataRow d in dt.Rows) students++;
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
                totalextras = totalgrades * students;
                #endregion

                #region classinsert
                List<SqlCommand> cmds = DBImplementation.CreateNBasicCommands(1 + t.Count + totalextras);
                cmds[0].CommandText = queryClass;
                cmds[0].Parameters.AddWithValue("@CourseId ", t[0].CourseId);
                cmds[0].Parameters.AddWithValue("@idMatter ", t[0].MatterId);
                int contadorcmd = 1;

                #endregion
                #region schedule
                for (int i = 0; i < t.Count; i++)
                {
                    cmds[contadorcmd].CommandText = querySchedule;
                    cmds[contadorcmd].Parameters.AddWithValue("@ClassId ", idclass);
                    cmds[contadorcmd].Parameters.AddWithValue("@ScheduleId ", t[i].ScheduleId);
                    cmds[contadorcmd].Parameters.AddWithValue("@dayClass ", t[i].Day);
                    contadorcmd++;
                }
                #endregion
                #region GRADES

                for (int j = 1; j <= typeq; j++)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        cmds[contadorcmd].CommandText = queryGrade;
                        cmds[contadorcmd].Parameters.AddWithValue("@StudentId", int.Parse(dataRow[0].ToString()));
                        cmds[contadorcmd].Parameters.AddWithValue("@ClassId", idclass);
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

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not Dele Class({1}).", DateTime.Now, ex.Message));
            }
        }

        public DataTable Select(int courseId)
        {
            string query = @"SELECT DISTINCT  c.ClassId AS 'ID' ,m.matterName AS 'Name', cm.CategoryName AS 'Category' from Class c 
INNER JOIN Matter m ON m.matterid = c.idMatter
INNER JOIN CategoryMatter cm ON cm.CategoryMatterId =m.CategoryMatterId 
WHERE c.CourseId = @CourseId AND c.status = 1";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public DataTable Select()
        {
            throw new NotImplementedException();
        }



        public DataTable SelectStudents(int id)
        {

            string query = @"SELECT studentId FROM Student s
                        INNER JOIN Person p ON p.PersonId =s.StudentId 
                        WHERE s.CourseId =@Course AND p.SchoolId = @SchoolId";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@Course", id);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }

        }

        public int Update(Class t)
        {
            throw new NotImplementedException();
        }

        public void Updatetransact(List<Class> t)
        {
            string query1 = @"DELETE ScheduleClass WHERE ClassId = @ClassId";
            string query2 = @"INSERT INTO ScheduleClass (ClassId,ScheduleId,dayClass)
                                     VALUES (@ClassId,@ScheduleId,@dayClass)";
            try
            {
                int contadorcmd = 0;
                List<SqlCommand> cmds = DBImplementation.CreateNBasicCommands(1 + t.Count);
                cmds[contadorcmd].CommandText = query1;
                cmds[contadorcmd].Parameters.AddWithValue("@ClassId ", t[0].MatterId);
                contadorcmd++;
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Parte1.", DateTime.Now));
                for (int i = 0; i < t.Count; i++)
                {
                    cmds[contadorcmd].CommandText = query2;
                    cmds[contadorcmd].Parameters.AddWithValue("@ClassId ", t[i].MatterId);
                    cmds[contadorcmd].Parameters.AddWithValue("@ScheduleId ", t[i].ScheduleId);
                    cmds[contadorcmd].Parameters.AddWithValue("@dayClass ", t[i].Day);
                    contadorcmd++;
                }
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Parte2", DateTime.Now));
                DBImplementation.ExecuteNBasicCommand(cmds);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not Dele Class({1}).", DateTime.Now, ex.Message));
            }
        }
    }
}
