using Web.Areas.Admin.ViewModels.Doctor;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IDoctorService
    {
        public Task<DoctorIndexVM> GetAllAsync();
        Task<bool> CreateAsync(DoctorCreateVM model);

        Task<DoctorUpdateVM> GetUpdateModelAsync(int id);

        Task<bool> UpdateAsync(DoctorUpdateVM model);

        Task<bool> DeleteAsync(int id);
    }
}
