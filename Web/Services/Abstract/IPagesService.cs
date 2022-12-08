using Web.ViewModels;

namespace Web.Services.Abstract
{
    public interface IPagesService
    {
        Task<PagesIndexVM> GetAllAsync();
    }
}
