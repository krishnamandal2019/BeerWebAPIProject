using BeerWebAPI.Shared.Models;

namespace BeerWebAPI.ControllerTests.TestData
{
    /// <summary>
    /// This class contain the test data for Beer Controller Tests.
    /// </summary>
    internal class BeerControllerTestData
    {
        internal static List<BeerModel> GetBeerList() => new() { new BeerModel { Name = "Tuburg", PercentageAlcoholByVolume = 6.0M, Id = 1 } };
        internal static BeerModel? GetBeerListById(int id) => GetBeerList().FirstOrDefault(x => x.Id == id);
        internal static List<BeerModel> GetBeerListByParameter(decimal param1, decimal param2) => GetBeerList().Where(x => x.PercentageAlcoholByVolume < param1 && x.PercentageAlcoholByVolume > param2).ToList();
    }
}
