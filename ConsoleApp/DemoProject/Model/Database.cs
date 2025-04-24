using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DemoProject.model.entities;

namespace DemoProject.model;

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