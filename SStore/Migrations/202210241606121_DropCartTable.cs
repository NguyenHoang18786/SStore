namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropCartTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "UserId", "dbo.UserInfoes");
            DropForeignKey("dbo.CartDetails", "CartId", "dbo.Carts");
            DropForeignKey("dbo.CartDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.CartDetails", new[] { "CartId" });
            DropIndex("dbo.CartDetails", new[] { "ProductId" });
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropTable("dbo.CartDetails");
            DropTable("dbo.Carts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CardId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CardId);
            
            CreateTable(
                "dbo.CartDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CartId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Carts", "UserId");
            CreateIndex("dbo.CartDetails", "ProductId");
            CreateIndex("dbo.CartDetails", "CartId");
            AddForeignKey("dbo.CartDetails", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CartDetails", "CartId", "dbo.Carts", "CardId", cascadeDelete: true);
            AddForeignKey("dbo.Carts", "UserId", "dbo.UserInfoes", "UserId");
        }
    }
}
