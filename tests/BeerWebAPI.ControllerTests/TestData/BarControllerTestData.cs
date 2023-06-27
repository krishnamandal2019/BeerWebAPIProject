using BeerWebAPI.Shared.Models;

namespace BeerWebAPI.ControllerTests.TestData
{
    /// <summary>
    /// This class contain the test data for Bar Controller Tests.
    /// </summary>
    internal class BarControllerTestData
    {
        internal static List<BarModel> GetBarList() => new() { new BarModel { Address = "KnightBar", Name = "Greater Noida", Id = 1 } };
        internal static BarModel GetBarListById(int id) => GetBarList().FirstOrDefault(x => x.Id == id);
    }
}
