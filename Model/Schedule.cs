using System;

namespace Model
{
    public class Schedule
    {

        int scheduleId;
        string day;
        DateTime startHour;
        DateTime finishHour;
        byte status;
        DateTime registrationDate;
        DateTime updateDate;

        public string Day { get => day; set => day = value; }
        public DateTime StartHour { get => startHour; set => startHour = value; }
        public DateTime FinishHour { get => finishHour; set => finishHour = value; }
        public byte Status { get => status; set => status = value; }
        public DateTime RegistrationDate { get => registrationDate; set => registrationDate = value; }
        public DateTime UpdateDate { get => updateDate; set => updateDate = value; }
        public int ScheduleId { get => scheduleId; set => scheduleId = value; }

        public Schedule(int scheduleId, string day, DateTime startHour, DateTime finishHour, byte status, DateTime registrationDate, DateTime updateDate)
        {
            this.scheduleId = scheduleId;
            this.day = day;
            this.startHour = startHour;
            this.finishHour = finishHour;
            this.status = status;
            this.registrationDate = registrationDate;
            this.updateDate = updateDate;
        }
        public Schedule()
        {

        }
    }
}
