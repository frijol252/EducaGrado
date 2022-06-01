using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Person
    {
        #region Properiarities
        int personId;
        int userAccountId;
        int townId;
        int schoolId;
        string names;
        string lastName;
        string secondLastName;
        string address;
        string ci;
        string ciextension;
        DateTime birthDate;
        byte[] photo;
        DateTime startDate;
        DateTime finishDate;
        string email;
        double latitude;
        double longitude;
        string phone;
        string gender;
        byte status;
        DateTime registrationDate;
        DateTime updateDate;
        string extra;
        



        #endregion
        #region Getters/Setters
        public int PersonId { get => personId; set => personId = value; }
        public int UserAccountId { get => userAccountId; set => userAccountId = value; }
        public int SchoolId { get => schoolId; set => schoolId = value; }
        public string Names { get => names; set => names = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string SecondLastName { get => secondLastName; set => secondLastName = value; }
        public string Address { get => address; set => address = value; }
        public string Ci { get => ci; set => ci = value; }
        public string Ciextension { get => ciextension; set => ciextension = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public byte[] Photo { get => photo; set => photo = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime FinishDate { get => finishDate; set => finishDate = value; }
        public string Email { get => email; set => email = value; }
        public double Latitude { get => latitude; set => latitude = value; }
        public double Longitude { get => longitude; set => longitude = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Gender { get => gender; set => gender = value; }
        public byte Status { get => status; set => status = value; }
        public DateTime RegistrationDate { get => registrationDate; set => registrationDate = value; }
        public DateTime UpdateDate { get => updateDate; set => updateDate = value; }
        public int TownId { get => townId; set => townId = value; }
        public string Extra { get => extra; set => extra = value; }
        #endregion
        #region Constructor
        public Person(int personId, int userAccountId, int schoolId, string names, string lastName, string secondLastName, string address, string ci, string ciextension, DateTime birthDate, byte[] photo, DateTime startDate, DateTime finishDate, string email, double latitude, double longitude, string phone, string gender, byte status, DateTime registrationDate, DateTime updateDate)
        {
            this.personId = personId;
            this.userAccountId = userAccountId;
            this.schoolId = schoolId;
            this.names = names;
            this.lastName = lastName;
            this.secondLastName = secondLastName;
            this.address = address;
            this.ci = ci;
            this.ciextension = ciextension;
            this.birthDate = birthDate;
            this.photo = photo;
            this.startDate = startDate;
            this.finishDate = finishDate;
            this.email = email;
            this.latitude = latitude;
            this.longitude = longitude;
            this.phone = phone;
            this.gender = gender;
            this.status = status;
            this.registrationDate = registrationDate;
            this.updateDate = updateDate;
        }
        public Person( string names, string lastName, string secondLastName, string address, string ci, string ciextension, DateTime birthDate, byte[] photo, string email, double latitude, double longitude, string phone, string gender,int townId)
        {
            
            this.names = names;
            this.lastName = lastName;
            this.secondLastName = secondLastName;
            this.address = address;
            this.ci = ci;
            this.ciextension = ciextension;
            this.birthDate = birthDate;
            this.photo = photo;
            this.email = email;
            this.latitude = latitude;
            this.longitude = longitude;
            this.phone = phone;
            this.gender = gender;
            this.townId = townId;
        }
        public Person(string names, string lastName, string secondLastName, string address, string ci, string ciextension, DateTime birthDate, byte[] photo, string email, double latitude, double longitude, string phone, string gender, int townId,string extra)
        {

            this.names = names;
            this.lastName = lastName;
            this.secondLastName = secondLastName;
            this.address = address;
            this.ci = ci;
            this.ciextension = ciextension;
            this.birthDate = birthDate;
            this.photo = photo;
            this.email = email;
            this.latitude = latitude;
            this.longitude = longitude;
            this.phone = phone;
            this.gender = gender;
            this.townId = townId;
            this.extra = extra;
        }
        public Person()
        {

            
        }
        #endregion
    }
}
