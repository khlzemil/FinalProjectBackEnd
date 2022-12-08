using Core.Entities;
using Core.Utilities.Abstract;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.CoverVideo;

namespace Web.Areas.Admin.Services.Concrete
{
    public class CoverVideoService : ICoverVideoService
    {
        private readonly ICoverVideoRepository _coverVideoRepository;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ModelStateDictionary _modelState;
        public CoverVideoService(ICoverVideoRepository coverVideoRepository ,
                                IActionContextAccessor actionContextAccessor,
                                IFileService fileService,
                                IWebHostEnvironment webHostEnvironment)
        {
            _coverVideoRepository = coverVideoRepository;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> CreateAsync(CoverVideoCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            bool hasError = false;

            if (!_fileService.IsImage(model.CoverPhoto))
            {
                _modelState.AddModelError("CoverPhoto", $"{model.CoverPhoto.FileName} yuklediyiniz fayl sekil formatinda olmalidir");
                hasError = true;
            }

            if (hasError) { return false; }


            var coverVideo = new CoverVideo
            {
                Url = model.Url,
                CoverImageName = await _fileService.UploadAsync(model.CoverPhoto),
                CreatedAt = DateTime.Now,
            };

            await _coverVideoRepository.CreateAsync(coverVideo);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var coverVideo = await _coverVideoRepository.GetAsync(id);
            if (coverVideo != null)
            {
                await _coverVideoRepository.DeleteAsync(coverVideo);
                return true;
            }

            return false;
        }

        public async Task<CoverVideoIndexVM> GetAsync()
        {
            var model = new CoverVideoIndexVM
            {
                CoverVideo = await _coverVideoRepository.GetAsync()
            };
            return model;
        }

        public async Task<CoverVideoUpdateVM> GetUpdateModelAsync(int id)
        {
            var coverVideo = await _coverVideoRepository.GetAsync(id);

            if (coverVideo == null) return null;

            var model = new CoverVideoUpdateVM
            {
                Id = coverVideo.Id,
                Url = coverVideo.Url,
                CoverImageName = coverVideo.CoverImageName
            };

            return model;
        }

        public async Task<bool> UpdateAsync(CoverVideoUpdateVM model)
        {
            if (!_modelState.IsValid) return false;


            var coverVideo = await _coverVideoRepository.GetAsync(model.Id);

            bool hasError = false;


            if (model.CoverPhoto != null)
            {
                if (!_fileService.IsImage(model.CoverPhoto))
                {
                    _modelState.AddModelError("CoverPhoto", $"{model.CoverPhoto.FileName} yuklediyiniz icon sekil formatinda olmalidir");
                    hasError = true;
                }
            }

            if (hasError) { return false; }


            if (coverVideo != null)
            {
                coverVideo.ModifiedAt = DateTime.Now;
                coverVideo.Url = model.Url;
                coverVideo.CoverImageName = model.CoverPhoto != null ? await _fileService.UploadAsync(model.CoverPhoto) : coverVideo.CoverImageName;
                await _coverVideoRepository.UpdateAsync(coverVideo);
            }
            return true;
        }
    }
}
