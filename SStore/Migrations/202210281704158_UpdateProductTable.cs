namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Products", "ModifiedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.Products", "CreatedDate", c => c.DateTime());
        }
    }
}
