namespace MapProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCategoryId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LocationViewModels", "CategoryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LocationViewModels", "CategoryId");
        }
    }
}
