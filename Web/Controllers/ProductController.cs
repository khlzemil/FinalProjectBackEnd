using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels.Product;

namespace Web.Controllers
{
    public class ProductController : Controller
    {

        private readonly IShopService _shopService;

        public ProductController(IShopService shopService)
        {

            _shopService = shopService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(ProductIndexVM model)
        {
            model = await _shopService.GetAllAsync(model);
            return View(model);
        }

        public async Task<IActionResult> CategoryProduct(int id)
        {
            var model = await _shopService.CategoryProductAsync(id);

            return PartialView("_ProductPartial", model);

        }
    }
}
