namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v26 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.BOOKING",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Action = c.String(),
                        Status = c.String(),
                        BookingDate = c.DateTime(nullable: false),
                        BookingAccepted = c.Boolean(nullable: false),
                        IsPaymentCompleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Exhibitor_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.EXHIBITOR", t => t.Exhibitor_Id)
                .Index(t => t.Exhibitor_Id);
            
            CreateTable(
                "gsmktg.BOOKINGREQUEST",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Action = c.String(),
                        Confirmed = c.Boolean(nullable: false),
                        Status = c.String(),
                        BookingRequestDate = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Booking_Id = c.Guid(),
                        PreviousBookingrequest_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.BOOKING", t => t.Booking_Id)
                .ForeignKey("gsmktg.BOOKINGREQUEST", t => t.PreviousBookingrequest_Id)
                .Index(t => t.Booking_Id)
                .Index(t => t.PreviousBookingrequest_Id);
            
            CreateTable(
                "gsmktg.BOOKINGREQUESTSTALL",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        BookingRequest_Id = c.Guid(),
                        Event_Id = c.Guid(),
                        Exhibition_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.BOOKINGREQUEST", t => t.BookingRequest_Id)
                .ForeignKey("gsmktg.EVENT", t => t.Event_Id)
                .ForeignKey("gsmktg.EXHIBITION", t => t.Exhibition_Id)
                .Index(t => t.BookingRequest_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Exhibition_Id);
            
            CreateTable(
                "gsmktg.STALLBOOKING",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Booking_Id = c.Guid(),
                        Event_Id = c.Guid(),
                        Exhibition_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.BOOKING", t => t.Booking_Id)
                .ForeignKey("gsmktg.EVENT", t => t.Event_Id)
                .ForeignKey("gsmktg.EXHIBITION", t => t.Exhibition_Id)
                .Index(t => t.Booking_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Exhibition_Id);
            
            AddColumn("gsmktg.STALL", "BookingRequestStall_Id", c => c.Guid());
            AddColumn("gsmktg.STALL", "StallBooking_Id", c => c.Guid());
            CreateIndex("gsmktg.STALL", "BookingRequestStall_Id");
            CreateIndex("gsmktg.STALL", "StallBooking_Id");
            AddForeignKey("gsmktg.STALL", "BookingRequestStall_Id", "gsmktg.BOOKINGREQUESTSTALL", "Id");
            AddForeignKey("gsmktg.STALL", "StallBooking_Id", "gsmktg.STALLBOOKING", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.STALL", "StallBooking_Id", "gsmktg.STALLBOOKING");
            DropForeignKey("gsmktg.STALLBOOKING", "Exhibition_Id", "gsmktg.EXHIBITION");
            DropForeignKey("gsmktg.STALLBOOKING", "Event_Id", "gsmktg.EVENT");
            DropForeignKey("gsmktg.STALLBOOKING", "Booking_Id", "gsmktg.BOOKING");
            DropForeignKey("gsmktg.STALL", "BookingRequestStall_Id", "gsmktg.BOOKINGREQUESTSTALL");
            DropForeignKey("gsmktg.BOOKINGREQUESTSTALL", "Exhibition_Id", "gsmktg.EXHIBITION");
            DropForeignKey("gsmktg.BOOKINGREQUESTSTALL", "Event_Id", "gsmktg.EVENT");
            DropForeignKey("gsmktg.BOOKINGREQUESTSTALL", "BookingRequest_Id", "gsmktg.BOOKINGREQUEST");
            DropForeignKey("gsmktg.BOOKINGREQUEST", "PreviousBookingrequest_Id", "gsmktg.BOOKINGREQUEST");
            DropForeignKey("gsmktg.BOOKINGREQUEST", "Booking_Id", "gsmktg.BOOKING");
            DropForeignKey("gsmktg.BOOKING", "Exhibitor_Id", "gsmktg.EXHIBITOR");
            DropIndex("gsmktg.STALLBOOKING", new[] { "Exhibition_Id" });
            DropIndex("gsmktg.STALLBOOKING", new[] { "Event_Id" });
            DropIndex("gsmktg.STALLBOOKING", new[] { "Booking_Id" });
            DropIndex("gsmktg.BOOKINGREQUESTSTALL", new[] { "Exhibition_Id" });
            DropIndex("gsmktg.BOOKINGREQUESTSTALL", new[] { "Event_Id" });
            DropIndex("gsmktg.BOOKINGREQUESTSTALL", new[] { "BookingRequest_Id" });
            DropIndex("gsmktg.BOOKINGREQUEST", new[] { "PreviousBookingrequest_Id" });
            DropIndex("gsmktg.BOOKINGREQUEST", new[] { "Booking_Id" });
            DropIndex("gsmktg.BOOKING", new[] { "Exhibitor_Id" });
            DropIndex("gsmktg.STALL", new[] { "StallBooking_Id" });
            DropIndex("gsmktg.STALL", new[] { "BookingRequestStall_Id" });
            DropColumn("gsmktg.STALL", "StallBooking_Id");
            DropColumn("gsmktg.STALL", "BookingRequestStall_Id");
            DropTable("gsmktg.STALLBOOKING");
            DropTable("gsmktg.BOOKINGREQUESTSTALL");
            DropTable("gsmktg.BOOKINGREQUEST");
            DropTable("gsmktg.BOOKING");
        }
    }
}
