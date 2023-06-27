using BeerWebAPI.DataAccess.Interface;
using BeerWebAPI.DataAccess.DbModel;
using BeerWebAPI.DataAccess.DatabaseContext;

namespace BeerWebAPI.DataAccess.Repository
{
    /// <summary>
    /// BeerRepository that has the implementation of  IBeerRepository,IRepository
    /// </summary>
    public class BeerRepository : IBeerRepository, IRepository<BeerDBModel>
    {
        private readonly AppDBContext _appDBContext;

        public BeerRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        /// <summary>
        /// Add new beer into beer table
        /// </summary>
        /// <param name="beer"></param>
        /// <returns>bool</returns>
        public bool Add(BeerDBModel beer)
        {
            _appDBContext.DbBeerModels.Add(beer);
            return _appDBContext.SaveChanges() == 1;
        }

        /// <summary>
        /// Get all beers form beer table
        /// </summary>
        /// <returns> List<BeerDBModel></returns>
        public List<BeerDBModel> GetAll()
        {
            return (from beer in _appDBContext.DbBeerModels select beer).ToList();
        }

        /// <summary>
        /// Get beer details from beer table based on alcohol parameter
        /// </summary>
        /// <param name="ltAlchPercentage"></param>
        /// <param name="gtAlchPercentage"></param>
        /// <returns>List<BeerDBModel></returns>
        public List<BeerDBModel> GetBeerByAlcoholParameter(decimal ltAlchPercentage, decimal gtAlchPercentage)
        {
            return (from br in _appDBContext.DbBeerModels
                    where br.PercentageAlcoholByVolume < ltAlchPercentage &&
                    br.PercentageAlcoholByVolume > gtAlchPercentage
                    select new BeerDBModel
                    {
                        Name = br.Name,
                        PercentageAlcoholByVolume = br.PercentageAlcoholByVolume,
                        Id = br.Id,
                    }).ToList();
        }

        /// <summary>
        /// Get beer details by beer Id
        /// </summary>
        /// <param name="beerid"></param>
        /// <returns>BeerDBModel</returns>
        public BeerDBModel GetById(int beerid)
        {
            return _appDBContext.DbBeerModels.Find(beerid);
        }

        /// <summary>
        /// Update beer details 
        /// </summary>
        /// <param name="beer"></param>
        /// <returns>bool</returns>
        public bool Update(BeerDBModel beer)
        {
            _appDBContext.DbBeerModels.Update(beer);
            return _appDBContext.SaveChanges() == 1;
        }
    }
}