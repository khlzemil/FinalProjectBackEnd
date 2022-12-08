using Web.Areas.Admin.ViewModels.Statistic;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IStatisticService
    {
        Task<StatisticIndexVM> GetAllAsync();
        Task<bool> CreateAsync(StatisticCreateVM model);
        Task<StatisticUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(StatisticUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
