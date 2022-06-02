using Model;
using System.Data;

namespace DAO
{
    public interface ProvinceDao : IDao<Province>
    {
        DataTable Select(int idcity);
    }
}
