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
        
        public byte Status { get => status; set => status = value; }
        public int ProvinceId { get => provinceId; set => provinceId = value; }



        #endregion
        #region Constructor
        public Province(int provinceId, int cityId, string name, byte status, string provinceName)
        {
            this.provinceId = provinceId;
            this.cityId = cityId;
            this.provinceName = name;
            this.status = status;
            this.provinceName = provinceName;
        }

        #endregion
    }
}
