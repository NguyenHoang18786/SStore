﻿namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "TotalPrice");
        }
    }
}
