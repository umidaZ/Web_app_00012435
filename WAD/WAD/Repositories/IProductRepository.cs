using System.Collections.Generic;
using WAD_12435.Models;

namespace WAD_12435.Repositories
{
    public interface IProductRepository
    {
        void InsertProdcut(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int productId);
        Product GetProductById(int Id);
        IEnumerable<Product> GetProduct();
    }
}


