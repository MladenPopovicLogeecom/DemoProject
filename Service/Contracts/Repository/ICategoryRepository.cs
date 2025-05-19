using Service.Entities;

namespace Service.Contracts.Repository;

public interface ICategoryRepository : IRepository<Category>
{
    Task<List<Category>> GetAllParents();
    Task HardDeleteBeforeDate(DateTime date,double threshold);
    Task SoftDelete(Guid id);
    Task DeleteChildFromParent(Category parent, Category child);
    Task AddChildToParent(Category parent, Category child);
    Task<Category?> GetCategoryByTitle(string title);
    Task<Category?> GetCategoryByCode(string code);
    
    
    
}