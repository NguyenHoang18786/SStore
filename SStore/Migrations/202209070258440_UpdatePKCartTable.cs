namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePKCartTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "UserId", "dbo.UserInfoes");
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropPrimaryKey("dbo.Carts");
            AlterColumn("dbo.Carts", "UserId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Carts", "CardId");
            CreateIndex("dbo.Carts", "UserId");
            AddForeignKey("dbo.Carts", "UserId", "dbo.UserInfoes", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "UserId", "dbo.UserInfoes");
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropPrimaryKey("dbo.Carts");
            AlterColumn("dbo.Carts", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Carts", new[] { "CardId", "UserId" });
            CreateIndex("dbo.Carts", "UserId");
            AddForeignKey("dbo.Carts", "UserId", "dbo.UserInfoes", "UserId", cascadeDelete: true);
        }
    }
}
