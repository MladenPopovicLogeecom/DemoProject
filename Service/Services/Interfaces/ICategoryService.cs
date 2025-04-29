using PresentationLayer.Entities;

namespace Service.Services.Interfaces
{
    public interface ICategoryService
    {
        void AddCategory(Category category);
        void DeleteCategoryWithId(Guid id);
        void UpdateCategory(Guid id, Category dto);
        Category GetCategoryWithId(Guid id);
        List<Category> GetAllCategories();
        List<Category> GetAllParents();
        void SeedDatabase();
    }
}