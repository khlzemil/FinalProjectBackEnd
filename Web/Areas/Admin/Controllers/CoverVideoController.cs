﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.CoverVideo;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CoverVideoController : Controller
    {
        private readonly ICoverVideoService _coverVideoService;

        public CoverVideoController(ICoverVideoService coverVideoService)
        {
            _coverVideoService = coverVideoService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _coverVideoService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CoverVideoCreateVM model)
        {
            var isSucceeded = await _coverVideoService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            await _coverVideoService.CreateAsync(model);
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _coverVideoService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _coverVideoService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Update(int id, CoverVideoUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _coverVideoService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _coverVideoService.GetUpdateModelAsync(id);
            return View(model);
        }

    }
}
