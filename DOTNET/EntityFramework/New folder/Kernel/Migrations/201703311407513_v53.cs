namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v53 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.STALL", "IsRequested", c => c.Boolean(nullable: false));
            AddColumn("gsmktg.BOOKING", "Discount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("gsmktg.BOOKING", "Discount");
            DropColumn("gsmktg.STALL", "IsRequested");
        }
    }
}
