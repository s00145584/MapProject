namespace MapProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePlannedTripsModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlannedTripsModels", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlannedTripsModels", "UserId");
        }
    }
}
