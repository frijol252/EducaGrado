using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Province
    {
        #region Properiarities
        int cityId;
        int provinceId;
        string provinceName;
        byte status;



        #endregion
        #region Getters/Setters
        public int CityId { get => cityId; set => cityId = value; }
        public string Name { get => name; set => name = value; }
        public byte Status { get => status; set => status = value; }
        public int ProvinceId { get => provinceId; set => provinceId = value; }



        #endregion
        #region Constructor
        public Province(int provinceId, int cityId, string name, byte status)
        {
            this.provinceId = provinceId;
            this.cityId = cityId;
            this.name = name;
            this.status = status;
        }

        #endregion
    }
}
