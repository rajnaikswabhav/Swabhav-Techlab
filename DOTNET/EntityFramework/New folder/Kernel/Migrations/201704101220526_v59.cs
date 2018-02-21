namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v59 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.PAYMENTDETAILS", "BookingInstallment_Id", c => c.Guid());
            CreateIndex("gsmktg.PAYMENTDETAILS", "BookingInstallment_Id");
            AddForeignKey("gsmktg.PAYMENTDETAILS", "BookingInstallment_Id", "gsmktg.BOOKINGINSTALLNENT", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.PAYMENTDETAILS", "BookingInstallment_Id", "gsmktg.BOOKINGINSTALLNENT");
            DropIndex("gsmktg.PAYMENTDETAILS", new[] { "BookingInstallment_Id" });
            DropColumn("gsmktg.PAYMENTDETAILS", "BookingInstallment_Id");
        }
    }
}
