using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface ScheduleDao : IDao<Schedule>
    {
        DataTable SelectHourClass(int course);
        DataTable SelectHourClass(int course,int idclass);
    }
}
