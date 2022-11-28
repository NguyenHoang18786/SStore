namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixOrderTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "FirstName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "FirstName", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "OrderDate");
        }
    }
}
