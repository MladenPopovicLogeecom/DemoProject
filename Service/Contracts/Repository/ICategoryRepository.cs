using Service.Entities;

namespace Service.Contracts.Repository;

public interface ICategoryRepository : IRepository<Category>
{
    List<Category> GetAllParents();
    void DeleteChildFromParent(Category parent, Category child);
    Category? GetCategoryByTitle(string title);
    Category? GetCategoryByCode(string code);
    
    
    
}