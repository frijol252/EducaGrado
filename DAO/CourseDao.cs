using Model;
using System.Data;

namespace DAO
{
    public interface CourseDao : IDao<Course>
    {
        DataTable SelectLike(string like);
    }
}
