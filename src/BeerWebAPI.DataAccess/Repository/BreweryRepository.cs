using BeerWebAPI.DataAccess.Interface;
using BeerWebAPI.DataAccess.DbModel;
using BeerWebAPI.DataAccess.DatabaseContext;

namespace BeerWebAPI.DataAccess.Repository
{
    /// <summary>
    /// Brewery repository that has the implementation of IRepository
    /// </summary>
    public class BreweryRepository : IRepository<BreweryDBModel>
    {
        private readonly AppDBContext _appDBContext;

        public BreweryRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        /// <summary>
        ///  Add new brewery
        /// </summary>
        /// <param name="brewery"></param>
        /// <returns>bool</returns>
        public bool Add(BreweryDBModel brewery)
        {
            _appDBContext.DbBrewery.Add(brewery);
            return _appDBContext.SaveChanges() == 1;
        }

        /// <summary>
        /// Ger all brewery from brewery table
        /// </summary>
        /// <returns>List<BreweryDBModel></returns>
        public List<BreweryDBModel> GetAll()
        {
            return (from brewery in _appDBContext.DbBrewery select brewery).ToList();
        }

        /// <summary>
        /// Get specific brewrey from brewery table based on brewery Id
        /// </summary>
        /// <param name="breweryId"></param>
        /// <returns>BreweryDBModel</returns>
        public BreweryDBModel GetById(int breweryId)
        {
            return _appDBContext.DbBrewery.Find(breweryId);
        }

        /// <summary>
        /// Update brewery details
        /// </summary>
        /// <param name="model"></param>
        /// <returns>bool</returns>
        public bool Update(BreweryDBModel model)
        {
            _appDBContext.DbBrewery.Update(model);
            return _appDBContext.SaveChanges() == 1;
        }
    }
}