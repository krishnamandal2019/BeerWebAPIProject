namespace BeerWebAPI.DataAccess.DbModel
{
    /// <summary>
    /// Model for table Beer
    /// </summary>
    public class BeerDBModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal PercentageAlcoholByVolume { get; set; }
    }
}
