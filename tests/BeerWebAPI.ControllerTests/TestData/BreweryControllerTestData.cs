using BeerWebAPI.Shared.Models;

namespace BeerWebAPI.ControllerTests.TestData
{
    /// <summary>
    /// This class contain the test data for BreweryBeer Controller Tests.
    /// </summary>
    internal class BreweryControllerTestData
    {
        internal static List<BreweryModel> GetBreweryList() => new() { new BreweryModel { Name = "United BeerModel Factory", Id = 1 } };
        internal static BreweryModel? GetBarListById(int id) => GetBreweryList().FirstOrDefault(x => x.Id == id);
    }
}
