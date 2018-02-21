namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v51 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.BOOKINGREQUESTSALESPERSONMAP",
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
                        Login_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.BOOKINGREQUEST", t => t.BookingRequest_Id)
                .ForeignKey("gsmktg.LOGIN", t => t.Login_Id)
                .Index(t => t.BookingRequest_Id)
                .Index(t => t.Login_Id);
            
            CreateTable(
                "gsmktg.EVENTLEADEXHIBITORMAP",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Event_Id = c.Guid(),
                        Exhibitor_Id = c.Guid(),
                        Login_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.EVENT", t => t.Event_Id)
                .ForeignKey("gsmktg.EXHIBITOR", t => t.Exhibitor_Id)
                .ForeignKey("gsmktg.LOGIN", t => t.Login_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Exhibitor_Id)
                .Index(t => t.Login_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.EVENTLEADEXHIBITORMAP", "Login_Id", "gsmktg.LOGIN");
            DropForeignKey("gsmktg.EVENTLEADEXHIBITORMAP", "Exhibitor_Id", "gsmktg.EXHIBITOR");
            DropForeignKey("gsmktg.EVENTLEADEXHIBITORMAP", "Event_Id", "gsmktg.EVENT");
            DropForeignKey("gsmktg.BOOKINGREQUESTSALESPERSONMAP", "Login_Id", "gsmktg.LOGIN");
            DropForeignKey("gsmktg.BOOKINGREQUESTSALESPERSONMAP", "BookingRequest_Id", "gsmktg.BOOKINGREQUEST");
            DropIndex("gsmktg.EVENTLEADEXHIBITORMAP", new[] { "Login_Id" });
            DropIndex("gsmktg.EVENTLEADEXHIBITORMAP", new[] { "Exhibitor_Id" });
            DropIndex("gsmktg.EVENTLEADEXHIBITORMAP", new[] { "Event_Id" });
            DropIndex("gsmktg.BOOKINGREQUESTSALESPERSONMAP", new[] { "Login_Id" });
            DropIndex("gsmktg.BOOKINGREQUESTSALESPERSONMAP", new[] { "BookingRequest_Id" });
            DropTable("gsmktg.EVENTLEADEXHIBITORMAP");
            DropTable("gsmktg.BOOKINGREQUESTSALESPERSONMAP");
        }
    }
}
