using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetAllGetCategoryAsync();
        Task<List<Product>> GetByCategoryIdAsync(int categoryId);

        IQueryable<Product> FilterByTitle(string title);
        Task<Product> GetProduct(int productId);
    }
}
