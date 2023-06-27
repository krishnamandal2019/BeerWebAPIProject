using BeerWebAPI.DataAccess.DbModel;

namespace BeerWebAPI.ServiceTests.TestData
{
    /// <summary>
    /// This class contain the test data for Beer Service Tests.
    /// </summary>
    internal class BeerServiceTestData
    {
        internal static BeerDBModel GetBeerListById(int id) => GetBeerList().FirstOrDefault(x => x.Id == id);

        internal static List<BeerDBModel> GetBeerListByParameter(decimal param1, decimal param2) => GetBeerList().Where(x => x.PercentageAlcoholByVolume < param1 && x.PercentageAlcoholByVolume > param2).ToList();

        internal static List<BeerDBModel> GetBeerList() => new() { new BeerDBModel { Name = "KingFisher", PercentageAlcoholByVolume = 9.0M, Id = 1 } };
    }
}
