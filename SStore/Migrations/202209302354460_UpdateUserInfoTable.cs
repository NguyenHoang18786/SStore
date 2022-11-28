namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserInfoTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInfoes", "Gender", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfoes", "Gender", c => c.Int(nullable: false));
        }
    }
}
