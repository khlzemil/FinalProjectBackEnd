using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Doctor;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService )
        {
            _doctorService = doctorService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _doctorService.GetAllAsync();
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(DoctorCreateVM model)
        {
            var isSucceeded = await _doctorService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            await _doctorService.CreateAsync(model);
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var model = await _doctorService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Update(int id, DoctorUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _doctorService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _doctorService.GetUpdateModelAsync(id);
            return View(model);
        }


        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _doctorService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }


    }
}
