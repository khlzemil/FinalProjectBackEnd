using Core.Entities;
using Core.Utilities.Abstract;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.OurVision;

namespace Web.Areas.Admin.Services.Concrete
{
    public class OurVisionService : IOurVisionService
    {
        private readonly IOurVisionRepository _ourVisionRepository;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ModelStateDictionary _modelState;
        public OurVisionService(IOurVisionRepository ourVisionRepository,
                                IActionContextAccessor actionContextAccessor,
                                IFileService fileService,
                                IWebHostEnvironment webHostEnvironment)
        {
            _ourVisionRepository = ourVisionRepository;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> CreateAsync(OurVisionCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _ourVisionRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda kontent mövcuddur");
                return false;
            }

            bool hasError = false;

            if (!_fileService.IsImage(model.Icon))
            {
                _modelState.AddModelError("Icon", $"{model.Icon.FileName} yuklediyiniz icon sekil formatinda olmalidir");
                hasError = true;
            }

            if (hasError) { return false; }



            var ourVision = new OurVision
            {
                Title = model.Title,
                Description = model.Description,
                IconName = await _fileService.UploadAsync(model.Icon),
                CreatedAt = DateTime.Now,
            };

            await _ourVisionRepository.CreateAsync(ourVision);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ourVision = await _ourVisionRepository.GetAsync(id);
            if (ourVision != null)
            {
                await _ourVisionRepository.DeleteAsync(ourVision);
                return true;
            }

            return false;
        }

        public async Task<OurVisionIndexVM> GetAllAsync()
        {
            var model = new OurVisionIndexVM
            {
                OurVisions = await _ourVisionRepository.GetAllAsync()
            };
            return model;
        }
        public async Task<OurVisionUpdateVM> GetUpdateModelAsync(int id)
        {
            var aboutUs = await _ourVisionRepository.GetAsync(id);

            if (aboutUs == null) return null;

            var model = new OurVisionUpdateVM
            {
                Id = aboutUs.Id,
                Description = aboutUs.Description,
                Title = aboutUs.Title,
                IconName = aboutUs.IconName,
            };

            return model;
        }
        public async Task<bool> UpdateAsync(OurVisionUpdateVM model)
        {
            if (!_modelState.IsValid) return false;


            var isExist = await _ourVisionRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda kontent mövcuddur");
                return false;
            }

            var ourVision = await _ourVisionRepository.GetAsync(model.Id);

            bool hasError = false;


            if (model.Icon != null)
            {
                if (!_fileService.IsImage(model.Icon))
                {
                    _modelState.AddModelError("Icon", $"{model.Icon.FileName} yuklediyiniz icon sekil formatinda olmalidir");
                    hasError = true;
                }
            }

            if (hasError) { return false; }


            if (ourVision != null)
            {
                ourVision.Title = model.Title;
                ourVision.ModifiedAt = DateTime.Now;
                ourVision.IconName = model.Icon != null ? await _fileService.UploadAsync(model.Icon) : ourVision.IconName;
                ourVision.Description = model.Description;
                await _ourVisionRepository.UpdateAsync(ourVision);

            }
            return true;
        }

        
    }
}
