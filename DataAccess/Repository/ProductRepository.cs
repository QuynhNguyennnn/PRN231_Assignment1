using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.repository
{
    public class ProductRepository : IProductRepository
    {
        public Product GetProductByID(int productId) => ProductDAO.GetProductById(productId);
        public List<Product> GetProducts() => ProductDAO.GetProducts();
        public void InsertProduct(Product product) => ProductDAO.InsertProduct(product);
        public void DeleteProduct(Product product) => ProductDAO.DeleteProduct(product);
        public void UpdateProduct(Product product) => ProductDAO.UpdateProduct(product);
        public List<Product> FindProductsByName(string productName) => ProductDAO.FindProductsByName(productName);
    }
}
