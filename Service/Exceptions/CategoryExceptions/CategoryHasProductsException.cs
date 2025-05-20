namespace Service.Exceptions.CategoryExceptions;

public class CategoryHasProductsException(string title)
    : Exception("Category : \"" + title + "\" has products, it cannot be deleted.");