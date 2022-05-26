using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CategoryMatter
    {
        int categoryId;
        string categoryName;
        byte status;
        DateTime registrationDate;
        DateTime updateDate;
        int modalityId;

        public int CategoryId { get => categoryId; set => categoryId = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
        public byte Status { get => status; set => status = value; }
        public DateTime RegistrationDate { get => registrationDate; set => registrationDate = value; }
        public DateTime UpdateDate { get => updateDate; set => updateDate = value; }
        public int ModalityId { get => modalityId; set => modalityId = value; }

        public CategoryMatter(int categoryId, string categoryName, byte status, DateTime registrationDate, DateTime updateDate, int modalityId)
        {
            this.categoryId = categoryId;
            this.categoryName = categoryName;
            this.status = status;
            this.registrationDate = registrationDate;
            this.updateDate = updateDate;
            this.modalityId = modalityId;
        }
        public CategoryMatter()
        {
            
        }
        public CategoryMatter(int categoryId, string categoryName)
        {
            this.categoryId = categoryId;
            this.categoryName = categoryName;
        }
    }
}
