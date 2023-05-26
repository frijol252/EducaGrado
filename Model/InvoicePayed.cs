using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class InvoicePayed
    {
        string FullName;
        string BusinessName;
        string NIT;
        string DateLiteral;
        string Code;
        string NroInvoice;
        string NroAuthorizacion;
        string ControlCode;
        string DeadLine;
        string LiteralAmount;
        string AmountTotal;
        string Detail;
        string Amount;
        string QR;
        string Payer;

        public string FullName1 { get => FullName; set => FullName = value; }
        public string BusinessName1 { get => BusinessName; set => BusinessName = value; }
        public string NIT1 { get => NIT; set => NIT = value; }
        public string DateLiteral1 { get => DateLiteral; set => DateLiteral = value; }
        public string Code1 { get => Code; set => Code = value; }
        public string NroInvoice1 { get => NroInvoice; set => NroInvoice = value; }
        public string NroAuthorizacion1 { get => NroAuthorizacion; set => NroAuthorizacion = value; }
        public string ControlCode1 { get => ControlCode; set => ControlCode = value; }
        public string DeadLine1 { get => DeadLine; set => DeadLine = value; }
        public string LiteralAmount1 { get => LiteralAmount; set => LiteralAmount = value; }
        public string AmountTotal1 { get => AmountTotal; set => AmountTotal = value; }
        public string Detail1 { get => Detail; set => Detail = value; }
        public string Amount1 { get => Amount; set => Amount = value; }
        public string QR1 { get => QR; set => QR = value; }
        public string Payer1 { get => Payer; set => Payer = value; }

        public InvoicePayed(string fullName, string businessName, string nIT, string dateLiteral, string code, string nroInvoice, string nroAuthorizacion, string controlCode, string deadLine, string literalAmount, string amountTotal, string detail, string amount, string qR, string payer)
        {
            FullName = fullName;
            BusinessName = businessName;
            NIT = nIT;
            DateLiteral = dateLiteral;
            Code = code;
            NroInvoice = nroInvoice;
            NroAuthorizacion = nroAuthorizacion;
            ControlCode = controlCode;
            DeadLine = deadLine;
            LiteralAmount = literalAmount;
            AmountTotal = amountTotal;
            Detail = detail;
            Amount = amount;
            QR = qR;
            Payer = payer;
        }
    }
}
