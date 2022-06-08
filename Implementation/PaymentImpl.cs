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
    public class PaymentImpl : PaymentDao
    {
        public void Delete(Payment t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Payment t)
        {
            throw new NotImplementedException();
        }

        public DataTable Select()
        {
            throw new NotImplementedException();
        }

        public int Update(Payment t)
        {
            throw new NotImplementedException();
        }
        public int InsertTransact(List<Fee> listfee, double total,int idpayer)
        {
            string queryFees = @"UPDATE Fee SET Balance = (Balance + @Balance) where FeeId =@FeeId";
            string queryInvoice = @"INSERT INTO Invoice (Amount,NroInvoice,ControlCode,DosageId,IdPayer)
                                VALUES(@Amount,@NroInvoice,@ControlCode,@DosageId,@IdPayer)";
            try
            {
                #region ATRIBUTOS
                int idInvoice = DBImplementation.GetIdentityFromTable("Invoice");
                int idPayment = DBImplementation.GetIdentityFromTable("Payment");
                Dosage dosage = new Dosage();
                DosageImpl dosageImpl = new DosageImpl();
                dosage = dosageImpl.GET();
                #endregion
                List<SqlCommand> cmds = DBImplementation.CreateNBasicCommands(2+(listfee.Count*2));
                int count = 0;
                #region feeupdates
                foreach (Fee fee in listfee)
                {
                    cmds[count].CommandText = queryFees;
                    cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@FeeId ", fee.FeeId));
                    cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@Balance ", fee.Balance));
                    count++;
                }
                #endregion
                #region invoice
                cmds[count].CommandText = queryInvoice;
                cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@Amount ", fee.FeeId));
                cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@NroInvoice ", fee.FeeId));
                cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@ControlCode ", fee.FeeId));
                cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@DosageId ", dosage.DosageKey));
                cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@IdPayer ", idpayer));

                count++;
                #endregion

                DBImplementation.ExecuteNBasicCommand(cmds);
                return idInvoice;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not Insert Student({1}).", DateTime.Now, ex.Message));
                return 0;
            }
        }
    }
}
