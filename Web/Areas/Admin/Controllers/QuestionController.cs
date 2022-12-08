﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Question;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _questionService.GetAllAsync();
            return View(model);
        }


        [HttpGet]

        public async Task<IActionResult> Create()
        {
            var model = await _questionService.GetCreateModelAsync();

            return View(model);
        }


        [HttpPost]

        public async Task<IActionResult> Create(QuestionCreateVM model)
        {
            var isSucceeded = await _questionService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            await _questionService.CreateAsync(model);
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var model = await _questionService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Update(int id, QuestionUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _questionService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            model = await _questionService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _questionService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
