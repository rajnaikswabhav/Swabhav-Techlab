namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v52 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("gsmktg.EVENTLEADEXHIBITORMAP", "Exhibitor_Id", "gsmktg.EXHIBITOR");
            DropIndex("gsmktg.EVENTLEADEXHIBITORMAP", new[] { "Exhibitor_Id" });
            AlterColumn("gsmktg.EVENTLEADEXHIBITORMAP", "Exhibitor_Id", c => c.Guid(nullable: false));
            CreateIndex("gsmktg.EVENTLEADEXHIBITORMAP", "Exhibitor_Id");
            AddForeignKey("gsmktg.EVENTLEADEXHIBITORMAP", "Exhibitor_Id", "gsmktg.EXHIBITOR", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.EVENTLEADEXHIBITORMAP", "Exhibitor_Id", "gsmktg.EXHIBITOR");
            DropIndex("gsmktg.EVENTLEADEXHIBITORMAP", new[] { "Exhibitor_Id" });
            AlterColumn("gsmktg.EVENTLEADEXHIBITORMAP", "Exhibitor_Id", c => c.Guid());
            CreateIndex("gsmktg.EVENTLEADEXHIBITORMAP", "Exhibitor_Id");
            AddForeignKey("gsmktg.EVENTLEADEXHIBITORMAP", "Exhibitor_Id", "gsmktg.EXHIBITOR", "Id");
        }
    }
}
