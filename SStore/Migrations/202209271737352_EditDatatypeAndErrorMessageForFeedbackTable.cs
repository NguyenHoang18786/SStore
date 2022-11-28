namespace SStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditDatatypeAndErrorMessageForFeedbackTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Feedbacks", "Description", c => c.String(nullable: false, maxLength: 550));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feedbacks", "Description", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
