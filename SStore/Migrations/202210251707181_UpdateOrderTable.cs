namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CustomerName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Orders", "CustomerPhone", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Orders", "CustomerEmail", c => c.String());
            AddColumn("dbo.Orders", "DeliveryAddress", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DeliveryAddress");
            DropColumn("dbo.Orders", "CustomerEmail");
            DropColumn("dbo.Orders", "CustomerPhone");
            DropColumn("dbo.Orders", "CustomerName");
        }
    }
}
