using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Statistic;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StatisticController : Controller
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
           _statisticService = statisticService;
        }


        public async Task<IActionResult> Index()
        {
            var model = await _statisticService.GetAllAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StatisticCreateVM model)
        {
            var isSucceeded = await _statisticService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            await _statisticService.CreateAsync(model);
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _statisticService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Update(int id, StatisticUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _statisticService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _statisticService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _statisticService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
