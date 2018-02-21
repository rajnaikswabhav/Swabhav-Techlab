namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v68 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.EXHIBITORINDUSTRYTYPE", "Color", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("gsmktg.EXHIBITORINDUSTRYTYPE", "Color");
        }
    }
}
