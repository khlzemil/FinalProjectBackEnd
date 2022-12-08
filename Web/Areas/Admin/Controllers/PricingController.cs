using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Pricing;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PricingController : Controller
    {
        private readonly IPricingService _pricingService;

        public PricingController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _pricingService.GetAllAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PricingCreateVM model)
        {
            var isSucceeded = await _pricingService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            await _pricingService.CreateAsync(model);
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _pricingService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Update(int id, PricingUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _pricingService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _pricingService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _pricingService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
