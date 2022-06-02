using Model;
using System.Data;

namespace DAO
{
    public interface ScheduleDao : IDao<Schedule>
    {
        DataTable SelectHourClass(int course);
        DataTable SelectHourClass(int course, int idclass);
    }
}
