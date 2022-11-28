namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditOrderDetailTable1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OrderDetails");
            AddPrimaryKey("dbo.OrderDetails", new[] { "OrderId", "ProductId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.OrderDetails");
            AddPrimaryKey("dbo.OrderDetails", new[] { "OrderId", "ProductId", "Quantity", "Price" });
        }
    }
}
