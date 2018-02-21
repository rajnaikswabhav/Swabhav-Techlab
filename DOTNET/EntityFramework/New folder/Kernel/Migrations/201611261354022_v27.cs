namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v27 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("gsmktg.STALL", "BookingRequestStall_Id", "gsmktg.BOOKINGREQUESTSTALL");
            DropForeignKey("gsmktg.STALL", "StallBooking_Id", "gsmktg.STALLBOOKING");
            DropIndex("gsmktg.STALL", new[] { "BookingRequestStall_Id" });
            DropIndex("gsmktg.STALL", new[] { "StallBooking_Id" });
            AddColumn("gsmktg.BOOKINGREQUEST", "Event_Id", c => c.Guid());
            AddColumn("gsmktg.BOOKINGREQUESTSTALL", "Stall_Id", c => c.Guid());
            AddColumn("gsmktg.STALLBOOKING", "Stall_Id", c => c.Guid());
            CreateIndex("gsmktg.BOOKINGREQUEST", "Event_Id");
            CreateIndex("gsmktg.BOOKINGREQUESTSTALL", "Stall_Id");
            CreateIndex("gsmktg.STALLBOOKING", "Stall_Id");
            AddForeignKey("gsmktg.BOOKINGREQUEST", "Event_Id", "gsmktg.EVENT", "Id");
            AddForeignKey("gsmktg.BOOKINGREQUESTSTALL", "Stall_Id", "gsmktg.STALL", "Id");
            AddForeignKey("gsmktg.STALLBOOKING", "Stall_Id", "gsmktg.STALL", "Id");
            DropColumn("gsmktg.STALL", "BookingRequestStall_Id");
            DropColumn("gsmktg.STALL", "StallBooking_Id");
        }
        
        public override void Down()
        {
            AddColumn("gsmktg.STALL", "StallBooking_Id", c => c.Guid());
            AddColumn("gsmktg.STALL", "BookingRequestStall_Id", c => c.Guid());
            DropForeignKey("gsmktg.STALLBOOKING", "Stall_Id", "gsmktg.STALL");
            DropForeignKey("gsmktg.BOOKINGREQUESTSTALL", "Stall_Id", "gsmktg.STALL");
            DropForeignKey("gsmktg.BOOKINGREQUEST", "Event_Id", "gsmktg.EVENT");
            DropIndex("gsmktg.STALLBOOKING", new[] { "Stall_Id" });
            DropIndex("gsmktg.BOOKINGREQUESTSTALL", new[] { "Stall_Id" });
            DropIndex("gsmktg.BOOKINGREQUEST", new[] { "Event_Id" });
            DropColumn("gsmktg.STALLBOOKING", "Stall_Id");
            DropColumn("gsmktg.BOOKINGREQUESTSTALL", "Stall_Id");
            DropColumn("gsmktg.BOOKINGREQUEST", "Event_Id");
            CreateIndex("gsmktg.STALL", "StallBooking_Id");
            CreateIndex("gsmktg.STALL", "BookingRequestStall_Id");
            AddForeignKey("gsmktg.STALL", "StallBooking_Id", "gsmktg.STALLBOOKING", "Id");
            AddForeignKey("gsmktg.STALL", "BookingRequestStall_Id", "gsmktg.BOOKINGREQUESTSTALL", "Id");
        }
    }
}
