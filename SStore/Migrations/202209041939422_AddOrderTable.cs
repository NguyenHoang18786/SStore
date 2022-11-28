namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        FirstName = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Delivered = c.Boolean(nullable: false),
                        DeliveredDate = c.DateTime(),
                        UserId = c.String(maxLength: 128),
                        PaymentType = c.String(maxLength: 100),
                        PaymentStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.UserInfoes", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.UserInfoes");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropTable("dbo.Orders");
        }
    }
}
