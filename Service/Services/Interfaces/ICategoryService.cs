using Service.Entities;

namespace Service.Services.Interfaces
{
    public interface ICategoryService
    {
        void Add(Category category);
        void DeleteById(Guid id);
        void Update(Guid id, Category dto);
        Category GetById(Guid id);
        List<Category> GetAll();
        List<Category> GetAllParents();
        void SeedDatabase();
    }
}