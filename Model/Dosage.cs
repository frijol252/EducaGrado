using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Dosage
    {
        int dosageId;
        string nroAutorization;
        DateTime deadLine;
        string dosageKey;
        int initialNumber;
        int finalNumbre;
        DateTime registrationDate;
        DateTime updateDate;
        byte status;
        int modalityId;

        public int DosageId { get => dosageId; set => dosageId = value; }
        public string NroAutorization { get => nroAutorization; set => nroAutorization = value; }
        public DateTime DeadLine { get => deadLine; set => deadLine = value; }
        public string DosageKey { get => dosageKey; set => dosageKey = value; }
        public int InitialNumber { get => initialNumber; set => initialNumber = value; }
        public int FinalNumber { get => finalNumbre; set => finalNumbre = value; }
        public DateTime RegistrationDate { get => registrationDate; set => registrationDate = value; }
        public DateTime UpdateDate { get => updateDate; set => updateDate = value; }
        public byte Status { get => status; set => status = value; }
        public int ModalityId { get => modalityId; set => modalityId = value; }


        public Dosage(int dosageId, string nroAutorization, DateTime deadLine, string dosageKey, int initialNumber, int finalNumbre, DateTime registrationDate, DateTime updateDate, byte status, int modalityId)
        {
            this.dosageId = dosageId;
            this.nroAutorization = nroAutorization;
            this.deadLine = deadLine;
            this.dosageKey = dosageKey;
            this.initialNumber = initialNumber;
            this.finalNumbre = finalNumbre;
            this.registrationDate = registrationDate;
            this.updateDate = updateDate;
            this.status = status;
            this.modalityId = modalityId;
        }

        public Dosage()
        {
            
        }

    }
}
