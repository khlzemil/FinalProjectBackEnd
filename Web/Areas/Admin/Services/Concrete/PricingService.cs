using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Pricing;

namespace Web.Areas.Admin.Services.Concrete
{
    public class PricingService : IPricingService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IPricingRepository _pricingRepository;

        public PricingService(IPricingRepository pricingRepository, IActionContextAccessor actionContextAccessor)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _pricingRepository = pricingRepository;
        }

        public async Task<PricingIndexVM> GetAllAsync()
        {
            var model = new PricingIndexVM
            {
                Pricings = await _pricingRepository.GetAllAsync()
            };
            return model;
        }


        public async Task<bool> CreateAsync(PricingCreateVM model)
        {
            if (!_modelState.IsValid) return false;



            var isExist = await _pricingRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());

            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda Kontent yaradila bilmez");
                return false;
            }


            var pricing = new Pricing
            {
                SubTitle = model.Title,
                Title = model.Title,
                Cost = model.Cost,
                Description = model.Description,
                Type = model.Type,
                CreatedAt = DateTime.Now,
            };

            await _pricingRepository.CreateAsync(pricing);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pricing = await _pricingRepository.GetAsync(id);
            if (pricing != null)
            {
                await _pricingRepository.DeleteAsync(pricing);
                return true;
            }

            return false;
        }


        public async Task<PricingUpdateVM> GetUpdateModelAsync(int id)
        {
            var pricing = await _pricingRepository.GetAsync(id);

            if (pricing == null) return null;

            var model = new PricingUpdateVM
            {
                Id = pricing.Id,
                SubTitle = pricing.SubTitle,
                Title = pricing.Title,
                Cost = pricing.Cost,
                Description = pricing.Description,
                Type = pricing.Type,
            };

            return model;
        }

        public async Task<bool> UpdateAsync(PricingUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _pricingRepository.AnyAsync(s => s.Title.Trim().ToLower() == model.Title.Trim().ToLower() && model.Id != s.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda Kontent mövcuddur");
                return false;
            }

            var statistic = await _pricingRepository.GetAsync(model.Id);

            if (statistic != null)
            {
                statistic.SubTitle = model.SubTitle;
                statistic.Title = model.Title;
                statistic.Cost = model.Cost;
                statistic.Description = model.Description;
                statistic.Type = model.Type;
                statistic.ModifiedAt = DateTime.Now;
                await _pricingRepository.UpdateAsync(statistic);
            }
            return true;
        }
    }
}
