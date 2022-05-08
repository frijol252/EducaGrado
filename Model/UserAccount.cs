using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserAccount
    {
        private int userID;
        private string userName;
        private string password;
        private byte[] key;
        private byte[] vi;
        private byte state;
        private DateTime createDate;
        private DateTime lastUpdate;
        public string Password { get => password; set => password = value; }
        public string UserName { get => userName; set => userName = value; }
        public byte[] Key { get => key; set => key = value; }
        public DateTime LastUpdate { get => lastUpdate; set => lastUpdate = value; }
        public DateTime CreateDate { get => createDate; set => createDate = value; }
        public byte State { get => state; set => state = value; }
        public byte[] VI { get => vi; set => vi = value; }
        public int UserID { get => userID; set => userID = value; }

        //GET
        public UserAccount(int userID, string userName, string password, byte[] vi, byte state, DateTime createDate, DateTime lastUpdate, byte[] key)
        {
            this.userID = userID;
            this.userName = userName;
            this.password = password;
            this.vi = vi;
            this.state = state;
            this.createDate = createDate;
            this.lastUpdate = lastUpdate;
            this.key = key;
        }
        //INSERT
        public UserAccount(string userName, string password, byte[] vi, byte[] key)
        {
            this.userName = userName;
            this.password = password;
            this.vi = vi;
            this.key = key;
        }
        //UPDATE password
        public UserAccount(string password, int userID)
        {
            this.password = password;
            this.userID = userID;
        }
    }
}
