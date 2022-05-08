using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Town
    {
        #region Properiarities
        int townId;
        int provinceId;
        string provinceName;
        byte status;


        #endregion
        #region Getters/Setters
        public int TownId { get => townId; set => townId = value; }
        public int ProvinceId { get => provinceId; set => provinceId = value; }
        public string ProvinceName { get => provinceName; set => provinceName = value; }
        public byte Status { get => status; set => status = value; }


        #endregion
        #region Constructor
        public Town(int townId, int provinceId, string provinceName, byte status)
        {
            this.townId = townId;
            this.provinceId = provinceId;
            this.provinceName = provinceName;
            this.status = status;
        }

        #endregion
    }
}
