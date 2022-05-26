using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Matter
    {
        int matterId;
        string matterName;
        int categoryId;
        byte status;
        DateTime registrationDate;
        DateTime updateDate;

        public int MatterId { get => matterId; set => matterId = value; }
        public string MatterName { get => matterName; set => matterName = value; }
        public int CategoryId { get => categoryId; set => categoryId = value; }
        public byte Status { get => status; set => status = value; }
        public DateTime RegistrationDate { get => registrationDate; set => registrationDate = value; }
        public DateTime UpdateDate { get => updateDate; set => updateDate = value; }

        public Matter(int matterId, string matterName, int categoryId, byte status, DateTime registrationDate, DateTime updateDate)
        {
            this.matterId = matterId;
            this.matterName = matterName;
            this.categoryId = categoryId;
            this.status = status;
            this.registrationDate = registrationDate;
            this.updateDate = updateDate;
        }
        public Matter(string matterName, int categoryId)
        {
            this.matterName = matterName;
            this.categoryId = categoryId;
        }
        public Matter(int matterId, string matterName)
        {
            this.matterId = matterId;
            this.matterName = matterName;
        }
        public Matter()
        {
            
        }
    }
}
