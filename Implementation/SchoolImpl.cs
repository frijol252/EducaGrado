﻿using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Implementation
{
    public class SchoolImpl : SchoolDao
    {
        public void Delete(School t)
        {
            throw new NotImplementedException();
        }

        public int Insert(School t)
        {
            throw new NotImplementedException();
        }

        public DataTable Select()
        {
            throw new NotImplementedException();
        }

        public int Update(School t)
        {
            throw new NotImplementedException();
        }



        public void UpdateTypeWork(SchoolType schoolType, Modality modality, int schoolid)
        {
            string queryModality = @"UPDATE  Modality  SET NumberGrades = @NumberGrades, NumberTest = @NumberTest,TypeQualify =@TypeQualify ,PorcentGrades = @PorcentGrades, PorcentTest=@PorcentTest
WHERE ModalityId = (SELECT s.ModalityId  FROM School s WHERE SchoolId = @SchoolId)";
            string querySchoolType = @"UPDATE  SchoolType  SET numberCourses  = @numberCourses  WHERE SchoolTypeId = (SELECT s.SchoolTypeId  FROM School s WHERE SchoolId = @SchoolId)";
            try
            {
                List<SqlCommand> cmds = DBImplementation.CreateNBasicCommands(2);
                cmds[0].CommandText = queryModality;
                cmds[0].Parameters.AddWithValue("@NumberGrades", modality.NumberGrades);
                cmds[0].Parameters.AddWithValue("@NumberTest", modality.NumberTest);
                cmds[0].Parameters.AddWithValue("@TypeQualify", modality.TypeQualify);
                cmds[0].Parameters.AddWithValue("@PorcentGrades", modality.PercentGrades);
                cmds[0].Parameters.AddWithValue("@PorcentTest", modality.PercentTest);
                cmds[0].Parameters.AddWithValue("@SchoolId", schoolid);
                cmds[1].CommandText = querySchoolType;
                cmds[1].Parameters.AddWithValue("@numberCourses", schoolType.Cursos);
                cmds[1].Parameters.AddWithValue("@SchoolId", schoolid);
                DBImplementation.ExecuteNBasicCommand(cmds);

            }
            catch (Exception ex)
            {

            }
        }
    }
}
