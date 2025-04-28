using Service.Service.Interface;
using Domain.Model.Entities;
using PresentationLayer.Repository.Implementation;

namespace Service.Service.Implementation;

public class CategoryService : ICategoryService
{
    private CategoryRepositoryInMemory Repository;
    private CategoryValidator CategoryValidator;

    public CategoryService()
    {
        Repository = new CategoryRepositoryInMemory();
        CategoryValidator = new CategoryValidator(Repository);
    }

    public void AddCategory(Category category)
    {
        CategoryValidator.EnsureCodeIsUnique(category.Code);
        CategoryValidator.EnsureTitleIsUnique(category.Title);

        if (category.ParentCategoryId != null)
        {
            CategoryValidator.EnsureIdExists(category.ParentCategoryId.Value);
            Repository.GetCategoryById(category.ParentCategoryId.Value).ChildCategories.Add(category);
        }

        category.Id = Guid.NewGuid();
        Repository.AddCategory(category);
    }

    public void DeleteCategoryWithId(Guid id)
    {
        CategoryValidator.EnsureIdExists(id);
        var category = Repository.GetCategoryById(id);
        CategoryValidator.EnsureCategoryHasNoChildren(category);
        var parent = Repository.GetCategoryById(category.ParentCategoryId.Value);

        if (parent != null)
        {
            Repository.DeleteChildFromParent(parent, category);
        }
        
        Repository.DeleteCategory(category);
    }

    public void UpdateCategory(Guid id, Category dto)
    {
        CategoryValidator.EnsureIdExists(id);
        CategoryValidator.EnsureTitleIsUnique(dto.Title);
        var existingCategory = Repository.GetCategoryById(id);
        CategoryValidator.EnsureNotSettingItselfAsParent(existingCategory, dto);
        CategoryValidator.HandleParentCategoryChange(existingCategory, dto.ParentCategoryId);

        existingCategory.Title = dto.Title;
        existingCategory.Code = dto.Code;
        existingCategory.Description = dto.Description;
        existingCategory.ParentCategoryId = dto.ParentCategoryId;
    }

    public Category GetCategoryWithId(Guid id)
    {
        CategoryValidator.EnsureIdExists(id);
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
            //I did not use this.AddCategory(), beacause it creates new Guid
            //For seeding database, i want to have "000-000...-000" Guid for every category, just for easier testing

            Repository.AddCategory(cat);
            //AddCategory(cat);
        }
    }
}