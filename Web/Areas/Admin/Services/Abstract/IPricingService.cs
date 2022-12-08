﻿using Web.Areas.Admin.ViewModels.Pricing;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IPricingService
    {
        Task<PricingIndexVM> GetAllAsync();
        Task<bool> CreateAsync(PricingCreateVM model);
        Task<PricingUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(PricingUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
