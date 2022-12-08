using Web.ViewModels;

namespace Web.Services.Abstract
{
    public interface IDoctorService
    {
        Task<DoctorIndexVM> GetAllAsync(DoctorIndexVM model);
    }
}
