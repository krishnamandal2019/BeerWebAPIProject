using BeerWebAPI.Shared.Models;

namespace BeerWebAPI.Service.Interface
{
    /// <summary>
    /// IBreweryService interface that contain the required methods.
    /// </summary>
    public interface IBreweryService
    {
        bool CreateBrewery(BreweryModel model);
        bool ModifyBrewery(BreweryModel model, int id);
        List<BreweryModel>? GetAllBreweries();
        BreweryModel? GetBreweryDetailsById(int id);
    }
}
