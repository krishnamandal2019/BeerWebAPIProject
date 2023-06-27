using BeerWebAPI.Shared.Models;

namespace BeerWebAPI.ControllerTests.TestData
{
    /// <summary>
    /// This class contain the test data for BarBeer Controller Tests.
    /// </summary>
    internal class BarBeerControllerTestData
    {
        internal static List<BarBeerResponseModel> GetBarBeerList() => new() {
            new BarBeerResponseModel
            {
                BarId = 1,
                BarName = "KnightBarMumbai",
                Beers = new List<BeerModel> { new BeerModel { Name = "Kingfisher", PercentageAlcoholByVolume = 0.8M, Id = 1 }}
            }
        };
        internal static BarBeerResponseModel GetBarBeerListById(int id) => GetBarBeerList().FirstOrDefault(x => x.BarId == id);
    }
}
