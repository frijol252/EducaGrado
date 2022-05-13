using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Grade
    {
        int gradeId;
        int studentId;
        int classId;
        byte type;

        public int GradeId { get => gradeId; set => gradeId = value; }
        public int StudentId { get => studentId; set => studentId = value; }
        public int ClassId { get => classId; set => classId = value; }
        public byte Type { get => type; set => type = value; }

        public Grade(int gradeId, int studentId, int classId, byte type)
        {
            this.gradeId = gradeId;
            this.studentId = studentId;
            this.classId = classId;
            this.type = type;
        }
    }
}
