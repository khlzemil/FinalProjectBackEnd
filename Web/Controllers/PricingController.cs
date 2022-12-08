using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;

namespace Web.Controllers
{
    public class PricingController : Controller
    {
        private readonly IPagesService _pagesService;

        public PricingController(IPagesService pagesService )
        {

            _pagesService = pagesService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _pagesService.GetAllAsync();
            return View(model);
        }
    }
}
