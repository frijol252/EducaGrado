using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class School
    {
        #region Properiarities
        int schoolId;
        int schooltypeId;
        int modalityId;
        string name;
        string address;
        double latitude;
        double longitude;
        byte status;
        DateTime registrationDate;
        DateTime updateDate;






        #endregion
        #region Getters/Setters

        public int SchoolId { get => schoolId; set => schoolId = value; }
        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        public double Latitude { get => latitude; set => latitude = value; }
        public double Longitude { get => longitude; set => longitude = value; }
        public byte Status { get => status; set => status = value; }
        public DateTime RegistrationDate { get => registrationDate; set => registrationDate = value; }
        public DateTime UpdateDate { get => updateDate; set => updateDate = value; }
        public int SchooltypeId { get => schooltypeId; set => schooltypeId = value; }
        public int ModalityId { get => modalityId; set => modalityId = value; }


        #endregion
        #region Constructor
        public School(int schoolId, int schooltypeId, int modalityId, string name, string address, double latitude, double longitude, byte status, DateTime registrationDate, DateTime updateDate)
        {
            this.schoolId = schoolId;
            this.schooltypeId = schooltypeId;
            this.modalityId = modalityId;
            this.name = name;
            this.address = address;
            this.latitude = latitude;
            this.longitude = longitude;
            this.status = status;
            this.registrationDate = registrationDate;
            this.updateDate = updateDate;
        }
        #endregion
    }
}
