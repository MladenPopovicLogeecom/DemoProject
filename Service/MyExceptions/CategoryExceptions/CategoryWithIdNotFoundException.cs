namespace Service.MyExceptions.CategoryExceptions;

public class CategoryWithIdNotFoundException(Guid id) : 
    Exception("Category with ID: \"" + id + "\" not found");