namespace Service.Exceptions.ProductExceptions;

public class ProductSkuIsNotUniqueException(string sku)
    : Exception("Product with sku: \"" + sku + "\" already exists");