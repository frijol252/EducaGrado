using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface CategoryMatterDao : IDao<CategoryMatter>
    {
        DataTable SelectLike(string like);
        void updateCategory(List<CategoryMatter> categoryMatters);
    }
}
