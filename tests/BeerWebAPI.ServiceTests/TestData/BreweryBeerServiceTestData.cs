using BeerWebAPI.Shared.Models;

namespace BeerWebAPI.ServiceTests.TestData
{
    /// <summary>
    /// This class contain the test data for BreweryBeer Service Tests.
    /// </summary>
    internal class BreweryBeerServiceTestData
    {
        internal static List<BreweryBeerResponseModel> GetBreweryBeerList() => new() {
            new BreweryBeerResponseModel
            {
                BreweryName = "United",
                BreweryId = 1,
                Beers = new List<BeerModel>(){new BeerModel{ Id=1,Name="KIngfisher", PercentageAlcoholByVolume=8.0M } }
            }
        };

        internal static BreweryBeerResponseModel GetBreweryBeerListById(int breweryId) => GetBreweryBeerList().FirstOrDefault(x => x.BreweryId == breweryId);
    }
}
