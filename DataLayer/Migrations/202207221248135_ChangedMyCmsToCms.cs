namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedMyCmsToCms : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PageComments", new[] { "PageID" });
            DropIndex("dbo.Pages", new[] { "GroupID" });
            CreateIndex("dbo.PageComments", "PageId");
            CreateIndex("dbo.Pages", "GroupId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Pages", new[] { "GroupId" });
            DropIndex("dbo.PageComments", new[] { "PageId" });
            CreateIndex("dbo.Pages", "GroupID");
            CreateIndex("dbo.PageComments", "PageID");
        }
    }
}
