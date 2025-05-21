namespace Service.Exceptions.ProductExceptions;

public class ProductWithIdNotFoundException(Guid id) :
    Exception("Product with ID: \"" + id + "\" not found");