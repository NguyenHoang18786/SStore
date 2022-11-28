namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditOrderDetailTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OrderDetails");
            AddPrimaryKey("dbo.OrderDetails", new[] { "OrderId", "ProductId", "Quantity", "Price" });
            DropColumn("dbo.OrderDetails", "id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.OrderDetails");
            AddPrimaryKey("dbo.OrderDetails", "id");
        }
    }
}
