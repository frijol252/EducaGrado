using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Implementation
{
    public class ScheduleImpl : ScheduleDao
    {
        public void Delete(Schedule t)
        {
            string queryClass = @"UPDATE Class SET ScheduleId = NULL, dayClass = NULL  WHERE ScheduleId = @ScheduleId";
            string querySchedule = @"DELETE Schedule WHERE ScheduleId  = @ScheduleId ";
            try
            {
                List<SqlCommand> cmds = DBImplementation.CreateNBasicCommands(2);
                cmds[0].CommandText = queryClass;
                cmds[0].Parameters.AddWithValue("@ScheduleId ", t.ScheduleId);
                cmds[1].CommandText = querySchedule;
                cmds[1].Parameters.AddWithValue("@ScheduleId ", t.ScheduleId);

                DBImplementation.ExecuteNBasicCommand(cmds);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not Dele Class({1}).", DateTime.Now, ex.Message));
            }
        }

        public int Insert(Schedule t)
        {
            string query = @"INSERT INTO Schedule (hourStart,hourFinish,ModalityId) VALUES
	 (@hourStart,@hourFinish,(SELECT ModalityId FROM School WHERE SchoolId=@SchoolId))";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);

                cmd.Parameters.AddWithValue("@hourStart", t.StartHour);
                cmd.Parameters.AddWithValue("@hourFinish", t.FinishHour);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);

                return DBImplementation.ExecuteBasicCommand(cmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable Select()
        {
            string query = @"SELECT ScheduleId AS 'ID',hourStart AS 'HS',hourFinish AS 'HF'
FROM Schedule s WHERE s.ModalityId = (SELECT m.ModalityId FROM Modality m INNER JOIN School sc ON sc.ModalityId = m.ModalityId
WHERE sc.SchoolId=@SchoolId) ORDER BY hourStart ASC ";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public DataTable SelectHourClass(int course)
        {
            string query = @"SELECT s.ScheduleId AS 'ID',CONCAT((CONVERT(VARCHAR(5), hourStart , 108)),'-',(CONVERT(VARCHAR(5), hourFinish , 108))) AS 'HR',
ISNULL((SELECT m.matterName  FROM ScheduleClass sc2 INNER JOIN Class c ON c.ClassId =sc2.ClassId INNER JOIN Matter m ON m.matterid = c.idMatter 
WHERE sc2.ScheduleId=s.ScheduleId AND sc2.dayClass='Lu' AND c.CourseId=1 AND c.status=@SchoolId),'O') AS 'Lunes',
ISNULL((SELECT m.matterName  FROM ScheduleClass sc2 INNER JOIN Class c ON c.ClassId =sc2.ClassId INNER JOIN Matter m ON m.matterid = c.idMatter 
WHERE sc2.ScheduleId=s.ScheduleId AND sc2.dayClass='Ma' AND c.CourseId=1 AND c.status=@SchoolId),'O') AS 'Martes',
ISNULL((SELECT m.matterName  FROM ScheduleClass sc2 INNER JOIN Class c ON c.ClassId =sc2.ClassId INNER JOIN Matter m ON m.matterid = c.idMatter 
WHERE sc2.ScheduleId=s.ScheduleId AND sc2.dayClass='Mi' AND c.CourseId=1 AND c.status=@SchoolId),'O') AS 'Miercoles',
ISNULL((SELECT m.matterName  FROM ScheduleClass sc2 INNER JOIN Class c ON c.ClassId =sc2.ClassId INNER JOIN Matter m ON m.matterid = c.idMatter 
WHERE sc2.ScheduleId=s.ScheduleId AND sc2.dayClass='Ju' AND c.CourseId=1 AND c.status=@SchoolId),'O') AS 'Jueves',
ISNULL((SELECT m.matterName  FROM ScheduleClass sc2 INNER JOIN Class c ON c.ClassId =sc2.ClassId INNER JOIN Matter m ON m.matterid = c.idMatter 
WHERE sc2.ScheduleId=s.ScheduleId AND sc2.dayClass='Vi' AND c.CourseId=1 AND c.status=@SchoolId),'O') AS 'Viernes',
ISNULL((SELECT m.matterName  FROM ScheduleClass sc2 INNER JOIN Class c ON c.ClassId =sc2.ClassId INNER JOIN Matter m ON m.matterid = c.idMatter 
WHERE sc2.ScheduleId=s.ScheduleId AND sc2.dayClass='Sa' AND c.CourseId=1 AND c.status=@SchoolId),'O') AS 'Sabado'
FROM Schedule s 
WHERE s.ModalityId = (SELECT m2.ModalityId FROM Modality m2 INNER JOIN School sc ON sc.ModalityId = m2.ModalityId
WHERE sc.SchoolId=@CourseId) ORDER BY s.hourStart ASC ";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                cmd.Parameters.AddWithValue("@CourseId", course);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public DataTable SelectHourClass(int course, int idclass)
        {
            string query = @"SELECT s.ScheduleId AS 'ID',CONCAT((CONVERT(VARCHAR(5), hourStart , 108)),'-',(CONVERT(VARCHAR(5), hourFinish , 108))) AS 'HR',
ISNULL((SELECT CASE c.ClassId 
 WHEN @ClassId THEN 'Seleccionado'
  ELSE  m.matterName
 END FROM ScheduleClass sc2 INNER JOIN Class c ON c.ClassId =sc2.ClassId INNER JOIN Matter m ON m.matterid = c.idMatter 
WHERE sc2.ScheduleId=s.ScheduleId AND sc2.dayClass='Lu' AND c.CourseId=@CourseId AND c.status=1),'O') AS 'Lunes',
ISNULL((SELECT CASE c.ClassId 
 WHEN @ClassId THEN 'Seleccionado'
  ELSE  m.matterName
 END  FROM ScheduleClass sc2 INNER JOIN Class c ON c.ClassId =sc2.ClassId INNER JOIN Matter m ON m.matterid = c.idMatter 
WHERE sc2.ScheduleId=s.ScheduleId AND sc2.dayClass='Ma' AND c.CourseId=@CourseId AND c.status=1),'O') AS 'Martes',
ISNULL((SELECT CASE c.ClassId 
 WHEN @ClassId THEN 'Seleccionado'
  ELSE  m.matterName
 END  FROM ScheduleClass sc2 INNER JOIN Class c ON c.ClassId =sc2.ClassId INNER JOIN Matter m ON m.matterid = c.idMatter 
WHERE sc2.ScheduleId=s.ScheduleId AND sc2.dayClass='Mi' AND c.CourseId=@CourseId AND c.status=1),'O') AS 'Miercoles',
ISNULL((SELECT CASE c.ClassId 
 WHEN @ClassId THEN 'Seleccionado'
  ELSE  m.matterName
 END  FROM ScheduleClass sc2 INNER JOIN Class c ON c.ClassId =sc2.ClassId INNER JOIN Matter m ON m.matterid = c.idMatter 
WHERE sc2.ScheduleId=s.ScheduleId AND sc2.dayClass='Ju' AND c.CourseId=@CourseId AND c.status=1),'O') AS 'Jueves',
ISNULL((SELECT CASE c.ClassId 
 WHEN @ClassId THEN 'Seleccionado'
  ELSE  m.matterName
 END  FROM ScheduleClass sc2 INNER JOIN Class c ON c.ClassId =sc2.ClassId INNER JOIN Matter m ON m.matterid = c.idMatter 
WHERE sc2.ScheduleId=s.ScheduleId AND sc2.dayClass='Vi' AND c.CourseId=@CourseId AND c.status=1),'O') AS 'Viernes',
ISNULL((SELECT CASE c.ClassId 
 WHEN @ClassId THEN 'Seleccionado'
  ELSE  m.matterName 
 END  FROM ScheduleClass sc2 INNER JOIN Class c ON c.ClassId =sc2.ClassId INNER JOIN Matter m ON m.matterid = c.idMatter 
WHERE sc2.ScheduleId=s.ScheduleId AND sc2.dayClass='Sa' AND c.CourseId=@CourseId AND c.status=1),'O') AS 'Sabado'
FROM Schedule s 
WHERE s.ModalityId = (SELECT m2.ModalityId FROM Modality m2 INNER JOIN School sc ON sc.ModalityId = m2.ModalityId
WHERE sc.SchoolId=1)  ORDER BY hourStart ASC ;";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                cmd.Parameters.AddWithValue("@CourseId", course);
                cmd.Parameters.AddWithValue("@ClassId", idclass);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public int Update(Schedule t)
        {
            throw new NotImplementedException();
        }
    }
}
