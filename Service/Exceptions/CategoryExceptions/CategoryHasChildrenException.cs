namespace Service.Exceptions.CategoryExceptions;

public class CategoryHasChildrenException(string title)
    : Exception("Category with title: \"" + title + "\" cannot be deleted because it has children.");