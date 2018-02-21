namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v50 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.VISITORPAYMENTDETAILS",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Double(nullable: false),
                        FileName = c.String(),
                        PurchaseDate = c.DateTime(nullable: false),
                        PaymentMode = c.Int(nullable: false),
                        PointsEarned = c.Int(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Event_Id = c.Guid(),
                        Exhibitor_Id = c.Guid(),
                        Section_Id = c.Guid(),
                        Visitor_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.EVENT", t => t.Event_Id)
                .ForeignKey("gsmktg.EXHIBITOR", t => t.Exhibitor_Id)
                .ForeignKey("gsmktg.SECTION", t => t.Section_Id)
                .ForeignKey("gsmktg.VISITOR", t => t.Visitor_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Exhibitor_Id)
                .Index(t => t.Section_Id)
                .Index(t => t.Visitor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.VISITORPAYMENTDETAILS", "Visitor_Id", "gsmktg.VISITOR");
            DropForeignKey("gsmktg.VISITORPAYMENTDETAILS", "Section_Id", "gsmktg.SECTION");
            DropForeignKey("gsmktg.VISITORPAYMENTDETAILS", "Exhibitor_Id", "gsmktg.EXHIBITOR");
            DropForeignKey("gsmktg.VISITORPAYMENTDETAILS", "Event_Id", "gsmktg.EVENT");
            DropIndex("gsmktg.VISITORPAYMENTDETAILS", new[] { "Visitor_Id" });
            DropIndex("gsmktg.VISITORPAYMENTDETAILS", new[] { "Section_Id" });
            DropIndex("gsmktg.VISITORPAYMENTDETAILS", new[] { "Exhibitor_Id" });
            DropIndex("gsmktg.VISITORPAYMENTDETAILS", new[] { "Event_Id" });
            DropTable("gsmktg.VISITORPAYMENTDETAILS");
        }
    }
}
