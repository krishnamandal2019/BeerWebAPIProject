using BeerWebAPI.DataAccess.DbModel;

namespace BeerWebAPI.ServiceTests.TestData
{
    /// <summary>
    /// This class contain the test data for Bar Service Tests.
    /// </summary>
    internal class BarServiceTestData
    {
        internal static BarDBModel GetBarListById(int id) => GetBarList().FirstOrDefault(x => x.Id == id);

        internal static List<BarDBModel> GetBarList() => new() { new BarDBModel { Address = "Greater Noida", Name = "KnightBar", Id = 1 } };
    }
}
