using Service.Service.Interface;
using Domain.Model.Entities;
using PresentationLayer.Repository.Interface;
namespace Service.Service.Implementation;

public class CategoryService: ICategoryService
{
    private readonly ICategoryRepository Repository;

    public CategoryService(ICategoryRepository repository)
    {
        Repository = repository;
    }
    public void AddCategory(Category category)
    {
        Repository.EnsureCodeIsUnique(category.Code);
        Repository.EnsureTitleIsUnique(category.Title);

        if (category.ParentCategoryId != null)
        {
            Repository.EnsureIdExists(category.ParentCategoryId.Value);
            Repository.GetCategoryById(category.ParentCategoryId.Value).ChildCategories.Add(category);
        }
        category.Id = Guid.NewGuid();
        Repository.AddCategory(category);
    }
    
    public void DeleteCategoryWithId(Guid id)
    {
        Repository.EnsureIdExists(id);
        var category = Repository.GetCategoryById(id);
        Repository.EnsureCategoryHasNoChildren(category);
        var parent = Repository.GetCategoryById(category.ParentCategoryId.Value);
        
        Repository.DeleteChildFromParent(parent, category);
        Repository.DeleteCategory(category);
        
    }
    
    public void UpdateCategory(Guid id, Category dto)
    {
        Repository.EnsureIdExists(id);
        Repository.EnsureTitleIsUnique(dto.Title);
        var existingCategory = Repository.GetCategoryById(id);
        Repository.EnsureNotSettingItselfAsParent(existingCategory, dto);
        Repository.HandleParentCategoryChange(existingCategory, dto.ParentCategoryId);

        existingCategory.Title = dto.Title;
        existingCategory.Code = dto.Code;
        existingCategory.Description = dto.Description;
        existingCategory.ParentCategoryId = dto.ParentCategoryId;
    }

    public Category GetCategoryWithId(Guid id)
    {
        Repository.EnsureIdExists(id);
        return Repository.GetCategoryById(id);
    }

    public List<Category> GetAllCategories()
    {
        return Repository.GetAllCategories();
    }
    
    
}