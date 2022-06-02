using System.Data;

namespace DAO
{
    public interface IDao<T>
    {
        int Insert(T t);
        int Update(T t);
        void Delete(T t);
        DataTable Select();
    }
}
