namespace BeerWebAPI.DataAccess.DbModel
{
    /// <summary>
    /// Model for table Bar
    /// </summary>
    public class BarDBModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}
