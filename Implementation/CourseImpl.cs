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
    public class CourseImpl : CourseDao
    {
        public void Delete(Course t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Course t)
        {
            throw new NotImplementedException();
        }

        public DataTable Select()
        {
            string query = @"SELECT a.IdCourse,a.Course,a.Sections
FROM (SELECT c.CourseId AS 'IdCourse',c.sectionCourse AS 'Sections' , CONCAT(c.numberCourse,c.letterCourse) AS 'Course', c.letterCourse from Course c ) a
WHERE a.letterCourse <= (SELECT CASE WHEN st.numberCourses = 1 THEN 'A' WHEN st.numberCourses = 2 THEN 'B' WHEN st.numberCourses = 3 THEN 'C'
WHEN st.numberCourses = 4 THEN 'D' WHEN st.numberCourses = 5 THEN 'E' Else 'E' END AS Course
FROM School s 
INNER JOIN SchoolType st ON st.SchoolTypeId = s.SchoolTypeId 
WHERE s.SchoolId = @SchoolId) 
order by a.Sections,a.Course";
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
            string query = @"SELECT a.IdCourse,a.Course,a.Sections
FROM (SELECT c.CourseId AS 'IdCourse',c.sectionCourse AS 'Sections' , CONCAT(c.numberCourse,c.letterCourse) AS 'Course', c.letterCourse from Course c ) a
WHERE a.letterCourse <= (SELECT CASE WHEN st.numberCourses = 1 THEN 'A' WHEN st.numberCourses = 2 THEN 'B' WHEN st.numberCourses = 3 THEN 'C'
WHEN st.numberCourses = 4 THEN 'D' WHEN st.numberCourses = 5 THEN 'E' Else 'E' END AS Course
FROM School s 
INNER JOIN SchoolType st ON st.SchoolTypeId = s.SchoolTypeId 
WHERE s.SchoolId = @SchoolId)  AND CONCAT(a.Course,' ',a.Sections) LIKE @like
order by a.Sections,a.Course";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                cmd.Parameters.AddWithValue("@like", "%" + like + "%");
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public int Update(Course t)
        {
            throw new NotImplementedException();
        }
    }
}
