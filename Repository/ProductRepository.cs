using System;
using ecommerceApi_netcore_devtalles.Models;
using ecommerceApi_netcore_devtalles.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ecommerceApi_netcore_devtalles.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;
    public ProductRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    private string Normalize(string input) => input.ToLower().Trim();

    public bool BuyProduct(string name, int quantity)
    {
        if (string.IsNullOrEmpty(name) || quantity <= 0)
        {
            return false;
        }
        var product = _db.Products.FirstOrDefault(p => p.Name.ToLower().Trim() == name.ToLower().Trim());
        if (product == null || product.Stock < quantity)
        {
            return false;
        }
        product.Stock -= quantity;
        _db.Products.Update(product);
        return Save();
    }

    public bool CreateProduct(Product product)
    {
        if (product == null)
        {
            return false;
        }
        product.CreationDate = DateTime.Now;
        product.UpdateDate = DateTime.Now;
        _db.Products.Add(product);
        return Save();
    }

    public bool DeleteProduct(Product product)
    {
        if (product == null)
        {
            return false;
        }
        _db.Products.Remove(product);
        return Save();
    }

    public Product? GetProductById(int id)
    {
        if (id <= 0)
        {
            return null;
        }
        return _db.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
    }

    public Product? GetProductByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;

        var normalizedName = name.ToLower().Trim();
        return _db.Products.FirstOrDefault(p => p.Name.ToLower().Trim() == normalizedName);
    }

    public ICollection<Product> GetAllProducts() => _db.Products.Include(p => p.Category).OrderBy(p => p.Name).ToList();

    public ICollection<Product> GetProductsByCategory(int categoryId)
    {
        if (categoryId <= 0)
        {
            return new List<Product>();
        }
        return _db.Products.Include(p => p.Category).Where(p => p.CategoryId == categoryId).OrderBy(p => p.Name).ToList();

    }

    public bool ProductExists(int id)
    {
        if (id <= 0)
        {
            return false;
        }
        return _db.Products.Any(p => p.ProductId == id);
    }

    public bool ProductExists(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }
        return _db.Products.Any(p => p.Name.ToLower().Trim() == name.ToLower().Trim());
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0;
    }

    public ICollection<Product> SearchProducts(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return _db.Products.Include(p => p.Category).OrderBy(p => p.Name).ToList();
        }

        var searchTermLowered = Normalize(searchTerm);
        var query = _db.Products.Include(p => p.Category)
            .Where(p =>
                p.Name.ToLower().Trim().Contains(searchTermLowered) ||
                p.Description.ToLower().Trim().Contains(searchTermLowered));
        return query.OrderBy(p => p.Name).ToList();
    }

    public bool UpdateProduct(Product product)
    {
        if (product == null)
        {
            return false;
        }
        product.UpdateDate = DateTime.Now;
        _db.Products.Update(product);
        return Save();
    }
}
