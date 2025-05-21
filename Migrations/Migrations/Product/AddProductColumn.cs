using FluentMigrator;

namespace Migrations.Migrations.Product;

[Migration(10, "Add Product Column")]
public class AddProductColumn : Migration
{
    public override void Up()
    {
        Alter.Table("Products").AddColumn("VisitedAt").AsDateTime2().Nullable();
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}