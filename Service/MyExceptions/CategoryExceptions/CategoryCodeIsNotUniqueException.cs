namespace Service.MyExceptions.CategoryExceptions;

public class CategoryCodeIsNotUniqueException(string code)
    : Exception("Category with code: \"" + code + "\" already exists!");