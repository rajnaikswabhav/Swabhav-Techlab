namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v24 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("gsmktg.STALL", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("gsmktg.STALL", "Price", c => c.Int(nullable: false));
        }
    }
}
