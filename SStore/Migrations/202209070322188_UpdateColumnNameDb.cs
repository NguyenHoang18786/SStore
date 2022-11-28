namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColumnNameDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartDetails", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrderDetails", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.CartDetails", "Price");
            DropColumn("dbo.OrderDetails", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.CartDetails", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.OrderDetails", "TotalPrice");
            DropColumn("dbo.CartDetails", "TotalPrice");
        }
    }
}
