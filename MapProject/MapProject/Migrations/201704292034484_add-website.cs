namespace MapProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addwebsite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LocationViewModels", "Website", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LocationViewModels", "Website");
        }
    }
}
