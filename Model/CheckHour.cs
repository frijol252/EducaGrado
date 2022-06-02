using System;

namespace Model
{
    public class CheckHour
    {
        int checkHourId;
        int employeeId;
        DateTime checkTime;

        public int CheckHourId { get => checkHourId; set => checkHourId = value; }
        public int EmployeeId { get => employeeId; set => employeeId = value; }
        public DateTime CheckTime { get => checkTime; set => checkTime = value; }

        public CheckHour(int checkHourId, int employeeId, DateTime checkTime)
        {
            this.checkHourId = checkHourId;
            this.employeeId = employeeId;
            this.checkTime = checkTime;
        }
    }
}
