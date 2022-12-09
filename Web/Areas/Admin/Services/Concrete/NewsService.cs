using Core.Entities;
using Core.Utilities.Abstract;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.News;

namespace Web.Areas.Admin.Services.Concrete
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public NewsService(INewsRepository newsRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _newsRepository = newsRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<NewsIndexVM> GetAllAsync()
        {
            var model = new NewsIndexVM
            {
                News = await _newsRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(NewsCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _newsRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda news mövcuddur");
                return false;
            }

            if (!_fileService.IsImage(model.MainPhoto))
            {
                _modelState.AddModelError("MainPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                return false;
            }
            if (!_fileService.CheckSize(model.MainPhoto, 300))
            {
                _modelState.AddModelError("MainPhoto", "File olcusu 300 kbdan boyukdur");
                return false;
            }



            var news = new News
            {

                Title = model.Title,
                Text = model.Text,
                Time = DateTime.Now,
                CreatedAt = DateTime.Now,

                PhotoName = await _fileService.UploadAsync(model.MainPhoto),
            };

            await _newsRepository.CreateAsync(news);
            return true;
        }

        public async Task<NewsUpdateVM> GetUpdateModelAsync(int id)
        {


            var news = await _newsRepository.GetAsync(id);

            if (news == null) return null;

            var model = new NewsUpdateVM
            {
                Id = news.Id,
                Title = news.Title,
                Text = news.Text,
                MainPhotoName = news.PhotoName,
                Time = news.Time,
            };

            return model;

        }

        public async Task<bool> UpdateAsync(NewsUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _newsRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda news mövcuddur");
                return false;
            }
            if (model.MainPhoto != null)
            {
                if (!_fileService.IsImage(model.MainPhoto))
                {
                    _modelState.AddModelError("MainPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.MainPhoto, 300))
                {
                    _modelState.AddModelError("MainPhoto", "File olcusu 300 kbdan boyukdur");
                    return false;
                }
            }

            var news = await _newsRepository.GetAsync(model.Id);




            if (news != null)
            {
                news.Id = model.Id;
                news.Title = model.Title;
                news.ModifiedAt = DateTime.Now;
                news.Text = model.Text;
                news.Time = model.Time;


                if (model.MainPhoto != null)
                {
                    news.PhotoName = await _fileService.UploadAsync(model.MainPhoto);
                }

                await _newsRepository.UpdateAsync(news);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var news = await _newsRepository.GetAsync(id);
            if (news != null)
            {
                _fileService.Delete(news.PhotoName);
                await _newsRepository.DeleteAsync(news);

                return true;

            }

            return false;
        }
    }
}
