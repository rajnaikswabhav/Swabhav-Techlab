namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v76 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.DISCOUNTCOUPON",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CouponCode = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        DiscountText = c.String(),
                        Discount = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        DiscountType_Id = c.Guid(),
                        Event_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.DISCOUNTTYPE", t => t.DiscountType_Id)
                .ForeignKey("gsmktg.EVENT", t => t.Event_Id)
                .Index(t => t.DiscountType_Id)
                .Index(t => t.Event_Id);
            
            CreateTable(
                "gsmktg.DISCOUNTTYPE",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "gsmktg.VISITORBOOKINGEXHIBITORDISCOUNT",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DiscountcoupanCode = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        BookingExhibitorDiscount_Id = c.Guid(),
                        Visitor_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.BOOKINGEXHIBITORDISCOUNT", t => t.BookingExhibitorDiscount_Id)
                .ForeignKey("gsmktg.VISITOR", t => t.Visitor_Id)
                .Index(t => t.BookingExhibitorDiscount_Id)
                .Index(t => t.Visitor_Id);
            
            CreateTable(
                "gsmktg.VISITORDISCOUNTCOUPONMAP",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        DiscountCoupon_Id = c.Guid(),
                        Visitor_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.DISCOUNTCOUPON", t => t.DiscountCoupon_Id)
                .ForeignKey("gsmktg.VISITOR", t => t.Visitor_Id)
                .Index(t => t.DiscountCoupon_Id)
                .Index(t => t.Visitor_Id);
            
            AddColumn("gsmktg.EVENT", "Latitude", c => c.String());
            AddColumn("gsmktg.EVENT", "Longitude", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.VISITORDISCOUNTCOUPONMAP", "Visitor_Id", "gsmktg.VISITOR");
            DropForeignKey("gsmktg.VISITORDISCOUNTCOUPONMAP", "DiscountCoupon_Id", "gsmktg.DISCOUNTCOUPON");
            DropForeignKey("gsmktg.VISITORBOOKINGEXHIBITORDISCOUNT", "Visitor_Id", "gsmktg.VISITOR");
            DropForeignKey("gsmktg.VISITORBOOKINGEXHIBITORDISCOUNT", "BookingExhibitorDiscount_Id", "gsmktg.BOOKINGEXHIBITORDISCOUNT");
            DropForeignKey("gsmktg.DISCOUNTCOUPON", "Event_Id", "gsmktg.EVENT");
            DropForeignKey("gsmktg.DISCOUNTCOUPON", "DiscountType_Id", "gsmktg.DISCOUNTTYPE");
            DropIndex("gsmktg.VISITORDISCOUNTCOUPONMAP", new[] { "Visitor_Id" });
            DropIndex("gsmktg.VISITORDISCOUNTCOUPONMAP", new[] { "DiscountCoupon_Id" });
            DropIndex("gsmktg.VISITORBOOKINGEXHIBITORDISCOUNT", new[] { "Visitor_Id" });
            DropIndex("gsmktg.VISITORBOOKINGEXHIBITORDISCOUNT", new[] { "BookingExhibitorDiscount_Id" });
            DropIndex("gsmktg.DISCOUNTCOUPON", new[] { "Event_Id" });
            DropIndex("gsmktg.DISCOUNTCOUPON", new[] { "DiscountType_Id" });
            DropColumn("gsmktg.EVENT", "Longitude");
            DropColumn("gsmktg.EVENT", "Latitude");
            DropTable("gsmktg.VISITORDISCOUNTCOUPONMAP");
            DropTable("gsmktg.VISITORBOOKINGEXHIBITORDISCOUNT");
            DropTable("gsmktg.DISCOUNTTYPE");
            DropTable("gsmktg.DISCOUNTCOUPON");
        }
    }
}
