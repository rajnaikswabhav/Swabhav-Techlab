namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.EVENTEXHIBITIONMAP", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("gsmktg.EVENTEXHIBITIONMAP", "EndDate", c => c.DateTime(nullable: false));
            DropColumn("gsmktg.EVENT", "StartDate");
            DropColumn("gsmktg.EVENT", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("gsmktg.EVENT", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("gsmktg.EVENT", "StartDate", c => c.DateTime(nullable: false));
            DropColumn("gsmktg.EVENTEXHIBITIONMAP", "EndDate");
            DropColumn("gsmktg.EVENTEXHIBITIONMAP", "StartDate");
        }
    }
}
