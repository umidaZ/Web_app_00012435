using System.Collections.Generic;
using WAD_12435.Models;

namespace WAD_12435.Repositories
{
    public interface ICategoryRepository
    {
        void InsertCategory (Category category); 
        void UpdateCategory (Category category);
        void DeleteCategory (int categoryId);
        Category GetCategoryById(int Id);
        IEnumerable<Category> GetCategory();
    }
}

