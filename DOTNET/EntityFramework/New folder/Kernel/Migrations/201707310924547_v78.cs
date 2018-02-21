namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v78 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.BOOKINGEXHIBITORDISCOUNT", "ClickCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("gsmktg.BOOKINGEXHIBITORDISCOUNT", "ClickCount");
        }
    }
}
