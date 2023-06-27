using BeerWebAPI.Shared.Models;

namespace BeerWebAPI.Service.Interface
{
    /// <summary>
    /// IBeerService that contain the required methods.
    /// </summary>
    public interface IBeerService
    {
        bool CreateBeer(BeerModel model);
        List<BeerModel>? GetAllBeers(decimal lessthanAlcoholPercentage, decimal greaterthanAlcoholPercentage);
        BeerModel? GetBeerDetailsById(int id);
        bool ModifyBeer(BeerModel model, int id);
    }
}
