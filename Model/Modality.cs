namespace Model
{
    public class Modality
    {
        #region Properiarities
        int modalityId;
        byte numberGrades;
        byte numberTest;
        string typeQualify;

        byte percentGrades;
        byte percentTest;



        #endregion
        #region Getters/Setters
        public int ModalityId { get => modalityId; set => modalityId = value; }
        public byte NumberGrades { get => numberGrades; set => numberGrades = value; }
        public byte NumberTest { get => numberTest; set => numberTest = value; }
        public string TypeQualify { get => typeQualify; set => typeQualify = value; }
        public byte PercentGrades { get => percentGrades; set => percentGrades = value; }
        public byte PercentTest { get => percentTest; set => percentTest = value; }
        #endregion
        #region Constructor
        public Modality(int modalityId, byte numberGrades, byte numberTest, string typeQualify, byte percentGrades, byte percentTest)
        {
            this.modalityId = modalityId;
            this.numberGrades = numberGrades;
            this.numberTest = numberTest;
            this.typeQualify = typeQualify;
            this.percentGrades = percentGrades;
            this.PercentTest = percentTest;
        }
        public Modality()
        {

        }
        #endregion
    }
}
