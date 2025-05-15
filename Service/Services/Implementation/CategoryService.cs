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

    public async Task Add(Category category)
    {
        categoryBusinessValidator.EnsureCodeIsUnique(category.Code);
        categoryBusinessValidator.EnsureTitleIsUnique(category.Title);

        if (category.ParentCategoryId != null)
        {
            categoryBusinessValidator.EnsureIdExists(category.ParentCategoryId.Value);
            await repository.GetById(category.ParentCategoryId.Value);
        }

        category.Id = Guid.NewGuid();
        await repository.Add(category);
    }

    public async Task DeleteById(Guid id)
    {
        categoryBusinessValidator.EnsureIdExists(id);
        Category category = (await repository.GetById(id))!;
        categoryBusinessValidator.EnsureCategoryHasNoChildren(category);
        if (category.ParentCategoryId != null)
        {
            categoryBusinessValidator.EnsureIdExists(category.ParentCategoryId.Value);
            Category parent = repository.GetById(category.ParentCategoryId.Value).GetAwaiter().GetResult()!;
            await repository.DeleteChildFromParent(parent, category);
        }

        await repository.Delete(category.Id!.Value);
    }

    public async Task Update(Guid id, Category dto)
    {
        categoryBusinessValidator.EnsureIdExists(id);
        categoryBusinessValidator.EnsureTitleIsUnique(dto.Title);
        Category existingCategory = (await repository.GetById(id))!;
        categoryBusinessValidator.EnsureNotSettingItselfAsParent(existingCategory, dto);
        categoryBusinessValidator.HandleParentCategoryChange(existingCategory, dto.ParentCategoryId);

        existingCategory.Title = dto.Title;
        existingCategory.Code = dto.Code;
        existingCategory.Description = dto.Description;
        existingCategory.ParentCategoryId = dto.ParentCategoryId;
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
                null,null)
            {
                Id = Guid.Empty
            };

            repository.Add(cat);
        }

        return Task.CompletedTask;
    }
}