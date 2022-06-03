﻿namespace Model
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

        public static void setnulls()
        {
            sessionID=-1;
            sessionRole=-1;
            sessionCurrent=null;
            sessionPersonId=-1;
            sessionSchoolId=-1;
            sessionSchoolName = null;
        }

    }
}
