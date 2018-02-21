namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v33 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("gsmktg.StallTransactions", "Booking_Id", "gsmktg.BOOKING");
            DropIndex("gsmktg.StallTransactions", new[] { "Booking_Id" });
            AddColumn("gsmktg.PAYMENT", "IsPaymentDone", c => c.Boolean(nullable: false));
            AddColumn("gsmktg.StallTransactions", "Payment_Id", c => c.Guid());
            CreateIndex("gsmktg.StallTransactions", "Payment_Id");
            AddForeignKey("gsmktg.StallTransactions", "Payment_Id", "gsmktg.PAYMENT", "Id");
            DropColumn("gsmktg.StallTransactions", "Booking_Id");
        }
        
        public override void Down()
        {
            AddColumn("gsmktg.StallTransactions", "Booking_Id", c => c.Guid());
            DropForeignKey("gsmktg.StallTransactions", "Payment_Id", "gsmktg.PAYMENT");
            DropIndex("gsmktg.StallTransactions", new[] { "Payment_Id" });
            DropColumn("gsmktg.StallTransactions", "Payment_Id");
            DropColumn("gsmktg.PAYMENT", "IsPaymentDone");
            CreateIndex("gsmktg.StallTransactions", "Booking_Id");
            AddForeignKey("gsmktg.StallTransactions", "Booking_Id", "gsmktg.BOOKING", "Id");
        }
    }
}
