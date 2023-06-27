using BeerWebAPI.Service.Interface;
using BeerWebAPI.DataAccess.Interface;
using BeerWebAPI.Service.Mapper;
using BeerWebAPI.Shared.Models;
using BeerWebAPI.DataAccess.DbModel;

namespace BeerWebAPI.Service.Services
{
    public class BarService : IBarService
    {
        private readonly IRepository<BarDBModel> _barRepository;
        public BarService(IRepository<BarDBModel> barRepository)
        {
            _barRepository = barRepository;
        }

        /// <summary>
        ///  Add new Bar details
        /// </summary>
        /// <param name="model"></param>
        /// <returns>bool</returns>
        public bool CreateBar(BarModel model)
        {
            var barModel = MapperObject<BarModel, BarDBModel>.Mapper.Map<BarDBModel>(model);
            return _barRepository.Add(barModel);
        }

        /// <summary>
        /// Get all bar details
        /// </summary>
        /// <returns>List<BarModel></returns>
        public List<BarModel>? GetAllBars()
        {
            List<BarDBModel> listBar = _barRepository.GetAll();
            if (listBar != null)
                return MapperObject<BarDBModel, BarModel>.Mapper.Map<List<BarModel>>(listBar);
            return null;
        }

        /// <summary>
        /// Get Bar detail by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BarModel</returns>
        public BarModel? GetBarDetailsById(int id)
        {
            var item = _barRepository.GetById(id);
            if (item != null)
                return MapperObject<BarDBModel, BarModel>.Mapper.Map<BarModel>(item);
            return null;
        }

        /// <summary>
        /// Update bar details by Bar id
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public bool ModifyBar(BarModel model, int id)
        {
            var item = _barRepository.GetById(id);
            if (item != null)
            {
                item.Address = model.Address;
                item.Name = model.Name;
                item.Id = id;
                return _barRepository.Update(item);
            }
            return false;
        }
    }
}