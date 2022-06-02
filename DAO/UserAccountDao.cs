using Model;
using System.Data;

namespace DAO
{
    public interface UserAccountDao : IDao<UserAccount>
    {
        DataTable GET(string userName);
    }
}
