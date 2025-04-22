using WebApplication1.model.entities;

namespace WebApplication1.model;

public class Database
{
    public List<Category> Categories { get; set; }

    public Database()
    {
        Console.WriteLine("usao");
        Categories = new List<Category>();
    }
    


    public bool AddCategory(Category category)
    {
        foreach (var cat in Categories)
        {
            if (cat.Code.Equals(category.Code))
            {
                return false; //Prosledjen je category sa istim kodom
                
            }
        }
        Categories.Add(category);

        return true;
    } 
    
    
    public void DeleteCategoryWithId(Guid id)
    {
        Category cat = Categories.FirstOrDefault(c => c.Id == id);
        if (cat == null)
        {
            Console.WriteLine("There is no such Category with that id");
            return;
        }

        Categories.Remove(cat);

    }
    public void updateCategory(Category cat)
    {
        //Proveri da li postoji
        Category foundCat = Categories.FirstOrDefault(c => c.Id == cat.Id);
        if (foundCat != null)
        {
            Categories.Remove(foundCat);
            Categories.Add(cat);
        }

    }

    public Category GetCategoryWithId(Guid id)
    {
        //vraca null
        return Categories.FirstOrDefault(c => c.Id == id);

    }
}