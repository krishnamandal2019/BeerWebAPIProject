using BeerWebAPI.DataAccess.DatabaseContext;
using BeerWebAPI.DataAccess.DbModel;
using BeerWebAPI.DataAccess.Interface;
using BeerWebAPI.Shared.Models;

namespace BeerWebAPI.DataAccess.Repository
{
    /// <summary>
    /// BarBeerRepository that has the implementation of IRelationalRepository
    /// </summary>
    public class BarBeerRepository : IRelationalRepository<BarBeerDBModel, BarBeerResponseModel>
    {
        private readonly AppDBContext _appDBContext;
        public BarBeerRepository(AppDBContext appDbContext)
        {
            this._appDBContext = appDbContext;
        }

        /// <summary>
        /// Save new beer to a Bar
        /// </summary>
        /// <param name="model"></param>
        /// <returns>bool</returns>
        public bool Add(BarBeerDBModel model)
        {
            _appDBContext.DbBarBeerModels.Add(model);
            return _appDBContext.SaveChanges() == 1;
        }

        /// <summary>
        /// Get all bars with served beers from DB
        /// </summary>
        /// <returns>List<BarBeerResponseModel></returns>
        public List<BarBeerResponseModel> GetAll()
        {
            return (from dbBarBeerModel in _appDBContext.DbBarBeerModels
                    join dbBeerModel in _appDBContext.DbBeerModels on dbBarBeerModel.BeerId equals dbBeerModel.Id
                    join dbBarModel in _appDBContext.DbBarModels on dbBarBeerModel.BarId equals dbBarModel.Id
                    group dbBarBeerModel by dbBarBeerModel.BarId into dbBarBeerModelGroup
                    select new BarBeerResponseModel()
                    {
                        BarId = dbBarBeerModelGroup.Key,
                        BarName = _appDBContext.DbBarModels.FirstOrDefault(b => b.Id == dbBarBeerModelGroup.Key).Name,
                        Beers = (from beer in _appDBContext.DbBeerModels
                                 join barbeer in _appDBContext.DbBarBeerModels on beer.Id equals barbeer.BeerId
                                 where barbeer.BarId == dbBarBeerModelGroup.Key
                                 select new BeerModel
                                 {
                                     Id = beer.Id,
                                     Name = beer.Name,
                                     PercentageAlcoholByVolume = beer.PercentageAlcoholByVolume
                                 }).ToList()
                    }).ToList();
        }

        /// <summary>
        /// Get specific bar with served beers by bar Id
        /// </summary>
        /// <param name="barId"></param>
        /// <returns>BarBeerResponseModel</returns>
        public BarBeerResponseModel? GetById(int barId)
        {

            return (from dbBarBeerModel in _appDBContext.DbBarBeerModels
                    join dbBeerModel in _appDBContext.DbBeerModels on dbBarBeerModel.BeerId equals dbBeerModel.Id
                    join dbBarModel in _appDBContext.DbBarModels on dbBarBeerModel.BarId equals dbBarModel.Id
                    where barId == dbBarBeerModel.BarId
                    select new BarBeerResponseModel
                    {
                        BarId = barId,
                        BarName = dbBarModel.Name,
                        Beers = (from beer in _appDBContext.DbBeerModels
                                 join barbeer in _appDBContext.DbBarBeerModels on beer.Id equals barbeer.BeerId
                                 where barbeer.BarId == barId
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