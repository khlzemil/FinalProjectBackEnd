using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.WhyChoose;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WhyChooseController : Controller
    {
        private readonly IWhyChooseService _whyChooseService;

        public WhyChooseController(IWhyChooseService whyChooseService)
        {
            _whyChooseService = whyChooseService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _whyChooseService.GetAsync();
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WhyChooseCreateVM model)
        {
            var isSucceeded = await _whyChooseService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            await _whyChooseService.CreateAsync(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _whyChooseService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, WhyChooseUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _whyChooseService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _whyChooseService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _whyChooseService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }


    }
}
