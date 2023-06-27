using BeerWebAPI.Shared.Models;

namespace BeerWebAPI.ServiceTests.TestData
{
    /// <summary>
    /// This class contain the test data for BarBeer Service Tests.
    /// </summary>
    internal class BarBeerServiceTestData
    {
        internal static List<BarBeerResponseModel> GetBarBeerList() => new()
        {
            new BarBeerResponseModel
            {
                BarName = "Bar Headquater",
                BarId = 1,
                Beers = new List<BeerModel>(){new BeerModel{ Id = 1, Name = "Kingfisher", PercentageAlcoholByVolume = 8.0M } }
            }
        };

        internal static BarBeerResponseModel? GetBarBeerListById(int barId)
        {
            return GetBarBeerList().FirstOrDefault(x => x.BarId == barId);
        }
    }
}
