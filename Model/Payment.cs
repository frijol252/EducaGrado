using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Payment
    {
        int paymentId;
        double amount;
        int invoiceId;
        DateTime updateDate;
        DateTime registrationDate;
        byte status;

        public int PaymentId { get => paymentId; set => paymentId = value; }
        public double Amount { get => amount; set => amount = value; }
        public int InvoiceId { get => invoiceId; set => invoiceId = value; }
        public DateTime UpdateDate { get => updateDate; set => updateDate = value; }
        public DateTime RegistrationDate { get => registrationDate; set => registrationDate = value; }
        public byte Status { get => status; set => status = value; }

        public Payment(int paymentId, double amount, int invoiceId, DateTime updateDate, DateTime registrationDate, byte status)
        {
            this.paymentId = paymentId;
            this.amount = amount;
            this.invoiceId = invoiceId;
            this.updateDate = updateDate;
            this.registrationDate = registrationDate;
            this.status = status;
        }
        public Payment()
        {
        }
    }
}
