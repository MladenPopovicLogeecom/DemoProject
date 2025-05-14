using Service.Contracts.Repository;
using Service.Entities;
using Service.Services.Interfaces;

namespace Service.Services.Implementation;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository repository;
    private readonly CategoryBusinessValidator categoryBusinessValidator;

    public CategoryService(ICategoryRepository iCategoryRepository)
    {
        repository = iCategoryRepository;
        categoryBusinessValidator = new CategoryBusinessValidator(repository);
    }

    public Task Add(Category category)
    {
        categoryBusinessValidator.EnsureCodeIsUnique(category.Code);
        categoryBusinessValidator.EnsureTitleIsUnique(category.Title);

        
        if (category.ParentCategoryId != null)
        {
            categoryBusinessValidator.EnsureIdExists(category.ParentCategoryId.Value);
            repository.GetById(category.ParentCategoryId.Value).GetAwaiter().GetResult()!.ChildCategories.Add(category);
        }

        category.Id = Guid.NewGuid();
        repository.Add(category);
        return Task.CompletedTask;
    }
    
    public Task DeleteById(Guid id)
    {
        categoryBusinessValidator.EnsureIdExists(id);
        Category category = repository.GetById(id).GetAwaiter().GetResult()!;
        categoryBusinessValidator.EnsureCategoryHasNoChildren(category);
        if (category.ParentCategoryId != null)
        {
            categoryBusinessValidator.EnsureIdExists(category.ParentCategoryId.Value);
            Category parent = repository.GetById(category.ParentCategoryId.Value).GetAwaiter().GetResult()!;
            repository.DeleteChildFromParent(parent, category);
        }
        
        repository.Delete(category.Id!.Value);
        return Task.CompletedTask;
    }

    public Task Update(Guid id, Category dto)
    {
        categoryBusinessValidator.EnsureIdExists(id);
        categoryBusinessValidator.EnsureTitleIsUnique(dto.Title);
        Category existingCategory = repository.GetById(id).GetAwaiter().GetResult()!;
        categoryBusinessValidator.EnsureNotSettingItselfAsParent(existingCategory, dto);
        categoryBusinessValidator.HandleParentCategoryChange(existingCategory, dto.ParentCategoryId);

        existingCategory.Title = dto.Title;
        existingCategory.Code = dto.Code;
        existingCategory.Description = dto.Description;
        existingCategory.ParentCategoryId = dto.ParentCategoryId;
        return Task.CompletedTask;
    }

    public async Task<Category> GetById(Guid id)
    {
        categoryBusinessValidator.EnsureIdExists(id);
        return (await repository.GetById(id))!;
    }

    public async Task<List<Category>> GetAll()
    {
        return await repository.GetAll();
    }
    
    public async Task<List<Category>> GetAllParents()
    {
        return await repository.GetAllParents();
    }

    public Task SeedDatabase()
    {
        for (var i = 0; i < 3; i++)
        {
            Category cat = new Category("Title " + i, "Code " + i,
                "Description " + i,
                null)
            {
                Id = Guid.Empty
            };

            repository.Add(cat);
        }
        return Task.CompletedTask;
    }
}