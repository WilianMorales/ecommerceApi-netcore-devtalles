using System;
using ecommerceApi_netcore_devtalles.Models;

namespace ecommerceApi_netcore_devtalles.Repository.IRepository;

public interface IProductRepository
{
    ICollection<Product> GetAllProducts();
    ICollection<Product> GetProductsByCategory(int categoryId);
    ICollection<Product> SearchProducts(string searchTerm);
    Product? GetProductById(int id);
    bool BuyProduct(string name, int quantity);
    bool ProductExists(int id);
    bool ProductExists(string name);
    bool CreateProduct(Product product);
    bool UpdateProduct(Product product);
    bool DeleteProduct(Product product);
    bool Save();
}
