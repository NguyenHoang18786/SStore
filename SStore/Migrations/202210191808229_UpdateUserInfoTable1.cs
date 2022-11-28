namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserInfoTable1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInfoes", "Gender", c => c.Int(nullable: false));
            AlterColumn("dbo.UserInfoes", "Country", c => c.String(maxLength: 100));
            AlterColumn("dbo.UserInfoes", "City", c => c.String(maxLength: 100));
            AlterColumn("dbo.UserInfoes", "Address", c => c.String(maxLength: 255));
            AlterColumn("dbo.UserInfoes", "Phone", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfoes", "Phone", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.UserInfoes", "Address", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.UserInfoes", "City", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.UserInfoes", "Country", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.UserInfoes", "Gender", c => c.Int());
        }
    }
}
