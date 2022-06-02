using DAO;
using Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Implementation
{
    public class ModalityImpl : ModalityDao
    {
        public void Delete(Modality t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Modality t)
        {
            throw new NotImplementedException();
        }

        public DataTable Select()
        {
            string query = @"SELECT m.NumberGrades ,m.NumberTest, CASE m.TypeQualify 
                            When 'Bimestral' Then 4
                            When 'Trimestral' Then 3
                            ELSE 2
                            END AS 'TYPEWORK', m.TypeQualify  FROM School s 
                            INNER JOIN Modality m ON m.ModalityId = s.ModalityId 
                            WHERE s.SchoolId = @SchoolId";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public int Update(Modality t)
        {
            throw new NotImplementedException();
        }
    }
}
