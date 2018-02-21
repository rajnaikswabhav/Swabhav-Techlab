namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v79 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.BOOKINGEXHIBITORDISCOUNT", "BgImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("gsmktg.BOOKINGEXHIBITORDISCOUNT", "BgImage");
        }
    }
}
