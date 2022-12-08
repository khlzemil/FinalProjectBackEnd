using Web.Areas.Admin.ViewModels.OurVision;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IOurVisionService
    {
        Task<OurVisionIndexVM> GetAllAsync();
        Task<bool> CreateAsync(OurVisionCreateVM model);
        Task<OurVisionUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(OurVisionUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
