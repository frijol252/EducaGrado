using Model;

namespace DAO
{
    public interface SchoolDao : IDao<School>
    {
        void UpdateTypeWork(SchoolType schoolType, Modality modality, int schoolid);
    }
}
