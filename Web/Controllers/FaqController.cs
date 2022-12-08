using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;

namespace Web.Controllers
{
    public class FaqController : Controller
    {
        private readonly IFAQService _faqService;

        public FaqController(IFAQService faqService )
        {
            _faqService = faqService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _faqService.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> LoadQuestions(int id)
        {
            var model = await _faqService.LoadQuestionById(id);
            return PartialView("_LoadQuestionsPartial", model);
        }


    }
}
