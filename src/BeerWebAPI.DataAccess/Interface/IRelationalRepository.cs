namespace BeerWebAPI.DataAccess.Interface
{
    /// <summary>
    /// IRelationalRepository<T1,T2> generic interface that contain the common methods
    /// </summary>
    public interface IRelationalRepository<T1,T2>
    {
        bool Add(T1 model);
        T2?  GetById(int id);
        List<T2>? GetAll();
    }
}