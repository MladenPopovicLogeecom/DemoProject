using Service.Entities;

namespace Service.Services.Interfaces
{
    public interface ICategoryService : IService<Category>
    {
        
        List<Category> GetAllParents();
        
    }
}