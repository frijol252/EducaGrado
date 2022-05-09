using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Attendance
    {
        int attendanceId;
        int classId;
        int studentId;
        DateTime dateAttendance;

        public int AttendanceId { get => attendanceId; set => attendanceId = value; }
        public int ClassId { get => classId; set => classId = value; }
        public int StudentId { get => studentId; set => studentId = value; }
        public DateTime DateAttendance { get => dateAttendance; set => dateAttendance = value; }

        public Attendance(int attendanceId, int classId, int studentId, DateTime dateAttendance)
        {
            this.attendanceId = attendanceId;
            this.classId = classId;
            this.studentId = studentId;
            this.dateAttendance = dateAttendance;
        }
    }
}
