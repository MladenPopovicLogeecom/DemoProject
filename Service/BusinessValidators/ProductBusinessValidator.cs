using Service.Contracts.Repository;
using Service.Entities;
using Service.Exceptions.ProductExceptions;

namespace Service.BusinessValidators;

public class ProductBusinessValidator(IProductRepository repository)
{
    public async Task<Product> EnsureProductExists(Guid id)
    {
        Product? product = await repository.GetById(id);
        if (product == null)
        {
            throw new ProductWithIdNotFoundException(id);
        }

        return product;
    }

    public async Task EnsureTitleIsUnique(string title)
    {
        if (await repository.GetProductByTitle(title) != null)
        {
            throw new ProductTitleIsNotUniqueException(title);
        }
    }

    public async Task EnsureSkuIsUnique(string sku)
    {
        if (await repository.GetProductBySku(sku) != null)
        {
            throw new ProductSkuIsNotUniqueException(sku);
        }
    }
}