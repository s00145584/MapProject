namespace MapProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUrltoByte : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LocationViewModels", "Url");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LocationViewModels", "Url", c => c.String(nullable: false));
        }
    }
}
