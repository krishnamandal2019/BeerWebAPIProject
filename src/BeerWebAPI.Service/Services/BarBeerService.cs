using BeerWebAPI.Service.Interface;
using BeerWebAPI.DataAccess.Interface;
using BeerWebAPI.Shared.Models;
using BeerWebAPI.DataAccess.DbModel;

namespace BeerWebAPI.Service.Services
{
    public class BarBeerService : IBarBeerService
    {
        private readonly IRelationalRepository<BarBeerDBModel, BarBeerResponseModel> _barBeerRepository;
        public BarBeerService(IRelationalRepository<BarBeerDBModel, BarBeerResponseModel> barBeerRepository)
        {
            _barBeerRepository = barBeerRepository;
        }

        /// <summary>
        /// Add new beer linked with bar
        /// </summary>
        /// <param name="barId"></param>
        /// <param name="beerId"></param>
        /// <returns>bool</returns>
        public bool IntroduceBeerTobar(int barId, int beerId)
        {
            return _barBeerRepository.Add(new BarBeerDBModel { BarId = barId, BeerId = beerId });
        }

        /// <summary>
        /// Get Bar with served beers by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BarBeerResponseModel</returns>
        public BarBeerResponseModel? GetBarWithServedBeersByBarId(int id)
        {
            return _barBeerRepository.GetById(id);
        }

        /// <summary>
        /// Get all bars with served beer.
        /// </summary>
        /// <returns>List<BarBeerResponseModel></returns>
        public List<BarBeerResponseModel>? GetAllBarsWithServedBeers()
        {
            return _barBeerRepository.GetAll();
        }
    }
}