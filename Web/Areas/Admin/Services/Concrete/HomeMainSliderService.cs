using Core.Entities;
using Core.Utilities.Abstract;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeMainSlider;

namespace Web.Areas.Admin.Services.Concrete
{
    public class HomeMainSliderService : IHomeMainSliderService
    {
        private readonly IHomeMainSliderRepository _homeMainSliderRepository;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ModelStateDictionary _modelState;
        public HomeMainSliderService(IHomeMainSliderRepository homeMainSliderRepository,
                                IActionContextAccessor actionContextAccessor,
                                IFileService fileService,
                                IWebHostEnvironment webHostEnvironment)
        {
            _homeMainSliderRepository = homeMainSliderRepository;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> CreateAsync(HomeMainSliderCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _homeMainSliderRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda slider mövcuddur");
                return false;
            }

            bool hasError = false;

            if (!_fileService.IsImage(model.Photo))
            {
                _modelState.AddModelError("Icon", $"{model.Photo.FileName} yuklediyiniz fayl sekil formatinda olmalidir");
                hasError = true;
            }

            if (hasError) { return false; }

            var countSlider = await _homeMainSliderRepository.GetAllAsync();
            int order = countSlider.Count();

            var homeMainSlider = new HomeMainSlider
            {
                Title = model.Title,
                Description = model.Description,
                LearnMore = model.LearnMore,
                Order = order + 1,
                UrlAdress = model.UrlAdress,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                CreatedAt = DateTime.Now,
            };

            await _homeMainSliderRepository.CreateAsync(homeMainSlider);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var homeMainSlider = await _homeMainSliderRepository.GetAsync(id);
            if (homeMainSlider != null)
            {
                await _homeMainSliderRepository.DeleteAsync(homeMainSlider);
                return true;
            }

            return false;
        }

        public async Task<HomeMainSliderIndexVM> GetAllAsync()
        {
            var model = new HomeMainSliderIndexVM
            {
                HomeMainSliders = await _homeMainSliderRepository.GetAllOrderedAsync()
            };
            return model;
        }

        public async Task<HomeMainSliderUpdateVM> GetUpdateModelAsync(int id)
        {
            var homeMainSlider = await _homeMainSliderRepository.GetAsync(id);

            if (homeMainSlider == null) return null;

            var model = new HomeMainSliderUpdateVM
            {
                Id = homeMainSlider.Id,
                Description = homeMainSlider.Description,
                Title = homeMainSlider.Title,
                Order = homeMainSlider.Order,
                LearnMore = homeMainSlider.LearnMore,
                UrlAdress = homeMainSlider.UrlAdress,
                PhotoName = homeMainSlider.PhotoName
            };

            return model;
        }
        public async Task<bool> UpdateAsync(HomeMainSliderUpdateVM model)
        {
            if (!_modelState.IsValid) return false;


            var isExist = await _homeMainSliderRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda kontent mövcuddur");
                return false;
            }

            var homeMainSlider = await _homeMainSliderRepository.GetAsync(model.Id);

            bool hasError = false;


            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelState.AddModelError("Icon", $"{model.Photo.FileName} yuklediyiniz icon sekil formatinda olmalidir");
                    hasError = true;
                }
            }

            if (hasError) { return false; }


            if (homeMainSlider != null)
            {
                homeMainSlider.Title = model.Title;
                homeMainSlider.ModifiedAt = DateTime.Now;
                homeMainSlider.LearnMore = model.LearnMore;
                homeMainSlider.Order = model.Order;
                homeMainSlider.UrlAdress = model.UrlAdress;
                homeMainSlider.PhotoName = model.Photo != null ? await _fileService.UploadAsync(model.Photo) : homeMainSlider.PhotoName;
                homeMainSlider.Description = model.Description;
                await _homeMainSliderRepository.UpdateAsync(homeMainSlider);
            }
            return true;
        }
    }
}
