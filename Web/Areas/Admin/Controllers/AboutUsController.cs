using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.AboutUs;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AboutUsController : Controller
    {
        private readonly IAboutUsService _aboutUsService;

        public AboutUsController(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _aboutUsService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AboutUsCreateVM model)
        {
            var isSucceeded = await _aboutUsService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            await _aboutUsService.CreateAsync(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            var model = await _aboutUsService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Update(int id, AboutUsUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _aboutUsService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _aboutUsService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _aboutUsService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> DeletePhoto(int id, int aboutUsId)
        {
            var isSucceded = await _aboutUsService.DeletePhotoAsync(id);
            if (isSucceded) return RedirectToAction("update", "aboutus", new { id = aboutUsId});

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePhoto(int id)
        {
            var model = await _aboutUsService.GetPhotoUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhoto(int id, AboutUsPhotoUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _aboutUsService.UpdatePhotoAsync(model);
            if (isSucceeded) return RedirectToAction("update", "aboutus", new { id = model.AboutUsId });

            return View(model);
        }
    }
}
