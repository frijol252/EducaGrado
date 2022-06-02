namespace Model
{
    public class City
    {
        #region Properiarities
        int cityId;
        string name;
        byte status;



        #endregion
        #region Getters/Setters
        public int CityId { get => cityId; set => cityId = value; }
        public string Name { get => name; set => name = value; }
        public byte Status { get => status; set => status = value; }


        #endregion
        #region Constructor
        public City(int cityId, string name, byte status)
        {
            this.cityId = cityId;
            this.name = name;
            this.status = status;
        }
        #endregion
    }
}
