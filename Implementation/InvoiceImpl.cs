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
    public class InvoiceImpl : InvoiceDao
    {
        public void Delete(Invoice t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Invoice t)
        {
            throw new NotImplementedException();
        }

        public DataTable Select(int idInvoice)
        {
            string query = @"SELECT CONCAT(p.Names,' ',p.LastName,' ',ISNULL(p.SLastName,'')) as 'FullName',
s2.NIT AS 'BusinessName', p3.NIT AS 'NIT', CAST(i2.RegistrationDate  AS varchar(11))
  AS 'DateLiteral', ua.UserName AS 'Code', i2.NroInvoice AS 'NroInvoice',
id.NroAutorization  AS 'NroAutorizacion', i2.ControlCode AS 'ControlCode', id.deadline AS 'DeadLine', 
i2.literal AS 'LiteralAmount', i2.Amount 
AS 'AmountTotal', 'Pago Colegiatura' AS 'Detail', f.Amount-f.Balance  AS 'Amount',

CONCAT(CAST(i2.RegistrationDate  AS varchar(11)),'/', s2.NIT,'/',s2.Name,'/',i2.NroInvoice,'/',i2.Amount) AS 'QR',
p3.BusinessName AS 'Payer'
FROM Fee f
INNER JOIN Student s ON s.StudentId =f.StudentId 
INNER JOIN Person p ON p.PersonId  = s.StudentId 
INNER JOIN UserAccount ua ON ua.UserAccountId =p.UserAccountId 
INNER JOIN FeePayment fp ON fp.FeeId =f.FeeId 
INNER JOIN Payment p2 ON p2.PaymentId =fp.PaymentId 
INNER JOIN Invoice i2 ON i2.InvoiceId =p2.InvoiceId 
INNER JOIN InvoiceDosage id ON id.DosageId = i2.DosageId 
INNER JOIN Payer p3 ON p3.IdPayer =i2.IdPayer 
INNER JOIN School s2 ON s2.SchoolId =p.SchoolId 
WHERE i2.InvoiceId = @InvoiceId";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@InvoiceId", idInvoice);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public DataTable Select()
        {
            throw new NotImplementedException();
        }

        public int Update(Invoice t)
        {
            throw new NotImplementedException();
        }
    }
}
