namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnFeedbackTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "FullName", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feedbacks", "FullName");
        }
    }
}
