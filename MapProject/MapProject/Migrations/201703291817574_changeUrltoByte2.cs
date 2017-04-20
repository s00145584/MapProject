namespace MapProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUrltoByte2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LocationViewModels", "Picture", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LocationViewModels", "Picture");
        }
    }
}
