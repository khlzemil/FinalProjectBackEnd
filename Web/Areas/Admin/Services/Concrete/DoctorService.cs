using Core.Entities;
using Core.Utilities.Abstract;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Doctor;

namespace Web.Areas.Admin.Services.Concrete
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ModelStateDictionary _modelState;
        public DoctorService(IDoctorRepository doctorRepository,
                                IActionContextAccessor actionContextAccessor,
                                IFileService fileService,
                                IWebHostEnvironment webHostEnvironment)
        {
           _doctorRepository = doctorRepository;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<DoctorIndexVM> GetAllAsync()
        {
            var model = new DoctorIndexVM
            {
                Doctors = await _doctorRepository.GetAllAsync()
            };
            return model;
        }

    
        public async Task<bool> CreateAsync(DoctorCreateVM model)
        {
            if (!_modelState.IsValid) return false;


            bool hasError = false;

            if (!_fileService.IsImage(model.Photo))
            {
                _modelState.AddModelError("Photo", $"{model.PhotoName} yuklediyiniz icon sekil formatinda olmalidir");
                hasError = true;
            }

            if (!_fileService.CheckSize(model.Photo, 2000))
            {
                _modelState.AddModelError("Photo", $"{model.PhotoName} yuklediyiniz sekil 2000 kb dan az olmalidir");
                hasError = true;
            }

            if (hasError) { return false; }



            var doctor = new Doctor
            {
                Name = model.Name,
                Surname = model.Surname,
                Profession = model.Profession,
                Bio = model.Bio,
                Skills = model.Skills,
                Summary = model.Summary,
                Email = model.Email,
                SaturdayEnd = model.SaturdayEnd,
                SaturdayStart = model.SaturdayStart,
                IsShowHomePage = model.IsShowHomePage,
                IsWorkingSunday = model.IsWorkingSunday,
                MondayToFridayStartDate = model.MondayToFridayStartDate,
                MondayToFridayEndDate = model.MondayToFridayEndDate,
                Facebook = model.Facebook,
                Linkedin = model.Linkedin,
                Twitter = model.Twitter,
                Phone = model.Phone,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                CreatedAt = DateTime.Now,
            };

            await _doctorRepository.CreateAsync(doctor);

            return true;
        }


        public async Task<DoctorUpdateVM> GetUpdateModelAsync(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);

            if (doctor == null) return null;

            var model = new DoctorUpdateVM
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Surname = doctor.Surname,
                Profession = doctor.Profession,
                Bio = doctor.Bio,
                Skills = doctor.Skills,
                Summary = doctor.Summary,
                Email = doctor.Email,
                SaturdayEnd = doctor.SaturdayEnd,
                SaturdayStart = doctor.SaturdayStart,
                IsShowHomePage = doctor.IsShowHomePage,
                IsWorkingSunday = doctor.IsWorkingSunday,
                MondayToFridayStartDate = doctor.MondayToFridayStartDate,
                MondayToFridayEndDate = doctor.MondayToFridayEndDate,
                Facebook = doctor.Facebook,
                Linkedin = doctor.Linkedin,
                Twitter = doctor.Twitter,
                Phone = doctor.Phone,
                PhotoName = doctor.PhotoName,
            };

            return model;
        }


        public async Task<bool> UpdateAsync(DoctorUpdateVM model)
        {
            if (!_modelState.IsValid) return false;


            var doctor = await _doctorRepository.GetAsync(model.Id);

            bool hasError = false;


            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelState.AddModelError("Photo", $"{model.PhotoName} yuklediyiniz icon sekil formatinda olmalidir");
                    hasError = true;
                }

                if (!_fileService.CheckSize(model.Photo, 2000))
                {
                    _modelState.AddModelError("Photo", $"{model.PhotoName} yuklediyiniz  sekil 2000 kb dan az olmalidir");
                    hasError = true;
                }
            }

            if (hasError) { return false; }


            if (doctor != null)
            {
                doctor.Name = model.Name;
                doctor.Surname = model.Surname;
                doctor.Bio = model.Bio;
                doctor.Skills = model.Skills;
                doctor.Summary = model.Summary;
                doctor.Profession = model.Profession;
                doctor.Email = model.Email;
                doctor.Twitter = model.Twitter;
                doctor.Facebook = model.Facebook;
                doctor.Linkedin = model.Linkedin;
                doctor.IsShowHomePage = model.IsShowHomePage;
                doctor.IsWorkingSunday = model.IsWorkingSunday;
                doctor.SaturdayStart = model.SaturdayStart;
                doctor.SaturdayEnd = model.SaturdayEnd;
                doctor.MondayToFridayEndDate = model.MondayToFridayEndDate;
                doctor.MondayToFridayStartDate = model.MondayToFridayStartDate;
                doctor.SaturdayStart = model.SaturdayStart;
                doctor.SaturdayEnd = model.SaturdayEnd;
                doctor.ModifiedAt = DateTime.Now;
                doctor.PhotoName = model.Photo != null ? await _fileService.UploadAsync(model.Photo) : doctor.PhotoName;
                await _doctorRepository.UpdateAsync(doctor);
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pricing = await _doctorRepository.GetAsync(id);
            if (pricing != null)
            {
                await _doctorRepository.DeleteAsync(pricing);
                return true;
            }

            return false;
        }
    }
}
