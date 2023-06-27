using BeerWebAPI.DataAccess.DbModel;

namespace BeerWebAPI.DataAccess.Interface
{
    /// <summary>
    /// IBeerRepository interface that contain the required methods
    /// </summary>
    public interface IBeerRepository
    {
        List<BeerDBModel> GetBeerByAlcoholParameter(decimal ltAlchPercentage, decimal gtAlchPercentage);
    }
}