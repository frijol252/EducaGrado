using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Course
    {
        #region Properiarities
        int idcourse;
        int number;
        string letter;
        string sectionCourse;


        #endregion
        #region Getters/Setters
        public int Idcourse { get => idcourse; set => idcourse = value; }
        public int Number { get => number; set => number = value; }
        public string Letter { get => letter; set => letter = value; }
        public string SectionCourse { get => sectionCourse; set => sectionCourse = value; }


        #endregion
        #region Constructor
        public Course(int idcourse, int number, string letter, string sectionCourse)
        {
            this.idcourse = idcourse;
            this.number = number;
            this.letter = letter;
            this.sectionCourse = sectionCourse;
        }
        public Course()
        {
            
        }
        #endregion
    }
}
