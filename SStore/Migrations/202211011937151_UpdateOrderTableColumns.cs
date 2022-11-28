namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderTableColumns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "PaymentStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "PaymentStatus", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Orders", "Status", c => c.Boolean(nullable: false));
        }
    }
}
