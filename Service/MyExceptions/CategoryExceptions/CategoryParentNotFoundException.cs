namespace Service.MyExceptions.CategoryExceptions;

public class CategoryParentNotFoundException(Guid id) : 
    Exception("Category parent with ID: \"" + id + "\" not found");