﻿using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.ViewModels.Product;

namespace Web.Services.Concrete
{
    public class ShopService : IShopService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ShopService(IProductRepository productRepository, IActionContextAccessor actionContextAccessor, IProductCategoryRepository productCategoryRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
        }


        public async Task<ProductIndexVM> GetAllAsync(ProductIndexVM model)
        {
            var category = await _productCategoryRepository.GetFirstAsync();

            var products = FilterProducts(model);

            model = new ProductIndexVM
            {
                ProductCategories = await _productCategoryRepository.GetAllCategoryAsync(),
                Products = await products.ToListAsync()
            };
            return model;

        }

        public async Task<ProductFilterIndexVM> CategoryProductAsync(int id)
        {
            var category = await _productCategoryRepository.GetAsync(id);
            var model = new ProductFilterIndexVM
            {
                ProductCategory = category,
                Products = category != null ? await _productRepository.GetByCategoryIdAsync(category.Id) : new List<Product>()
            };

            return model;

        }

        public IQueryable<Product> FilterProducts(ProductIndexVM model)
        {
            var products = _productRepository.FilterByTitle(model.Title);
            return products;
        }
    }
}
