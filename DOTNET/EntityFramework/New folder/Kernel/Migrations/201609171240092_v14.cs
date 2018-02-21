namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.EVENT",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        BookingStartDate = c.DateTime(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Venue_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.VENUE", t => t.Venue_Id)
                .Index(t => t.Venue_Id);
            
            CreateTable(
                "gsmktg.EVENTEXHIBITIONMAP",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Event_Id = c.Guid(),
                        Exhibition_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.EVENT", t => t.Event_Id)
                .ForeignKey("gsmktg.EXHIBITION", t => t.Exhibition_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Exhibition_Id);
            
            CreateTable(
                "gsmktg.EVENTTICKETTYPE",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Cost = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Event_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.EVENT", t => t.Event_Id)
                .Index(t => t.Event_Id);
            
            CreateTable(
                "gsmktg.EXHIBITORMAP",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        EventExhibitionMap_Id = c.Guid(),
                        Exhibitor_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.EVENTEXHIBITIONMAP", t => t.EventExhibitionMap_Id)
                .ForeignKey("gsmktg.EXHIBITOR", t => t.Exhibitor_Id)
                .Index(t => t.EventExhibitionMap_Id)
                .Index(t => t.Exhibitor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.EXHIBITORMAP", "Exhibitor_Id", "gsmktg.EXHIBITOR");
            DropForeignKey("gsmktg.EXHIBITORMAP", "EventExhibitionMap_Id", "gsmktg.EVENTEXHIBITIONMAP");
            DropForeignKey("gsmktg.EVENTTICKETTYPE", "Event_Id", "gsmktg.EVENT");
            DropForeignKey("gsmktg.EVENTEXHIBITIONMAP", "Exhibition_Id", "gsmktg.EXHIBITION");
            DropForeignKey("gsmktg.EVENTEXHIBITIONMAP", "Event_Id", "gsmktg.EVENT");
            DropForeignKey("gsmktg.EVENT", "Venue_Id", "gsmktg.VENUE");
            DropIndex("gsmktg.EXHIBITORMAP", new[] { "Exhibitor_Id" });
            DropIndex("gsmktg.EXHIBITORMAP", new[] { "EventExhibitionMap_Id" });
            DropIndex("gsmktg.EVENTTICKETTYPE", new[] { "Event_Id" });
            DropIndex("gsmktg.EVENTEXHIBITIONMAP", new[] { "Exhibition_Id" });
            DropIndex("gsmktg.EVENTEXHIBITIONMAP", new[] { "Event_Id" });
            DropIndex("gsmktg.EVENT", new[] { "Venue_Id" });
            DropTable("gsmktg.EXHIBITORMAP");
            DropTable("gsmktg.EVENTTICKETTYPE");
            DropTable("gsmktg.EVENTEXHIBITIONMAP");
            DropTable("gsmktg.EVENT");
        }
    }
}
