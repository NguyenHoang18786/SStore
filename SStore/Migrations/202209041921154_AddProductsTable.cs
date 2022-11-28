namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 255),
                        CategoryId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Weight = c.Double(),
                        Size = c.String(maxLength: 20),
                        Color = c.String(maxLength: 100),
                        BrandId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Description = c.String(maxLength: 255),
                        Image = c.String(maxLength: 255),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        Hot = c.Boolean(),
                        View = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductBrands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.ProductCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.BrandId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.Products", "BrandId", "dbo.ProductBrands");
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.Products");
        }
    }
}
