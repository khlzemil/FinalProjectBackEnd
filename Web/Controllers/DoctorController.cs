using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService )
        {

            _doctorService = doctorService;
        }

        public async Task<IActionResult> Find(DoctorIndexVM model)
        {
            model = await _doctorService.GetAllAsync(model);
            if (model == null) return NotFound();

            return View(model);

        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}
