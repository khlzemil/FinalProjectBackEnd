using DataAccess.Repositories.Abstract;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Services.Concrete
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<DoctorIndexVM> GetAllAsync(DoctorIndexVM model)
        {
            var pageCount = await _doctorRepository.GetPageCountAsync(model.Take);


            if (model.Page <= 0 || model.Page > pageCount) return null;

            var doctors = await _doctorRepository.FilterDoctors(model.Name ,model.Page, model.Take);

            model = new DoctorIndexVM
            {
                Doctors = doctors,
                Page = model.Page,
                PageCount = pageCount,
                Take = model.Take,
                Name = model.Name
            };

            return model;
        }





        
    }
}
