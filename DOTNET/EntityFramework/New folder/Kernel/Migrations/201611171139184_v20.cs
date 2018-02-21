namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.EVENTTICKETTYPE", "BusinessHrs", c => c.Int(nullable: false));
            AddColumn("gsmktg.EVENTTICKETTYPE", "NonBusinessHrs", c => c.Int(nullable: false));
            AddColumn("gsmktg.EVENTTICKETTYPE", "BusinessHrsDiscount", c => c.Int(nullable: false));
            AddColumn("gsmktg.EVENTTICKETTYPE", "NonBusinessHrsDiscount", c => c.Int(nullable: false));
            AddColumn("gsmktg.EVENTTICKETTYPE", "NumberOfDaysIncluded", c => c.Int(nullable: false));
            DropColumn("gsmktg.EVENTTICKETTYPE", "Cost");
        }
        
        public override void Down()
        {
            AddColumn("gsmktg.EVENTTICKETTYPE", "Cost", c => c.Int(nullable: false));
            DropColumn("gsmktg.EVENTTICKETTYPE", "NumberOfDaysIncluded");
            DropColumn("gsmktg.EVENTTICKETTYPE", "NonBusinessHrsDiscount");
            DropColumn("gsmktg.EVENTTICKETTYPE", "BusinessHrsDiscount");
            DropColumn("gsmktg.EVENTTICKETTYPE", "NonBusinessHrs");
            DropColumn("gsmktg.EVENTTICKETTYPE", "BusinessHrs");
        }
    }
}
