using BeerWebAPI.DataAccess.DatabaseContext;
using BeerWebAPI.DataAccess.DbModel;
using BeerWebAPI.DataAccess.Interface;
using BeerWebAPI.Shared.Models;

namespace BeerWebAPI.DataAccess.Repository
{
    /// <summary>
    /// BreweryBeer repository that has the implementation of IRelationalRepository
    /// </summary>
    public class BreweryBeerRepository : IRelationalRepository<BreweryBeerDBModel, BreweryBeerResponseModel>
    {
        private readonly AppDBContext _appDBContext;
        public BreweryBeerRepository(AppDBContext appDbContext)
        {
            this._appDBContext = appDbContext;
        }

        /// <summary>
        ///  Add new beer to a Berwery
        /// </summary>
        /// <param name="model"></param>
        /// <returns>bool</returns>
        public bool Add(BreweryBeerDBModel model)
        {
            _appDBContext.DbBreweryBeerModels.Add(model);
            return _appDBContext.SaveChanges() == 1;
        }

        /// <summary>
        /// Get all breweries with served beers
        /// </summary>
        /// <returns>List<BreweryBeerResponseModel></returns>
        public List<BreweryBeerResponseModel> GetAll()
        {
            return (from dbBreweryBeerModel in _appDBContext.DbBreweryBeerModels
                    join dbBeerModel in _appDBContext.DbBeerModels on dbBreweryBeerModel.BeerId equals dbBeerModel.Id
                    join dbBreweryModel in _appDBContext.DbBrewery on dbBreweryBeerModel.BreweryId equals dbBreweryModel.Id
                    group dbBreweryBeerModel by dbBreweryBeerModel.BreweryId into dbBreweryBeerModelGroup
                    select new BreweryBeerResponseModel()
                    {
                        BreweryId = dbBreweryBeerModelGroup.Key,
                        BreweryName = _appDBContext.DbBrewery.FirstOrDefault(b => b.Id == dbBreweryBeerModelGroup.Key).Name,
                        Beers = (from beer in _appDBContext.DbBeerModels
                                 join brewerybeer in _appDBContext.DbBreweryBeerModels on beer.Id equals brewerybeer.BeerId
                                 where brewerybeer.BreweryId == dbBreweryBeerModelGroup.Key
                                 select new BeerModel
                                 {
                                     Id = beer.Id,
                                     Name = beer.Name,
                                     PercentageAlcoholByVolume = beer.PercentageAlcoholByVolume
                                 }).ToList()
                    }).ToList();
        }

        /// <summary>
        /// Get brewery with served beers by brewery Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BreweryBeerResponseModel</returns>
        public BreweryBeerResponseModel? GetById(int id)
        {
            return (from dbBreweryBeerModel in _appDBContext.DbBreweryBeerModels
                    join dbBeerModel in _appDBContext.DbBeerModels on dbBreweryBeerModel.BeerId equals dbBeerModel.Id
                    join dbBreweryModel in _appDBContext.DbBrewery on dbBreweryBeerModel.BreweryId equals dbBreweryModel.Id
                    where dbBreweryBeerModel.BreweryId == id
                    select new BreweryBeerResponseModel()
                    {
                        BreweryId = id,
                        BreweryName = _appDBContext.DbBrewery.FirstOrDefault(b => b.Id == id).Name,
                        Beers = (from beer in _appDBContext.DbBeerModels
                                 join brewerybeer in _appDBContext.DbBreweryBeerModels on beer.Id equals brewerybeer.BeerId
                                 where brewerybeer.BreweryId == id
                                 select new BeerModel
                                 {
                                     Id = beer.Id,
                                     Name = beer.Name,
                                     PercentageAlcoholByVolume = beer.PercentageAlcoholByVolume
                                 }).ToList()
                    }).FirstOrDefault();
        }
    }
}