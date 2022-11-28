namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFeedbackTableErrorMessage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Feedbacks", "Title", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Feedbacks", "Description", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feedbacks", "Description", c => c.String(maxLength: 255));
            AlterColumn("dbo.Feedbacks", "Title", c => c.String(maxLength: 150));
        }
    }
}
