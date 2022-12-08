using Web.Areas.Admin.ViewModels.WhyChoose;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IWhyChooseService
    {
        Task<WhyChooseIndexVM> GetAsync();
        Task<bool> CreateAsync(WhyChooseCreateVM model);
        Task<WhyChooseUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(WhyChooseUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
