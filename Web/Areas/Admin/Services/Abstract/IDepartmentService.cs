using Web.Areas.Admin.ViewModels.Department;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IDepartmentService
    {
        Task<DepartmentIndexVM> GetAllAsync();
        Task<bool> CreateAsync(DepartmentCreateVM model);
        Task<DepartmentUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(DepartmentUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
