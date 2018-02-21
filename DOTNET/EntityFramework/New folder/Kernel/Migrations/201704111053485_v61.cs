namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v61 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.EVENT", "IsTicketingActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("gsmktg.EVENT", "IsTicketingActive");
        }
    }
}
