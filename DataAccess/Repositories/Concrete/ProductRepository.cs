using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllGetCategoryAsync()
        {
            return await _context.Products.Include(p => p.ProductCategory).ToListAsync();
        }

        public async Task<List<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Products.Where(p => p.ProductCategoryId == categoryId).ToListAsync();
        }


        public IQueryable<Product> FilterByTitle(string title)
        {
            return _context.Products.Where(p => !string.IsNullOrEmpty(title) ? p.Title.Contains(title) : true);
        }

        public async Task<Product> GetProduct(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            return product;
        }
    }
}
