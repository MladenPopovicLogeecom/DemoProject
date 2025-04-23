using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApplication1.model.entities;

namespace WebApplication1.model;

public class Database
{
    public Database()
    {
        Categories = new List<Category>();
    }

    public List<Category> Categories { get; set; }

    public Category? GetCategoryWithId(Guid id)
    {
        return Categories.FirstOrDefault(c => c.Id == id);
    }
}
