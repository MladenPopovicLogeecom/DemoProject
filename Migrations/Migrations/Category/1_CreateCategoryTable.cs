using System.Data;
using FluentMigrator;

namespace Migrations.Migrations.Category;

[Migration(1, "Create Category Table")]
public class CreateCategoryTable : Migration
{
    public override void Up()
    {
        Create.Table("Categories")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Title").AsString().NotNullable()
            .WithColumn("Code").AsString().NotNullable()
            .WithColumn("Description").AsString().NotNullable()
            .WithColumn("ParentCategoryId").AsGuid().Nullable()
            .WithColumn("DeletedAt").AsDateTime2().Nullable();

        Create.ForeignKey("FK_Category_Category")
            .FromTable("Categories").ForeignColumn("ParentCategoryId")
            .ToTable("Categories").PrimaryColumn("Id").OnDeleteOrUpdate(Rule.None);
    }

    public override void Down()
    {
        Delete.Table("Categories");
    }
}