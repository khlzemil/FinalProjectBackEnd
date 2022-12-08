using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Department;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _departmentService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentCreateVM model)
        {
            var isSucceeded = await _departmentService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            await _departmentService.CreateAsync(model);
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _departmentService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Update(int id, DepartmentUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _departmentService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _departmentService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _departmentService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
