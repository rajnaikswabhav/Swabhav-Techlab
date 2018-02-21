namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.TICKETTYPE", "BusinessHrs", c => c.Int(nullable: false));
            AddColumn("gsmktg.TICKETTYPE", "NonBusinessHrs", c => c.Int(nullable: false));
            AddColumn("gsmktg.TICKETTYPE", "BusinessHrsDiscount", c => c.Int(nullable: false));
            AddColumn("gsmktg.TICKETTYPE", "NonBusinessHrsDiscount", c => c.Int(nullable: false));
            DropColumn("gsmktg.TICKETTYPE", "Price");
        }
        
        public override void Down()
        {
            AddColumn("gsmktg.TICKETTYPE", "Price", c => c.Int(nullable: false));
            DropColumn("gsmktg.TICKETTYPE", "NonBusinessHrsDiscount");
            DropColumn("gsmktg.TICKETTYPE", "BusinessHrsDiscount");
            DropColumn("gsmktg.TICKETTYPE", "NonBusinessHrs");
            DropColumn("gsmktg.TICKETTYPE", "BusinessHrs");
        }
    }
}
