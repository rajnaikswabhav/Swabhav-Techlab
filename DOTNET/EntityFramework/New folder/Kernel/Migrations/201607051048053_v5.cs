namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.EXHIBITION", "Latitude", c => c.String());
            AddColumn("gsmktg.EXHIBITION", "Longitude", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("gsmktg.EXHIBITION", "Longitude");
            DropColumn("gsmktg.EXHIBITION", "Latitude");
        }
    }
}
