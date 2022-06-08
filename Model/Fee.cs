using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Fee
    {
        int feeId;
        int studentId;
        double amount;
        double balance;
        DateTime deadLine;
        DateTime updateDate;
        DateTime registrationDate;
        byte status;

        public Fee(int feeId, int studentId, double amount, double balance, DateTime deadLine, DateTime updateDate, DateTime registrationDate, byte status)
        {
            this.feeId = feeId;
            this.studentId = studentId;
            this.amount = amount;
            this.balance = balance;
            this.deadLine = deadLine;
            this.updateDate = updateDate;
            this.registrationDate = registrationDate;
            this.status = status;
        }
        public Fee(int feeId,  double balance, byte status)
        {
            this.feeId = feeId;
            this.balance = balance;
            this.status = status;
        }
        public Fee()
        {
        }
        public int FeeId { get => feeId; set => feeId = value; }
        public int StudentId { get => studentId; set => studentId = value; }
        public double Amount { get => amount; set => amount = value; }
        public double Balance { get => balance; set => balance = value; }
        public DateTime DeadLine { get => deadLine; set => deadLine = value; }
        public DateTime UpdateDate { get => updateDate; set => updateDate = value; }
        public DateTime RegistrationDate { get => registrationDate; set => registrationDate = value; }
        public byte Status { get => status; set => status = value; }
    }
}
