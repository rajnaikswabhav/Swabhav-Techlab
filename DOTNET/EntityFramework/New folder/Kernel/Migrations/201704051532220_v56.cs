namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v56 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("gsmktg.BOOKING", "FinalAmountTax", c => c.Double(nullable: false));
            AlterColumn("gsmktg.BOOKINGREQUEST", "FinalAmountTax", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("gsmktg.BOOKINGREQUEST", "FinalAmountTax", c => c.Double());
            AlterColumn("gsmktg.BOOKING", "FinalAmountTax", c => c.Double());
        }
    }
}
