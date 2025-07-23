using System;

namespace ecommerceApi_netcore_devtalles.Repository.IRepository;

public interface ICategoryRepository
{
    ICollection<Category> GetAllCategories();
    Category? GetCategoryById(int id);
    bool CategoryExists(int id);
    bool CategoryExists(string name);
    bool CreateCategory(Category category);
    bool UpdateCategory(Category category);
    bool DeleteCategory(Category category);
    bool Save();
}
