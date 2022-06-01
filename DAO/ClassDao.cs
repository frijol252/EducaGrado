using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface ClassDao : IDao<Class>
    {
        DataTable Select(int courseId);
        void Inserttransact(List<Class> t);
        void Updatetransact(List<Class> t);
        DataTable SelectStudents(int id);
        
    }
}
