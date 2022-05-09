using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SchoolType
    {

        #region Properiarities
        int schoolTypeId;
        string nameType;


        #endregion
        #region Getters/Setters
        public int SchoolTypeId { get => schoolTypeId; set => schoolTypeId = value; }
        public string NameType { get => nameType; set => nameType = value; }


        #endregion
        #region Constructor
        public SchoolType(int schoolTypeId, string nameType)
        {
            this.schoolTypeId = schoolTypeId;
            this.nameType = nameType;
        }
        #endregion
    }
}
