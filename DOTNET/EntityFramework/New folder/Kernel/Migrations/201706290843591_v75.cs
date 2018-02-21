namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v75 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.BOOKINGEXHIBITORDISCOUNT",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Heading = c.String(),
                        Description = c.String(),
                        IsApproved = c.Boolean(nullable: false),
                        IsRejected = c.Boolean(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.BOOKINGEXHIBITORDISCOUNT", "Booking_Id", "gsmktg.BOOKING");
            DropIndex("gsmktg.BOOKINGEXHIBITORDISCOUNT", new[] { "Booking_Id" });
            DropTable("gsmktg.BOOKINGEXHIBITORDISCOUNT");
        }
    }
}
