using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class DosageImpl : DosageDao
    {
        public void Delete(Dosage t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Dosage t)
        {
            throw new NotImplementedException();
        }

        public DataTable Select()
        {
            throw new NotImplementedException();
        }

        public int Update(Dosage t)
        {
            throw new NotImplementedException();
        }

        public Dosage GET()
        {
            string query = @"SELECT DosageId,NroAutorization, deadline ,DosageKey from InvoiceDosage 
WHERE ModalityId = (SELECT s.ModalityId  FROM School s WHERE s.SchoolId=@SchoolId)
AND status = 1";

            try
            {
                Dosage dosage = new Dosage();
                SqlCommand cmd = null;
                cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                SqlDataReader dr = DBImplementation.ExecuteDataReaderCommand(cmd);
                while (dr.Read())
                {
                    dosage.DosageId = dr.GetInt32(0);
                    dosage.NroAutorization = dr.GetString(1); dosage.DeadLine = dr.GetDateTime(2); dosage.DosageKey= dr.GetString(3);

                }
                dr.Close();
                cmd.Connection.Close();
                return dosage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertTransaction(Dosage d)
        {

            string queryUpdate = @"UPDATE InvoiceDosage SET status = 0 WHERE ModalityId 
                            = (SELECT s.ModalityId FROM School s WHERE s.SchoolId=1) ";

            string queryInsert = @"INSERT INTO InvoiceDosage (NroAutorization,deadline,DosageKey,ModalityId)
                                VALUES(@NroAutorization,@deadline,@DosageKey,(SELECT s.ModalityId  FROM School s WHERE s.SchoolId=@SchoolId))";
            try
            {
                List<SqlCommand> cmds = DBImplementation.CreateNBasicCommands(2);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start Insert New Dosage.", DateTime.Now));
                cmds[0].CommandText = queryUpdate;

                cmds[1].CommandText = queryInsert;
                cmds[1].Parameters.AddWithValue("@NroAutorization", d.NroAutorization);
                cmds[1].Parameters.AddWithValue("@deadline", d.DeadLine);
                cmds[1].Parameters.AddWithValue("@DosageKey", d.DosageKey);
                cmds[1].Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start Insert New Dosage.", DateTime.Now));
                DBImplementation.ExecuteNBasicCommand(cmds);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Info: Start Insert New Dosage.", DateTime.Now));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not Insert Student({1}).", DateTime.Now, ex.Message));
            }
        }
    }
}
