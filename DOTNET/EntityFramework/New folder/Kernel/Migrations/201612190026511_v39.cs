namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v39 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.STALL", "StallSize", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("gsmktg.STALL", "StallSize");
        }
    }
}
