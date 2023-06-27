namespace BeerWebAPI.DataAccess.DbModel
{
    /// <summary>
    /// Model for table Brewery_Beer
    /// </summary>
    public class BreweryBeerDBModel
    {
        public int Id { get; set; }
        public int BreweryId { get; set; }
        public int BeerId { get; set; }
    }
}