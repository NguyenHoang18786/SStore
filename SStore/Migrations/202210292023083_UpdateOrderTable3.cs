namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderTable3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "Delivered");
            DropColumn("dbo.Orders", "DeliveredDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "DeliveredDate", c => c.DateTime());
            AddColumn("dbo.Orders", "Delivered", c => c.Boolean(nullable: false));
        }
    }
}
