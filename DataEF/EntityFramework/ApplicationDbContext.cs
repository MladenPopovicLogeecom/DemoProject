using Microsoft.EntityFrameworkCore;
using Service.Entities;

namespace DataEF.EntityFramework;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //mapping
        modelBuilder.Entity<Category>()
            .ToTable("Categories")
            .HasMany(c => c.ChildCategories)
            .WithOne()
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();
    }
}