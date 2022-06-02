using Model;

namespace DAO
{
    public interface PersonDao : IDao<Person>
    {
        Person SelectPerson(int idPerson);
    }
}
