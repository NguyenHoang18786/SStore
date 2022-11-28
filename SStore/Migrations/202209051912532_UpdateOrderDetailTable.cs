namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderDetailTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropPrimaryKey("dbo.OrderDetails");
            AddColumn("dbo.OrderDetails", "id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.OrderDetails", "id");
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropPrimaryKey("dbo.OrderDetails");
            DropColumn("dbo.OrderDetails", "id");
            AddPrimaryKey("dbo.OrderDetails", "OrderId");
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "OrderId");
        }
    }
}
