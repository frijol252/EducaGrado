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
        private byte[] password;
        private byte[] key;
        private byte[] vi;
        private byte roleUserId;
        private byte state;
        private byte revisionPass;
        private DateTime createDate;
        private DateTime lastUpdate;
        public byte[] Password { get => password; set => password = value; }
        public string UserName { get => userName; set => userName = value; }
        public byte[] Key { get => key; set => key = value; }
        public DateTime LastUpdate { get => lastUpdate; set => lastUpdate = value; }
        public DateTime CreateDate { get => createDate; set => createDate = value; }
        public byte State { get => state; set => state = value; }
        public byte[] VI { get => vi; set => vi = value; }
        public int UserID { get => userID; set => userID = value; }
        public byte RoleUserId { get => roleUserId; set => roleUserId = value; }
        public byte RevisionPass { get => revisionPass; set => revisionPass = value; }

        //GET
        public UserAccount(int userID, string userName, byte[] password, byte[] key, byte[] vi, byte roleUserId, byte revisionPass, byte state, DateTime createDate, DateTime lastUpdate)
        {
            this.userID = userID;
            this.userName = userName;
            this.password = password;
            this.vi = vi;
            this.state = state;
            this.createDate = createDate;
            this.lastUpdate = lastUpdate;
            this.key = key;
            this.roleUserId = roleUserId;
            this.revisionPass = revisionPass;
        }
        //INSERT
        public UserAccount(string userName, byte[] password, byte[] key, byte[] vi, byte roleUserId)
        {
            this.userName = userName;
            this.password = password;
            this.vi = vi;
            this.key = key;
            this.roleUserId = roleUserId;
        }
        //UPDATE password
        public UserAccount(byte[] password, int userID)
        {
            this.password = password;
            this.userID = userID;
        }

        public UserAccount()
        {
            
        }
    }
}
