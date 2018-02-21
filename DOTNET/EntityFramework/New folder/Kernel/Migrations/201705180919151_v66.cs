namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v66 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.BOOKING", "BookingId", c => c.String());
            AddColumn("gsmktg.BOOKING", "Comment", c => c.String());
            AddColumn("gsmktg.BOOKINGREQUEST", "BookingRequestId", c => c.String());
            AddColumn("gsmktg.BOOKINGREQUEST", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("gsmktg.BOOKINGREQUEST", "Comment");
            DropColumn("gsmktg.BOOKINGREQUEST", "BookingRequestId");
            DropColumn("gsmktg.BOOKING", "Comment");
            DropColumn("gsmktg.BOOKING", "BookingId");
        }
    }
}
