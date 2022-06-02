namespace Model
{
    public class SchoolType
    {

        #region Properiarities
        int schoolTypeId;
        string nameType;
        string cursos;

        #endregion
        #region Getters/Setters
        public int SchoolTypeId { get => schoolTypeId; set => schoolTypeId = value; }
        public string NameType { get => nameType; set => nameType = value; }
        public string Cursos { get => cursos; set => cursos = value; }




        #endregion
        #region Constructor
        public SchoolType(int schoolTypeId, string nameType, string cursos)
        {
            this.schoolTypeId = schoolTypeId;
            this.nameType = nameType;
            this.cursos = cursos;
        }
        public SchoolType()
        {

        }
        #endregion
    }
}
