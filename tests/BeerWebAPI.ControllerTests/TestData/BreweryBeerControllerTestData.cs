using BeerWebAPI.Shared.Models;

namespace BeerWebAPI.ControllerTests.TestData
{
    /// <summary>
    /// This class contain the test data for Brewery Controller Tests.
    /// </summary>
    internal class BreweryBeerControllerTestData
    {
        internal static List<BreweryBeerResponseModel> GetBreweryBeerList() => new() { new BreweryBeerResponseModel
            {
                BreweryId = 1,
                BreweryName = "Asahi BeerModel",
                Beers = new List<BeerModel> { new BeerModel { Name = "Kingfisher", PercentageAlcoholByVolume = 0.8M, Id = 1 }}
            }
        };
        internal static BreweryBeerResponseModel GetBreweryBeerListById(int id) => GetBreweryBeerList().FirstOrDefault(x => x.BreweryId == id);

    }
}
