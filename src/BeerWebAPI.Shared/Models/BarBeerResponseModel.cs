namespace BeerWebAPI.Shared.Models
{
    public class BarBeerResponseModel
    {
        public int BarId { get; set; }
        public string BarName { get; set; }
        public List<BeerModel>? Beers { get; set; }
    }
}
