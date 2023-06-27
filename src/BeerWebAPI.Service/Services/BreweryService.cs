using BeerWebAPI.Service.Interface;
using BeerWebAPI.DataAccess.Interface;
using BeerWebAPI.Service.Mapper;
using BeerWebAPI.Shared.Models;
using BeerWebAPI.DataAccess.DbModel;

namespace BeerWebAPI.Service.Services
{
    public class BreweryService : IBreweryService
    {
        private readonly IRepository<BreweryDBModel> _breweryRepository;

        /// <summary>
        /// BreweryService constructor
        /// </summary>
        /// <param name="breweryRepository"></param>
        public BreweryService(IRepository<BreweryDBModel> breweryRepository)
        {
            _breweryRepository = breweryRepository;
        }
        /// <summary>
        /// Add new Brewery details
        /// </summary>
        /// <param name="model"></param>
        /// <returns>bool</returns>
        public bool CreateBrewery(BreweryModel model)
        {
            return _breweryRepository.Add(MapperObject<BreweryModel, BreweryDBModel>.Mapper.Map<BreweryDBModel>(model));
        }

        /// <summary>
        /// Update Brewery by id
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public bool ModifyBrewery(BreweryModel model, int id)
        {
            var item = _breweryRepository.GetById(id);
            if (item != null)
            {
                item.Name = model.Name;
                item.Id = id;
                return _breweryRepository.Update(item);
            }
            return false;
        }

        /// <summary>
        /// Get all brewery Details
        /// </summary>
        /// <returns>List<BreweryModel></returns>
        public List<BreweryModel>? GetAllBreweries()
        {
            var items = _breweryRepository.GetAll();
            if (items != null)
                return MapperObject<BreweryDBModel, BreweryModel>.Mapper.Map<List<BreweryModel>>(items);
            return null;
        }
        /// <summary>
        /// Get brewery details brewery Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BreweryModel</returns>
        public BreweryModel? GetBreweryDetailsById(int id)
        {
            var item = _breweryRepository.GetById(id);
            if (item != null)
                return MapperObject<BreweryDBModel, BreweryModel>.Mapper.Map<BreweryModel>(item);
            return null;
        }
    }
}
