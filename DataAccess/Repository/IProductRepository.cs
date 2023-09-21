using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.repository
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProductByID(int productId);
        void DeleteProduct(Product product);
        void InsertProduct(Product product);
        void UpdateProduct(Product product);
        List<Product> FindProductsByName(string productName);
    }
}
