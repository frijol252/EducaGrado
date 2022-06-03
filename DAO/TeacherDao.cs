using Model;
using System.Data;

namespace DAO
{
    public interface TeacherDao : IDao<Teacher>
    {
        DataTable SelectDis();
        Person Get(int idperson);
        void UpdateTransact(Person p);
        
    }
}
