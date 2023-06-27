namespace BeerWebAPI.Shared.Models
{
    public class BreweryBeerResponseModel
    {
        public int BreweryId { get; set; }
        public string BreweryName { get; set; }
        public List<BeerModel>? Beers { get; set; }
    }
}
