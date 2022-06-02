namespace Model
{
    public class Modality
    {
        #region Properiarities
        int modalityId;
        byte numberGrades;
        byte numberTest;
        string typeQualify;




        #endregion
        #region Getters/Setters
        public int ModalityId { get => modalityId; set => modalityId = value; }
        public byte NumberGrades { get => numberGrades; set => numberGrades = value; }
        public byte NumberTest { get => numberTest; set => numberTest = value; }
        public string TypeQualify { get => typeQualify; set => typeQualify = value; }
        #endregion
        #region Constructor
        public Modality(int modalityId, byte numberGrades, byte numberTest, string typeQualify)
        {
            this.modalityId = modalityId;
            this.numberGrades = numberGrades;
            this.numberTest = numberTest;
            this.typeQualify = typeQualify;
        }
        public Modality()
        {

        }
        #endregion
    }
}
