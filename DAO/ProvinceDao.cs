using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface ProvinceDao : IDao<Province>
    {
        DataTable Select(int idcity);
    }
}
