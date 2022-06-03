using Model;
using System.Collections.Generic;
using System.Data;

namespace DAO
{
    public interface ClassDao : IDao<Class>
    {
        DataTable Select(int courseId);
        void Inserttransact(List<Class> t);
        void Updatetransact(List<Class> t);
        DataTable SelectStudents(int id);
        int UpdateTeacher(int idClass, int idTeacher);

    }
}
