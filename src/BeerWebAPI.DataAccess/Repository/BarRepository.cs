using BeerWebAPI.DataAccess.Interface;
using BeerWebAPI.DataAccess.DatabaseContext;
using BeerWebAPI.DataAccess.DbModel;

namespace BeerWebAPI.DataAccess.Repository
{
    /// <summary>
    /// BarRepository that has the implementation of  IRepository
    /// </summary>
    public class BarRepository : IRepository<BarDBModel>
    {
        private readonly AppDBContext _appDBContext;

        public BarRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        /// <summary>
        /// Save new bar into database
        /// </summary>
        /// <param name="bar"></param>
        /// <returns>bool</returns>
        public bool Add(BarDBModel bar)
        {
            _appDBContext.DbBarModels.Add(bar);
            return _appDBContext.SaveChanges() == 1;
        }

        /// <summary>
        /// Get all bars from Bar table
        /// </summary>
        /// <returns>List<BarDBModel></returns>
        public List<BarDBModel> GetAll()
        {
            return (from bar in _appDBContext.DbBarModels select bar).ToList();
        }

        /// <summary>
        /// Get specific bar details by bar id from bar table
        /// </summary>
        /// <param name="barid"></param>
        /// <returns>BarDBModel</returns>
        public BarDBModel GetById(int barId)
        {
            return _appDBContext.DbBarModels.Find(barId);
        }

        /// <summary>
        /// Update bar details by bar id
        /// </summary>
        /// <param name="bar"></param>
        /// <returns>bool</returns>
        public bool Update(BarDBModel bar)
        {
            _appDBContext.DbBarModels.Update(bar);
            return _appDBContext.SaveChanges() == 1;
        }
    }
}
