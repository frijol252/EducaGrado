using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Payer
    {
        int idPayer;
        string nit;
        string businessName;

        public int IdPayer { get => idPayer; set => idPayer = value; }
        public string Nit { get => nit; set => nit = value; }
        public string BusinessName { get => businessName; set => businessName = value; }

        public Payer(int idPayer, string nit, string businessName)
        {
            this.idPayer = idPayer;
            this.nit = nit;
            this.businessName = businessName;
        }
        public Payer()
        {
        }
    }
}
