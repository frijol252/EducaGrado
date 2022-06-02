namespace Model
{
    public class RoleUser
    {
        int roleUserId;
        string roleName;

        public int RoleUserId { get => roleUserId; set => roleUserId = value; }
        public string RoleName { get => roleName; set => roleName = value; }

        public RoleUser(int roleUserId, string roleName)
        {
            this.roleUserId = roleUserId;
            this.roleName = roleName;
        }
    }
}
