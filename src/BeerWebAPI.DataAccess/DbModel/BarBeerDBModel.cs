namespace BeerWebAPI.DataAccess.DbModel
{
    /// <summary>
    /// Model for table Bar_Beer
    /// </summary>
    public class BarBeerDBModel
    {
        public int Id { get; set; }
        public int BarId { get; set; }
        public int BeerId { get; set; }
    }
}
