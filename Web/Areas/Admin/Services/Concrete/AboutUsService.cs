using Core.Entities;
using Core.Utilities.Abstract;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.AboutUs;

namespace Web.Areas.Admin.Services.Concrete
{
    public class AboutUsService : IAboutUsService
    {
        private readonly IAboutUsRepository _aboutUsRepository;
        private readonly IAboutUsPhotoRepository _aboutUsPhotoRepository;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ModelStateDictionary _modelState;
        public AboutUsService(IAboutUsRepository aboutUsRepository, IAboutUsPhotoRepository aboutUsPhotoRepository,
                                IActionContextAccessor actionContextAccessor,
                                IFileService fileService,
                                IWebHostEnvironment webHostEnvironment)
        {
            _aboutUsRepository = aboutUsRepository;
            _aboutUsPhotoRepository = aboutUsPhotoRepository;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> CreateAsync(AboutUsCreateVM model)
        {
            if (!_modelState.IsValid) return false;


            bool hasError = false;
            foreach (var photo in model.AboutUsPhotos)
            {
                if (!_fileService.IsImage(photo))
                {
                    _modelState.AddModelError("AboutUsPhotos", $"{photo.FileName} yuklediyiniz file sekil formatinda olmalidir");
                    hasError = true;

                }
                else if (!_fileService.CheckSize(photo, 300))
                {
                    _modelState.AddModelError("AboutUsPhotos", $"{photo.FileName} ci yuklediyiniz sekil 300 kb dan az olmalidir");
                    hasError = true;

                }
            }

            if (!_fileService.IsImage(model.Icon))
            {
                _modelState.AddModelError("AboutUsPhotos", $"{model.Icon.FileName} yuklediyiniz icon sekil formatinda olmalidir");
                hasError = true;

            }

            if (hasError) { return false; }



            var aboutUs = new AboutUs
            {
                Title = model.Title,
                Slogan = model.Slogan,
                Description = model.Description,
                IconName = await _fileService.UploadAsync(model.Icon),
                CreatedAt = DateTime.Now,
            };

            await _aboutUsRepository.CreateAsync(aboutUs);

            int order = 1;
            foreach (var photo in model.AboutUsPhotos)
            {
                var aboutUsPhoto = new AboutUsPhoto
                {
                    PhotoName = await _fileService.UploadAsync(photo),
                    AboutUsId = aboutUs.Id,
                    Order = order
                    
                };
                await _aboutUsPhotoRepository.CreateAsync(aboutUsPhoto);
                order++;
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _aboutUsRepository.GetAsync(id);
            var productPhotos = await _aboutUsPhotoRepository.GetAllAsync();
            if (product != null)
            {
                foreach (var photo in productPhotos)
                {
                    _fileService.Delete(photo.PhotoName);
                    await _aboutUsPhotoRepository.DeleteAsync(photo);
                }
                await _aboutUsRepository.DeleteAsync(product);
                return true;
            }

            return false;
        }

        public async Task<bool> DeletePhotoAsync(int id)
        {
            var aboutUsPhoto = await _aboutUsPhotoRepository.GetAsync(id);
            if (aboutUsPhoto != null)
            {
                _fileService.Delete(aboutUsPhoto.PhotoName);

                await _aboutUsPhotoRepository.DeleteAsync(aboutUsPhoto);

                return true;

            }

            return false;
        }

        public async Task<AboutUsIndexVM> GetAsync()
        {
            var model = new AboutUsIndexVM
            {
                AboutUs = await _aboutUsRepository.GetAsync()
            };
            return model;
        }

        public async Task<AboutUsUpdateVM> GetUpdateModelAsync(int id)
        {
            var aboutUs = await _aboutUsRepository.GetAsync(id);

            if (aboutUs == null) return null;

            var model = new AboutUsUpdateVM
            {
                Id = aboutUs.Id,
                Slogan = aboutUs.Slogan,
                Description = aboutUs.Description,
                Title = aboutUs.Title,
                IconName = aboutUs.IconName,
                AboutUsPhotos = await _aboutUsPhotoRepository.GetAllAsync()
            };

            return model;
        }

        public async Task<bool> UpdateAsync(AboutUsUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var aboutUs = await _aboutUsRepository.GetWithPhotosAsync();
            bool hasError = false;


            foreach (var photo in model.Photos)
            {
                if (!_fileService.IsImage(photo))
                {
                    _modelState.AddModelError("AboutUsPhotos", $"{photo.FileName} yuklediyiniz file sekil formatinda olmalidir");
                    hasError = true;

                }
                else if (!_fileService.CheckSize(photo, 300))
                {
                    _modelState.AddModelError("AboutUsPhotos", $"{photo.FileName} ci yuklediyiniz sekil 300 kb dan az olmalidir");
                    hasError = true;

                }
            }

            if (model.Icon != null)
            {
                if (!_fileService.IsImage(model.Icon))
                {
                    _modelState.AddModelError("AboutUsPhotos", $"{model.Icon.FileName} yuklediyiniz icon sekil formatinda olmalidir");
                    hasError = true;
                }
            }

            if (hasError) { return false; }


            
            int order = aboutUs.AboutUsPhotos.Count > 0 ?  aboutUs.AboutUsPhotos.OrderByDescending(pp => pp.Order).FirstOrDefault().Order : 1;
            foreach (var photo in model.Photos)
            {
                if(photo != null)
                {
                    var productPhoto = new AboutUsPhoto
                    {
                        PhotoName = await _fileService.UploadAsync(photo),
                        Order = order++,
                        AboutUsId = aboutUs.Id
                    };
                    await _aboutUsPhotoRepository.CreateAsync(productPhoto);
                }
                
            }


            if (aboutUs != null)
            {
                aboutUs.Title = model.Title;
                aboutUs.ModifiedAt = DateTime.Now;
                aboutUs.Description = model.Description;
                aboutUs.IconName =   model.Icon != null ? await _fileService.UploadAsync(model.Icon) : aboutUs.IconName;
                aboutUs.Slogan = model.Slogan;
                await _aboutUsRepository.UpdateAsync(aboutUs);
            }

                
            return true;
        }

        public async Task<AboutUsPhotoUpdateVM> GetPhotoUpdateModelAsync(int id)
        {



            var productPhoto = await _aboutUsPhotoRepository.GetAsync(id);

            if (productPhoto == null) return null;

            var model = new AboutUsPhotoUpdateVM
            {
                Id = id,
                Order = productPhoto.Order,
                AboutUsId = productPhoto.AboutUsId
            };

            return model;

        }


        public async Task<bool> UpdatePhotoAsync(AboutUsPhotoUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var aboutUsPhoto = await _aboutUsPhotoRepository.GetAsync(model.Id);

            if (aboutUsPhoto != null)
            {
                aboutUsPhoto.Order = model.Order;

                await _aboutUsPhotoRepository.UpdateAsync(aboutUsPhoto);
            }
            return true;
        }
    }
}
