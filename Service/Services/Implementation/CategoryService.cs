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

    public void Add(Category category)
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
    }

    public void DeleteById(Guid id)
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
    }

    public void Update(Guid id, Category dto)
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
    }

    public Category GetById(Guid id)
    {
        categoryBusinessValidator.EnsureIdExists(id);
        return repository.GetById(id).GetAwaiter().GetResult()!;
    }

    public List<Category> GetAll()
    {
        return repository.GetAll().GetAwaiter().GetResult();
    }
    
    public List<Category> GetAllParents()
    {
        return repository.GetAllParents().GetAwaiter().GetResult();
    }

    public void SeedDatabase()
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
    }
}