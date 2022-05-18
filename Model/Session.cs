using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Session
    {
        private static int sessionID;
        private static int sessionRole;
        private static string sessionCurrent;
        private static int sessionPersonId;
        private static int sessionSchoolId;
        private static string sessionSchoolName;

        public static int SessionID { get => sessionID; set => sessionID = value; }
        public static int SessionRole { get => sessionRole; set => sessionRole = value; }
        public static string SessionCurrent { get => sessionCurrent; set => sessionCurrent = value; }
        public static int SessionPersonId { get => sessionPersonId; set => sessionPersonId = value; }
        public static int SessionSchoolId { get => sessionSchoolId; set => sessionSchoolId = value; }
        public static string SessionSchoolName { get => sessionSchoolName; set => sessionSchoolName = value; }

    }
}
