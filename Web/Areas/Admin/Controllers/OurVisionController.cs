using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.OurVision;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OurVisionController : Controller
    {
        private readonly IOurVisionService _ourVisionService;

        public OurVisionController(IOurVisionService ourVisionService)
        {
            _ourVisionService = ourVisionService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _ourVisionService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OurVisionCreateVM model)
        {
            var isSucceeded = await _ourVisionService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            await _ourVisionService.CreateAsync(model);
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _ourVisionService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Update(int id, OurVisionUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _ourVisionService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _ourVisionService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _ourVisionService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}

