using DAO;
using Model;
using System;
using System.Collections.Generic;
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
        public DataTable SelectTeacher(int clase,int type)
        {
            string query = @"EXEC SelectStudentTeacher @UserAccountId , @TypeGrade , @ClassId";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@UserAccountId", Session.SessionID);
                cmd.Parameters.AddWithValue("@TypeGrade", type);
                cmd.Parameters.AddWithValue("@ClassId", clase);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public int Update(Grade t)
        {
            throw new NotImplementedException();
        }

        public void UpdateTransact(List<int> listaids, List<double> listgrades)
        {
            string queryGrade = @"UPDATE SectionGrade SET Score = @Score WHERE SectionGradeId =@SectionGradeId";
            try
            {
                List<SqlCommand> cmds = DBImplementation.CreateNBasicCommands(listaids.Count);
                for (int i=0;i<listaids.Count;i++)
                {
                    cmds[i].CommandText = queryGrade;
                    cmds[i].Parameters.AddWithValue("@Score ",listgrades[i]);
                    cmds[i].Parameters.AddWithValue("@SectionGradeId ", listaids[i]);
                }

                DBImplementation.ExecuteNBasicCommand(cmds);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not Dele Class({1}).", DateTime.Now, ex.Message));
            }
        }
    }
}
