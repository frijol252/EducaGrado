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
        
        public int InsertTransact(List<Fee> listfee,Invoice invoice)
        {
            string queryFees = @"UPDATE Fee SET Balance = (Balance + @Balance), status=@status where FeeId =@FeeId";
            string queryInvoice = @"INSERT INTO Invoice (Amount,NroInvoice,ControlCode,DosageId,IdPayer,literal)
                                VALUES(@Amount,@NroInvoice,@ControlCode,@DosageId,@IdPayer,@literal)";
            string queryPayment = @"INSERT INTO Payment (Amount, InvoiceId)
                                VALUES(@Amount,@InvoiceId)";
            string queryPaymentFee = @"INSERT INTO FeePayment (FeeId,PaymentId,Amount)
                                VALUES(@FeeId,@PaymentId,@Amount)";
            string queryDosage = @"UPDATE InvoiceDosage set FinalNumber =FinalNumber +1";
            try
            {
                #region ATRIBUTOS
                int idInvoice = DBImplementation.GetIdentityFromTable("Invoice");
                int idPayment = DBImplementation.GetIdentityFromTable("Payment");

                #endregion
                List<SqlCommand> cmds = DBImplementation.CreateNBasicCommands(3+(listfee.Count*2));
                int count = 0;
                
                
                #region invoice
                cmds[count].CommandText = queryInvoice;
                cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@Amount ", invoice.Amount));
                cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@NroInvoice ", invoice.NroInvoice));
                cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@ControlCode ", invoice.ControlCode));
                cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@DosageId ", invoice.IdDosage));
                cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@IdPayer", invoice.IdPayer));
                cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@literal", invoice.Literal));

                count++;
                #endregion
                #region Payer
                cmds[count].CommandText = queryPayment;
                cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@Amount ", invoice.Amount));
                cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@InvoiceId ", idInvoice));
            
                count++;
                #endregion

                #region Fee
                foreach (Fee fee in listfee)
                {
                    
                    cmds[count].CommandText = queryFees;
                    cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@FeeId ", fee.FeeId));
                    cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@Balance ", fee.Balance));
                    cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@status ", fee.Status));
                    count++; 
                    cmds[count].CommandText = queryPaymentFee;
                    cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@FeeId", fee.FeeId));
                    cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@PaymentId", idPayment));
                    cmds[count].Parameters.Add(new System.Data.SqlClient.SqlParameter("@Amount", fee.Balance));
                    count++;
                }
                #endregion
                
                #region Dosage
                cmds[count].CommandText = queryDosage;
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
