using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface MatterDao : IDao<Matter> 
    {
        DataTable SelectLike(string like);
        DataTable SelectLikeByCategory(int idcat, string like);
        DataTable SelectByCategory(int idcat);
        DataTable SelectForAddMatters(int idCourse);
        void updateMatters(List<Matter> matters);
    }
}
