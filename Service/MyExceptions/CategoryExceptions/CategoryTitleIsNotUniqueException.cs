namespace Service.MyExceptions.CategoryExceptions;

public class CategoryTitleIsNotUniqueException(string title)
    : Exception("Category with title: \"" + title + "\" already exists");