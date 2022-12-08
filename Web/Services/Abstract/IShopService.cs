using Core.Entities;
using Web.ViewModels.Product;

namespace Web.Services.Abstract
{
    public interface IShopService
    {
        Task<ProductIndexVM> GetAllAsync(ProductIndexVM model);
        Task<ProductFilterIndexVM> CategoryProductAsync(int id);

        IQueryable<Product> FilterProducts(ProductIndexVM model);
    }
}
