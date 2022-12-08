using Core.Entities;
using Core.Utilities.Abstract;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Statistic;

namespace Web.Areas.Admin.Services.Concrete
{
    public class StatisticService : IStatisticService
    {
        private readonly IStatisticRepository _statisticRepository;
        private readonly ModelStateDictionary _modelState;
        public StatisticService(IStatisticRepository statisticRepository, IActionContextAccessor actionContextAccessor)
        {
            _statisticRepository = statisticRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<StatisticIndexVM> GetAllAsync()
        {
            var model = new StatisticIndexVM
            {
                Statistics = await _statisticRepository.GetAllAsync()
            };
            return model;
        }


        public async Task<bool> CreateAsync(StatisticCreateVM model)
        {
            if (!_modelState.IsValid) return false;



            var isExist = await _statisticRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());

            if (isExist)
            {
                _modelState.AddModelError("Element", "Bu adda Kontent yaradila bilmez");
                return false;
            }


            var statistic = new Statistic
            {
                Count = model.Count,
                Title = model.Title,
                CreatedAt = DateTime.Now,
            };

            await _statisticRepository.CreateAsync(statistic);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var whyChoose = await _statisticRepository.GetAsync(id);
            if (whyChoose != null)
            {
                await _statisticRepository.DeleteAsync(whyChoose);
                return true;
            }

            return false;
        }

        
        public async Task<StatisticUpdateVM> GetUpdateModelAsync(int id)
        {
            var statistic = await _statisticRepository.GetAsync(id);

            if (statistic == null) return null;

            var model = new StatisticUpdateVM
            {
                Id = statistic.Id,
                Count = statistic.Count,
                Title = statistic.Title
            };

            return model;
        }

        public async Task<bool> UpdateAsync(StatisticUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _statisticRepository.AnyAsync(s => s.Title.Trim().ToLower() == model.Title.Trim().ToLower() && model.Id != s.Id);
            if (isExist)
            {
                _modelState.AddModelError("Element", "Bu adda Kontent mövcuddur");
                return false;
            }

            var statistic = await _statisticRepository.GetAsync(model.Id);

            if (statistic != null)
            {
                statistic.Title = model.Title;
                statistic.Count = model.Count;
                await _statisticRepository.UpdateAsync(statistic);
            }
            return true;
        }
    }
}
