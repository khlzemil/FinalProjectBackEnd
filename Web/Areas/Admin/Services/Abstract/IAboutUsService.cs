using Web.Areas.Admin.ViewModels.AboutUs;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IAboutUsService
    {
        Task<AboutUsIndexVM> GetAsync();
        Task<bool> CreateAsync(AboutUsCreateVM model);
        Task<AboutUsUpdateVM> GetUpdateModelAsync(int id);

        Task<bool> UpdateAsync(AboutUsUpdateVM model);

        Task<bool> DeleteAsync(int id);
        Task<bool> DeletePhotoAsync(int id);

        Task<AboutUsPhotoUpdateVM> GetPhotoUpdateModelAsync(int id);
        Task<bool> UpdatePhotoAsync(AboutUsPhotoUpdateVM model);


    }
}
