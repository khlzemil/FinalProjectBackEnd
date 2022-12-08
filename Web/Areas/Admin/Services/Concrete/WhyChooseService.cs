using Core.Entities;
using Core.Utilities.Abstract;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.WhyChoose;

namespace Web.Areas.Admin.Services.Concrete
{
    public class WhyChooseService : IWhyChooseService
    {
        private readonly IWhyChooseRepository _whyChooseRepository;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ModelStateDictionary _modelState;
        public WhyChooseService(IWhyChooseRepository whyChooseRepository,
                                IActionContextAccessor actionContextAccessor,
                                IFileService fileService,
                                IWebHostEnvironment webHostEnvironment)
        {
            _whyChooseRepository = whyChooseRepository;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> CreateAsync(WhyChooseCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            bool hasError = false;

            if (!_fileService.IsImage(model.Photo))
            {
                _modelState.AddModelError("Photo", $"{model.Photo.FileName} yuklediyiniz fayl sekil formatinda olmalidir");
                hasError = true;
            }

            if (hasError) { return false; }


            var whyChoose = new WhyChoose
            {
                Title = model.Title,
                Text = model.Text,
                Description = model.Description,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                CreatedAt = DateTime.Now,
            };

            await _whyChooseRepository.CreateAsync(whyChoose);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var whyChoose = await _whyChooseRepository.GetAsync(id);
            if (whyChoose != null)
            {
                await _whyChooseRepository.DeleteAsync(whyChoose);
                return true;
            }

            return false;
        }

        public async Task<WhyChooseIndexVM> GetAsync()
        {
            var model = new WhyChooseIndexVM
            {
                WhyChoose = await _whyChooseRepository.GetAsync()
            };
            return model;
        }

        public async Task<WhyChooseUpdateVM> GetUpdateModelAsync(int id)
        {
            var whyChoose = await _whyChooseRepository.GetAsync(id);

            if (whyChoose == null) return null;

            var model = new WhyChooseUpdateVM
            {
                Id = whyChoose.Id,
                Title = whyChoose.Title,
                Text = whyChoose.Text,
                Description = whyChoose.Description,
                PhotoName = whyChoose.PhotoName
            };

            return model;
        }

        public async Task<bool> UpdateAsync(WhyChooseUpdateVM model)
        {
            if (!_modelState.IsValid) return false;


            var whyChoose = await _whyChooseRepository.GetAsync(model.Id);

            bool hasError = false;


            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelState.AddModelError("CoverPhoto", $"{model.Photo.FileName} yuklediyiniz icon sekil formatinda olmalidir");
                    hasError = true;
                }
            }

            if (hasError) { return false; }


            if (whyChoose != null)
            {
                whyChoose.Title = model.Title;
                whyChoose.Text = model.Text;
                whyChoose.Description = model.Description;
                whyChoose.ModifiedAt = DateTime.Now;
                whyChoose.PhotoName = model.Photo != null ? await _fileService.UploadAsync(model.Photo) : whyChoose.PhotoName;
                await _whyChooseRepository.UpdateAsync(whyChoose);
            }
            return true;
        }
    }
}
