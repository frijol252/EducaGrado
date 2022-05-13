using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SectionGrade
    {
        int sectionGradeId;
        int gradeId;
        double score;
        byte typeScore;

        public int SectionGradeId { get => sectionGradeId; set => sectionGradeId = value; }
        public int GradeId { get => gradeId; set => gradeId = value; }
        public double Score { get => score; set => score = value; }
        public byte TypeScore { get => typeScore; set => typeScore = value; }

        public SectionGrade(int sectionGradeId, int gradeId, double score, byte typeScore)
        {
            this.sectionGradeId = sectionGradeId;
            this.gradeId = gradeId;
            this.score = score;
            this.typeScore = typeScore;
        }
    }
}
