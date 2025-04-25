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
    

    public List<Category> GetAllParents()
    {
        var allCategories = GetAllCategories();

        return allCategories
            .Where(c => c.ParentCategoryId == null)
            .ToList();
    }

    public void seedDatabase()
    {
        for (var i = 0; i < 3; i++)
        {
            var cat = new Category("Title " + i, "Code " + i,
                "Description " + i,
                null);
            //I did not use AddCategory, beacause it creates new Guid
            //For seeding database, i want to have "000-000...-000" Guid for every category, just for easier testing
            Repository.AddCategory(cat);
            //AddCategory(cat);
        }
    }
}