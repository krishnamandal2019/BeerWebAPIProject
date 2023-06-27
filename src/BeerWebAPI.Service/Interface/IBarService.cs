using BeerWebAPI.Shared.Models;

namespace BeerWebAPI.Service.Interface
{
    /// <summary>
    /// IBarService that contain the required methods.
    /// </summary>
    public interface IBarService
    {
        bool CreateBar(BarModel model);
        bool ModifyBar(BarModel model, int id);
        List<BarModel>? GetAllBars();
        BarModel? GetBarDetailsById(int id);
    }
}
