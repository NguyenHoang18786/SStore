namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCartDetailTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CartId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quatity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.CartId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CartId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CartDetails", "CartId", "dbo.Carts");
            DropIndex("dbo.CartDetails", new[] { "ProductId" });
            DropIndex("dbo.CartDetails", new[] { "CartId" });
            DropTable("dbo.CartDetails");
        }
    }
}
