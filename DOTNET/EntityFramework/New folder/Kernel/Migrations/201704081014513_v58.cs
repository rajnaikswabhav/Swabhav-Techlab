namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v58 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.BOOKINGINSTALLNENT",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Percent = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Booking_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.BOOKING", t => t.Booking_Id)
                .Index(t => t.Booking_Id);
            
            CreateTable(
                "gsmktg.BOOKINGREQUESTINSTALLMENT",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Percent = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        BookingRequest_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.BOOKINGREQUEST", t => t.BookingRequest_Id)
                .Index(t => t.BookingRequest_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.BOOKINGREQUESTINSTALLMENT", "BookingRequest_Id", "gsmktg.BOOKINGREQUEST");
            DropForeignKey("gsmktg.BOOKINGINSTALLNENT", "Booking_Id", "gsmktg.BOOKING");
            DropIndex("gsmktg.BOOKINGREQUESTINSTALLMENT", new[] { "BookingRequest_Id" });
            DropIndex("gsmktg.BOOKINGINSTALLNENT", new[] { "Booking_Id" });
            DropTable("gsmktg.BOOKINGREQUESTINSTALLMENT");
            DropTable("gsmktg.BOOKINGINSTALLNENT");
        }
    }
}
