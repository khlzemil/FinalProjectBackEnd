using Web.ViewModels;

namespace Web.Services.Abstract
{
    public interface IDepartmentService
    {
        Task<DepartmentIndexVM> GetAllAsync();
    }
}
