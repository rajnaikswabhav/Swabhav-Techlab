namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v46 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.EXHIBITOR", "Age", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("gsmktg.EXHIBITOR", "Age");
        }
    }
}
