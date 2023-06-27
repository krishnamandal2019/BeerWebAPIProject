using BeerWebAPI.Shared.Models;

namespace BeerWebAPI.Service.Interface
{
    /// <summary>
    /// IBreweryBeerService interface that contain the required methods.
    /// </summary>
    public interface IBreweryBeerService
    {
        bool IntroduceBeerToBrewery(int breweryId, int beerId);
        BreweryBeerResponseModel? GetBreweryWithServedBeersByBreweryId(int breweryId);
        List<BreweryBeerResponseModel>? GetAllBreweriesWithServedBeers();
    }
}
