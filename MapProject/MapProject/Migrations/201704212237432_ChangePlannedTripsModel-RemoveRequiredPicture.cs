namespace MapProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePlannedTripsModelRemoveRequiredPicture : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PlannedTripsModels", "Picture", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PlannedTripsModels", "Picture", c => c.Binary(nullable: false));
        }
    }
}
