using Service.Entities;

namespace Service.Services.Interfaces
{
    public interface ICategoryService : IService<Category>
    {
        
        Task<List<Category>> GetAllParents();
        Task SoftDelete(Guid id);
        
        
    }
}