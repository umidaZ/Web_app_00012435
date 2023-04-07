using System.Collections.Generic;
using System.Linq;
using WAD_12435.DAL;
using WAD_12435.Models;

namespace WAD_12435.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryContext _dbContext;
        public CategoryRepository(CategoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Category FindCategoryById(int categoryId)
        {
            return _dbContext.Categories.Find(categoryId);
        }


        public void DeleteCategory(int categoryId)
        {
            var category = FindCategoryById(categoryId);
            _dbContext.Categories.Remove(category);
            Save();
        }

        private void Save()
        {
                _dbContext.SaveChanges();
        }

        public Category GetCategoryById(int  Id)
        {
            var category = FindCategoryById(Id);
            return category;
        }

        public IEnumerable<Category> GetCategory()
        {
            return _dbContext.Categories.ToList();
        }

        public void InsertCategory(Category category)
        {
            _dbContext.Add(category);
            Save();
        }

        public void UpdateCategory(Category category)
        {
            var isCatExist = _dbContext.Categories.Find(category.ID);
            isCatExist.Name = category.Name;
            isCatExist.Capacity = category.Capacity;
            isCatExist.CategoryTypeEnum = category.CategoryTypeEnum;
            isCatExist.CreatedAt = category.CreatedAt;
            Save();
        }
    }
}
