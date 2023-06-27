using BeerWebAPI.Shared.Models;

namespace BeerWebAPI.Service.Interface
{
    /// <summary>
    /// IBarBeerService interface that contain the required methods.
    /// </summary>
    public interface IBarBeerService
    {
        bool IntroduceBeerTobar(int barId, int beerId);
        BarBeerResponseModel? GetBarWithServedBeersByBarId(int barId);
        List<BarBeerResponseModel>? GetAllBarsWithServedBeers();
    }
}
