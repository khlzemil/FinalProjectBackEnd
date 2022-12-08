using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.QuestionTopic;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuestionTopicController : Controller
    {
        private readonly IQuestionTopicService _questionTopicService;

        public QuestionTopicController(IQuestionTopicService questionTopicService)
        {
            _questionTopicService = questionTopicService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _questionTopicService.GetAllAsync();
            return View(model);
        }


        [HttpGet]

        public async Task<IActionResult> Create()
        {
            return View();

        }

        [HttpPost]

        public async Task<IActionResult> Create(QuestionTopicCreateVM model)
        {

            var isSucceeded = await _questionTopicService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            await _questionTopicService.CreateAsync(model);
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var model = await _questionTopicService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Update(int id, QuestionTopicUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _questionTopicService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _questionTopicService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _questionTopicService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
        
    }
}
