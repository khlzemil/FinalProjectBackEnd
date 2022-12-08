using Web.Areas.Admin.ViewModels.CoverVideo;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface ICoverVideoService
    {
        Task<CoverVideoIndexVM> GetAsync();

        Task<bool> CreateAsync(CoverVideoCreateVM model);
        Task<CoverVideoUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(CoverVideoUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
