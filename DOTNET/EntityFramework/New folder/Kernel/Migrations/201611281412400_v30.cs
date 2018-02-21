namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v30 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.PAYMENT",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AmountPaid = c.Double(nullable: false),
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
            
            AddColumn("gsmktg.BOOKING", "TotalAmount", c => c.Double(nullable: false));
            AddColumn("gsmktg.BOOKINGREQUEST", "TotalAmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.PAYMENT", "Booking_Id", "gsmktg.BOOKING");
            DropIndex("gsmktg.PAYMENT", new[] { "Booking_Id" });
            DropColumn("gsmktg.BOOKINGREQUEST", "TotalAmount");
            DropColumn("gsmktg.BOOKING", "TotalAmount");
            DropTable("gsmktg.PAYMENT");
        }
    }
}
