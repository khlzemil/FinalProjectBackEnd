using Web.Areas.Admin.ViewModels.News;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface INewsService
    {
        Task<NewsIndexVM> GetAllAsync();

        Task<bool> CreateAsync(NewsCreateVM model);
        Task<NewsUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(NewsUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
