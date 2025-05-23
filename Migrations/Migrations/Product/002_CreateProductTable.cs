using System.Data;
using FluentMigrator;

namespace Migrations.Migrations.Product;

[Migration(2, "Create Product Table")]
public class CreateProductTable : Migration
{
    public override void Up()
    {
        Create.Table("Products")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Sku").AsString().NotNullable()
            .WithColumn("Title").AsString().NotNullable()
            .WithColumn("Brand").AsString().NotNullable()
            .WithColumn("CategoryId").AsGuid().NotNullable()
            .WithColumn("Price").AsDecimal().NotNullable()
            .WithColumn("ShortDescription").AsString().Nullable()
            .WithColumn("LongDescription").AsString().Nullable()
            .WithColumn("IsEnabled").AsBoolean().Nullable()
            .WithColumn("IsFeatured").AsBoolean().Nullable()
            .WithColumn("ViewCount").AsInt16().Nullable()
            .WithColumn("VisitedAt").AsDateTime2().Nullable();

        Create.ForeignKey("FK_Products_Categories")
            .FromTable("Products").ForeignColumn("CategoryId")
            .ToTable("Categories").PrimaryColumn("Id")
            .OnDeleteOrUpdate(Rule.None);
    }

    public override void Down()
    {
        Delete.Table("Users");
    }
}