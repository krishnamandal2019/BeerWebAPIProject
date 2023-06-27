namespace BeerWebAPI.DataAccess.Interface
{
    /// <summary>
    /// IRepository<T> generic interface that contain the common methods
    /// </summary>
    /// <typeparam name="T">T is a generic class</typeparam>
    public interface IRepository<T>
    {
        bool Add(T model);
        bool Update(T model);
        List<T> GetAll();
        T GetById(int id);
    }
}