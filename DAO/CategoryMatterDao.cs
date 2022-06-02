using Model;
using System.Collections.Generic;
using System.Data;

namespace DAO
{
    public interface CategoryMatterDao : IDao<CategoryMatter>
    {
        DataTable SelectLike(string like);
        void updateCategory(List<CategoryMatter> categoryMatters);
    }
}
