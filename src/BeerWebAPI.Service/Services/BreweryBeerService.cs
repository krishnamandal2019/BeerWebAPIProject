using BeerWebAPI.Service.Interface;
using BeerWebAPI.DataAccess.Interface;
using BeerWebAPI.Shared.Models;
using BeerWebAPI.DataAccess.DbModel;

namespace BeerWebAPI.Service.Services
{
    public class BreweryBeerService : IBreweryBeerService
    {
        private readonly IRelationalRepository<BreweryBeerDBModel, BreweryBeerResponseModel> _breweryBeerRepository;
        public BreweryBeerService(IRelationalRepository<BreweryBeerDBModel, BreweryBeerResponseModel> breweryBeerRepository)
        {
            _breweryBeerRepository = breweryBeerRepository;
        }

        /// <summary>
        /// Add new beer linked with brewery
        /// </summary>
        /// <param name="breweryId"></param>
        /// <param name="beerId"></param>
        /// <returns>bool</returns>
        public bool IntroduceBeerToBrewery(int breweryId, int beerId)
        {
            return _breweryBeerRepository.Add(new BreweryBeerDBModel { BeerId = beerId, BreweryId = breweryId });
        }

        /// <summary>
        /// Get brewery with served beers
        /// </summary>
        /// <param name="breweryId"></param>
        /// <returns>BreweryBeerResponseModel</returns>
        public BreweryBeerResponseModel? GetBreweryWithServedBeersByBreweryId(int breweryId)
        {
            return _breweryBeerRepository.GetById(breweryId);
        }

        /// <summary>
        /// Get all breweries with served beers
        /// </summary>
        /// <returns>List<BreweryBeerResponseModel></returns>
        public List<BreweryBeerResponseModel>? GetAllBreweriesWithServedBeers()
        {
            return _breweryBeerRepository.GetAll();
        }
    }
}