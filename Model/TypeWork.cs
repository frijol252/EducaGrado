namespace Model
{
    public class TypeWork
    {
        #region Properiarities
        int typeWorkId;
        int modalityId;
        int schoolTypeId;

        #endregion
        #region Getters/Setters
        public int TypeWorkId { get => typeWorkId; set => typeWorkId = value; }
        public int ModalityId { get => modalityId; set => modalityId = value; }
        public int SchoolTypeId { get => schoolTypeId; set => schoolTypeId = value; }
        #endregion
        #region Constructor
        public TypeWork(int typeWorkId, int modalityId, int schoolTypeId)
        {
            this.typeWorkId = typeWorkId;
            this.modalityId = modalityId;
            this.schoolTypeId = schoolTypeId;
        }
        #endregion
    }
}
