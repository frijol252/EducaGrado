using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Administrator
    {
        int administratorId;
        string position;
        string profesion;
        string speciality;

        public int AdministratorId { get => administratorId; set => administratorId = value; }
        public string Position { get => position; set => position = value; }
        public string Profesion { get => profesion; set => profesion = value; }
        public string Speciality { get => speciality; set => speciality = value; }

        public Administrator(int administratorId, string position, string profesion, string speciality)
        {
            this.administratorId = administratorId;
            this.position = position;
            this.profesion = profesion;
            this.speciality = speciality;
        }
    }
}
