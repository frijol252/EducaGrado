using Model;
using System.Data;

namespace DAO
{
    public interface TownDao : IDao<Town>
    {
        DataTable Select(int idcity);
    }
}
