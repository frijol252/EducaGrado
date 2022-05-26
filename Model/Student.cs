using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Student
    {
        int studentId;
        int courseId;
        string rudeNume;

        public int StudentId { get => studentId; set => studentId = value; }
        public int CourseId { get => courseId; set => courseId = value; }
        public string RudeNume { get => rudeNume; set => rudeNume = value; }

        public Student(int studentId, int courseId, string rudeNume)
        {
            this.studentId = studentId;
            this.courseId = courseId;
            this.rudeNume = rudeNume;
        }

    }
}
