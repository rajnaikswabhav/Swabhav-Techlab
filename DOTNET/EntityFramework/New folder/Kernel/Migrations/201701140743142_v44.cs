namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v44 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.BOOKINGSALESPERSONMAP",
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
                        Login_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.BOOKING", t => t.Booking_Id)
                .ForeignKey("gsmktg.LOGIN", t => t.Login_Id)
                .Index(t => t.Booking_Id)
                .Index(t => t.Login_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.BOOKINGSALESPERSONMAP", "Login_Id", "gsmktg.LOGIN");
            DropForeignKey("gsmktg.BOOKINGSALESPERSONMAP", "Booking_Id", "gsmktg.BOOKING");
            DropIndex("gsmktg.BOOKINGSALESPERSONMAP", new[] { "Login_Id" });
            DropIndex("gsmktg.BOOKINGSALESPERSONMAP", new[] { "Booking_Id" });
            DropTable("gsmktg.BOOKINGSALESPERSONMAP");
        }
    }
}
