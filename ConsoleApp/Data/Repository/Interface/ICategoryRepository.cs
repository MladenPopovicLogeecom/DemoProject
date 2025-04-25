using Domain.Model.Entities;

namespace PresentationLayer.Repository.Interface;

public interface ICategoryRepository
{
    void AddCategory(Category category);
    void DeleteCategory(Category category);
    void SaveCategory (Category category);

    void DeleteChildFromParent(Category parent, Category child);
    Category GetCategoryById(Guid id);
    List<Category> GetAllCategories();
    
    //Checkings and validations
    void EnsureCodeIsUnique(string code);
    void EnsureTitleIsUnique(string title);
    void EnsureIdExists(Guid id);
    void EnsureCategoryHasNoChildren(Category category);
    void EnsureNotSettingItselfAsParent(Category category, Category dto);
    void HandleParentCategoryChange(Category category, Guid? newParentId);
    
}