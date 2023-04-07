using System.Collections.Generic;
using WAD_12435.DAL;
using WAD_12435.Models;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace WAD_12435.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CategoryContext _dbContext;
        public ProductRepository(CategoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Product FindCategoryById(int progressId)
        {
            return _dbContext.Products.Find(progressId);
        }

        public void DeleteProduct(int progressId)
        {
            var progress = FindCategoryById(progressId);
            _dbContext.Products.Remove(progress);
            Save();
        }

        public IEnumerable<Product> GetProduct()
        {
            return _dbContext.Products.Include(s => s.Category).ToList();

        }

        public Product GetProductById(int Id)
        {
            var progress = FindCategoryById(Id);
            _dbContext.Entry(progress).Reference(s => s.Category).Load();

            return progress;
        }

        public void InsertProdcut(Product product)
        {
            if (product.Category != null)
            {
                product.Category = _dbContext.Categories.FirstOrDefault(h => h.ID == product.Category.ID);
            }
            _dbContext.Add(product);
            Save();
        }

        private void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            var existingProgress = _dbContext.Products.Find(product.ID);


            // Update the existingProgress object with the updated Category object
            existingProgress.Category.ID = product.Category.ID;
            existingProgress.Price = product.Price;
            existingProgress.isActive = product.isActive;
            existingProgress.Description = product.Description;
            existingProgress.CreatedAt = product.CreatedAt;

            Save();
        }

    }
}
