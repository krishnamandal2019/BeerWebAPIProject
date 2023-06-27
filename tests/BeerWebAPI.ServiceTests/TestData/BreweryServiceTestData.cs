using BeerWebAPI.DataAccess.DbModel;

namespace BeerWebAPI.ServiceTests.TestData
{
    /// <summary>
    /// This class contain the test data for Brewery Service Tests.
    /// </summary>
    internal class BreweryServiceTestData
    {
        internal static BreweryDBModel GetBreweryListById(int id) => GetBreweryList().FirstOrDefault(x => x.Id == id);

        internal static List<BreweryDBModel> GetBreweryList() => new() { new BreweryDBModel { Name = "United Beer Company", Id = 1 } };
    }
}
