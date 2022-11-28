namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropColumnTotalPriceInTableCartDetail : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CartDetails", "TotalPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CartDetails", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
