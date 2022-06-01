using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Class
    {
        int classId;
        int courseId;
        int scheduleId;
        int matterId;
        byte status;
        int teacherId;
        string day;

        public int ClassId { get => classId; set => classId = value; }
        public int CourseId { get => courseId; set => courseId = value; }
        public int ScheduleId { get => scheduleId; set => scheduleId = value; }
        public int MatterId { get => matterId; set => matterId = value; }
        public byte Status { get => status; set => status = value; }
        public int TeacherId { get => teacherId; set => teacherId = value; }
        public string Day { get => day; set => day = value; }

        public Class(int classId, int courseId, int scheduleId, int matterId, byte status, int teacherId,string day)
        {
            this.classId = classId;
            this.courseId = courseId;
            this.scheduleId = scheduleId;
            this.matterId = matterId;
            this.status = status;
            this.teacherId = teacherId;
            this.day = day;
        }
        public Class(int courseId, int scheduleId, int matterId,string day)
        {
            this.courseId = courseId;
            this.scheduleId = scheduleId;
            this.matterId = matterId;
            this.day = day;
        }
        public Class()
        {
        }
    }
}
