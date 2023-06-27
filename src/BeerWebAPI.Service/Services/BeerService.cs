using BeerWebAPI.Service.Interface;
using BeerWebAPI.DataAccess.Interface;
using BeerWebAPI.Service.Mapper;
using BeerWebAPI.Shared.Models;
using BeerWebAPI.DataAccess.DbModel;

namespace BeerWebAPI.Service.Services
{
    public class BeerService : IBeerService
    {
        private readonly IBeerRepository _beerRepository;
        private readonly IRepository<BeerDBModel> _repository;

        public BeerService(IBeerRepository beerRepository, IRepository<BeerDBModel> repository)
        {
            _beerRepository = beerRepository;
            _repository = repository;
        }

        /// <summary>
        /// Add new Beer details
        /// </summary>
        /// <param name="model"></param>
        /// <returns>bool</returns>
        public bool CreateBeer(BeerModel model)
        {
            return _repository.Add(MapperObject<BeerModel, BeerDBModel>.Mapper.Map<BeerDBModel>(model));
        }

        /// <summary>
        /// Get beer details with alcohol percentage parameter
        /// </summary>
        /// <param name="lessthanAlcoholPercentage"></param>
        /// <param name="greaterthanAlcoholPercentage"></param>
        /// <returns>List<BeerModel></returns>
        public List<BeerModel>? GetAllBeers(decimal lessthanAlcoholPercentage, decimal greaterthanAlcoholPercentage)
        {
            List<BeerDBModel> listBeerModel = _beerRepository.GetBeerByAlcoholParameter(lessthanAlcoholPercentage, greaterthanAlcoholPercentage);
            if (listBeerModel != null)
                return MapperObject<BeerDBModel, BeerModel>.Mapper.Map<List<BeerModel>>(listBeerModel);
            return null;
        }

        /// <summary>
        /// Get Beer Details based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BeerModel</returns>
        public BeerModel? GetBeerDetailsById(int id)
        {
            var item = _repository.GetById(id);
            if (item != null)
                return MapperObject<BeerDBModel, BeerModel>.Mapper.Map<BeerModel>(item);
            return null;
        }

        /// <summary>
        /// Update beer details by beer id
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public bool ModifyBeer(BeerModel model, int id)
        {
            var item = _repository.GetById(id);
            if (item != null)
            {
                item.Name = model.Name;
                item.PercentageAlcoholByVolume = model.PercentageAlcoholByVolume;
                item.Id = id;
                return _repository.Update(item);
            }
            return false;
        }
    }
}
