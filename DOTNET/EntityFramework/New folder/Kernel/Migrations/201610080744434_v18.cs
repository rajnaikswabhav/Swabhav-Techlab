namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v18 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("gsmktg.TICKET", "TotalPriceOfTicket", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("gsmktg.TICKET", "TotalPriceOfTicket", c => c.Int(nullable: false));
        }
    }
}
