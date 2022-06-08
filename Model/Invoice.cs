using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Invoice
    {
        int invoideId;
        double amount;
        int nroInvoice;
        string controlCode;
        int idDosage;
        string status;
        DateTime updateDate;
        DateTime registrationDate;
        int idPayer;
        public Invoice()
        {

        }

        public int InvoideId { get => invoideId; set => invoideId = value; }
        public double Amount { get => amount; set => amount = value; }
        public int NroInvoice { get => nroInvoice; set => nroInvoice = value; }
        public string ControlCode { get => controlCode; set => controlCode = value; }
        public int IdDosage { get => idDosage; set => idDosage = value; }
        public string Status { get => status; set => status = value; }
        public DateTime UpdateDate { get => updateDate; set => updateDate = value; }
        public DateTime RegistrationDate { get => registrationDate; set => registrationDate = value; }
        public int IdPayer { get => idPayer; set => idPayer = value; }
        
        public Invoice(int invoideId, double amount, int nroInvoice, string controlCode, int idDosage, string status, DateTime updateDate, DateTime registrationDate, int idPayer)
        {
            this.InvoideId = invoideId;
            this.Amount = amount;
            this.NroInvoice = nroInvoice;
            this.ControlCode = controlCode;
            this.IdDosage = idDosage;
            this.Status = status;
            this.UpdateDate = updateDate;
            this.RegistrationDate = registrationDate;
            this.IdPayer = idPayer;
        }
        
    }
}
