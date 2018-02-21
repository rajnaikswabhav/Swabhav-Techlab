namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v49 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.EVENTTICKET", "Device", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("gsmktg.EVENTTICKET", "Device");
        }
    }
}
