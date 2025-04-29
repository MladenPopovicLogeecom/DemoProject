using PresentationLayer.Entities;
using PresentationLayer.Repositories.Implementations;
using Service.Services.Interfaces;

namespace Service.Services.Implementation;

public class CategoryService : ICategoryService
{
    private readonly CategoryRepositoryInMemory repository;
    private readonly CategoryBusinessValidator categoryBusinessValidator;

    public CategoryService()
    {
        repository = new CategoryRepositoryInMemory();
        categoryBusinessValidator = new CategoryBusinessValidator(repository);
    }

    public void AddCategory(Category category)
    {
        categoryBusinessValidator.EnsureCodeIsUnique(category.Code);
        categoryBusinessValidator.EnsureTitleIsUnique(category.Title);

        
        if (category.ParentCategoryId != null)
        {
            categoryBusinessValidator.EnsureIdExists(category.ParentCategoryId.Value);
            repository.GetCategoryById(category.ParentCategoryId.Value)!.ChildCategories.Add(category);
        }

        category.Id = Guid.NewGuid();
        repository.AddCategory(category);
    }

    public void DeleteCategoryWithId(Guid id)
    {
        categoryBusinessValidator.EnsureIdExists(id);
        Category category = repository.GetCategoryById(id)!;
        categoryBusinessValidator.EnsureCategoryHasNoChildren(category);
        if (category.ParentCategoryId != null)
        {
            categoryBusinessValidator.EnsureIdExists(category.ParentCategoryId.Value);
            Category parent = repository.GetCategoryById(category.ParentCategoryId.Value)!;
            repository.DeleteChildFromParent(parent, category);
        }
        
        repository.DeleteCategory(category);
    }

    public void UpdateCategory(Guid id, Category dto)
    {
        categoryBusinessValidator.EnsureIdExists(id);
        categoryBusinessValidator.EnsureTitleIsUnique(dto.Title);
        Category existingCategory = repository.GetCategoryById(id)!;
        categoryBusinessValidator.EnsureNotSettingItselfAsParent(existingCategory, dto);
        categoryBusinessValidator.HandleParentCategoryChange(existingCategory, dto.ParentCategoryId);

        existingCategory.Title = dto.Title;
        existingCategory.Code = dto.Code;
        existingCategory.Description = dto.Description;
        existingCategory.ParentCategoryId = dto.ParentCategoryId;
    }

    public Category GetCategoryWithId(Guid id)
    {
        categoryBusinessValidator.EnsureIdExists(id);
        return repository.GetCategoryById(id)!;
    }

    public List<Category> GetAllCategories()
    {
        return repository.GetAllCategories();
    }


    public List<Category> GetAllParents()
    {
        List<Category> allCategories = GetAllCategories();

        return allCategories
            .Where(c => c.ParentCategoryId == null)
            .ToList();
    }

    public void SeedDatabase()
    {
        for (var i = 0; i < 3; i++)
        {
            
            Category cat = new Category("Title " + i, "Code " + i,
                "Description " + i,
                null);
            //I did not use this.AddCategory(), because it creates new Guid
            //For seeding database, I want to have "000-000...-000" Guid for every category, just for easier testing

            repository.AddCategory(cat);
            //AddCategory(cat);
        }
    }
}