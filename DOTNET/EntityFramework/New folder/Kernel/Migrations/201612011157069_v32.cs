namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v32 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.StallTransactions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Status = c.String(),
                        Amount = c.Double(nullable: false),
                        PgTransactionNo = c.String(),
                        IssuerRefNo = c.String(),
                        AuthIdCode = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PgResponseCode = c.String(),
                        AddressZip = c.String(),
                        CitrusTxnId = c.String(),
                        TxMsg = c.String(),
                        PaymentMode = c.String(),
                        TransactionDate = c.DateTime(nullable: false),
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
            
            AddColumn("gsmktg.PAYMENT", "IsPayOnLocation", c => c.Boolean(nullable: false));
            AddColumn("gsmktg.PAYMENT", "TxnIdForPaymentGateWay", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.StallTransactions", "Booking_Id", "gsmktg.BOOKING");
            DropIndex("gsmktg.StallTransactions", new[] { "Booking_Id" });
            DropColumn("gsmktg.PAYMENT", "TxnIdForPaymentGateWay");
            DropColumn("gsmktg.PAYMENT", "IsPayOnLocation");
            DropTable("gsmktg.StallTransactions");
        }
    }
}
