namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v55 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.CHANGEPASSWORDREQUEST",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Login_Id = c.Guid(),
                        Role_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.LOGIN", t => t.Login_Id)
                .ForeignKey("gsmktg.ROLE", t => t.Role_Id)
                .Index(t => t.Login_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "gsmktg.PAYMENTDETAILS",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AmountPaid = c.Double(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        PaymentID = c.String(),
                        PaymentMode = c.Int(nullable: false),
                        ChequeNo = c.String(),
                        BankName = c.String(),
                        BankBranch = c.String(),
                        UTRNo = c.String(),
                        PaymentStatus = c.Int(nullable: false),
                        IsPaymentApprove = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Booking_Id = c.Guid(),
                        Event_Id = c.Guid(),
                        Exhibitor_Id = c.Guid(),
                        Login_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.BOOKING", t => t.Booking_Id)
                .ForeignKey("gsmktg.EVENT", t => t.Event_Id)
                .ForeignKey("gsmktg.EXHIBITOR", t => t.Exhibitor_Id)
                .ForeignKey("gsmktg.LOGIN", t => t.Login_Id)
                .Index(t => t.Booking_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Exhibitor_Id)
                .Index(t => t.Login_Id);
            
            CreateTable(
                "gsmktg.USERROLE",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("gsmktg.BOOKING", "PaymentStatus", c => c.String());
            AddColumn("gsmktg.BOOKING", "FinalAmountTax", c => c.Double());
            AddColumn("gsmktg.BOOKING", "DiscountPercent", c => c.Int());
            AddColumn("gsmktg.BOOKING", "DiscountAmount", c => c.Int());
            AddColumn("gsmktg.LOGIN", "RoleName", c => c.String());
            AddColumn("gsmktg.LOGIN", "Role_Id", c => c.Guid());
            AddColumn("gsmktg.BOOKINGREQUEST", "FinalAmountTax", c => c.Double());
            AddColumn("gsmktg.ROLE", "RoleName", c => c.String());
            CreateIndex("gsmktg.LOGIN", "Role_Id");
            AddForeignKey("gsmktg.LOGIN", "Role_Id", "gsmktg.ROLE", "Id");
            DropColumn("gsmktg.BOOKING", "Discount");
            DropColumn("gsmktg.LOGIN", "Role");
            DropColumn("gsmktg.ROLE", "Name");
        }
        
        public override void Down()
        {
            AddColumn("gsmktg.ROLE", "Name", c => c.String());
            AddColumn("gsmktg.LOGIN", "Role", c => c.String());
            AddColumn("gsmktg.BOOKING", "Discount", c => c.Int(nullable: false));
            DropForeignKey("gsmktg.PAYMENTDETAILS", "Login_Id", "gsmktg.LOGIN");
            DropForeignKey("gsmktg.PAYMENTDETAILS", "Exhibitor_Id", "gsmktg.EXHIBITOR");
            DropForeignKey("gsmktg.PAYMENTDETAILS", "Event_Id", "gsmktg.EVENT");
            DropForeignKey("gsmktg.PAYMENTDETAILS", "Booking_Id", "gsmktg.BOOKING");
            DropForeignKey("gsmktg.CHANGEPASSWORDREQUEST", "Role_Id", "gsmktg.ROLE");
            DropForeignKey("gsmktg.CHANGEPASSWORDREQUEST", "Login_Id", "gsmktg.LOGIN");
            DropForeignKey("gsmktg.LOGIN", "Role_Id", "gsmktg.ROLE");
            DropIndex("gsmktg.PAYMENTDETAILS", new[] { "Login_Id" });
            DropIndex("gsmktg.PAYMENTDETAILS", new[] { "Exhibitor_Id" });
            DropIndex("gsmktg.PAYMENTDETAILS", new[] { "Event_Id" });
            DropIndex("gsmktg.PAYMENTDETAILS", new[] { "Booking_Id" });
            DropIndex("gsmktg.CHANGEPASSWORDREQUEST", new[] { "Role_Id" });
            DropIndex("gsmktg.CHANGEPASSWORDREQUEST", new[] { "Login_Id" });
            DropIndex("gsmktg.LOGIN", new[] { "Role_Id" });
            DropColumn("gsmktg.ROLE", "RoleName");
            DropColumn("gsmktg.BOOKINGREQUEST", "FinalAmountTax");
            DropColumn("gsmktg.LOGIN", "Role_Id");
            DropColumn("gsmktg.LOGIN", "RoleName");
            DropColumn("gsmktg.BOOKING", "DiscountAmount");
            DropColumn("gsmktg.BOOKING", "DiscountPercent");
            DropColumn("gsmktg.BOOKING", "FinalAmountTax");
            DropColumn("gsmktg.BOOKING", "PaymentStatus");
            DropTable("gsmktg.USERROLE");
            DropTable("gsmktg.PAYMENTDETAILS");
            DropTable("gsmktg.CHANGEPASSWORDREQUEST");
        }
    }
}
