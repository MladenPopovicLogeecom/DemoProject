namespace Service.Exceptions.ProductExceptions;

public class ProductTitleIsNotUniqueException(string title)
    : Exception("Product with title: \"" + title + "\" already exists");