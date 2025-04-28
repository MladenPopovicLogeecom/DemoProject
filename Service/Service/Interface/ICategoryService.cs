using Domain.Model.Entities;

namespace Service.Service.Interface
{
    public interface ICategoryService
    {
        void AddCategory(Category category);
        void DeleteCategoryWithId(Guid id);
        void UpdateCategory(Guid id, Category dto);
        Category GetCategoryWithId(Guid id);
        List<Category> GetAllCategories();
        List<Category> GetAllParents();
        void seedDatabase();
    }
}