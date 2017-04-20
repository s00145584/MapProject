namespace MapProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpictotrips : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlannedTripsModels", "Picture", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlannedTripsModels", "Picture");
        }
    }
}
