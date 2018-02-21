namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v28 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.BOOKINGREQUEST", "Exhibitor_Id", c => c.Guid());
            CreateIndex("gsmktg.BOOKINGREQUEST", "Exhibitor_Id");
            AddForeignKey("gsmktg.BOOKINGREQUEST", "Exhibitor_Id", "gsmktg.EXHIBITOR", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.BOOKINGREQUEST", "Exhibitor_Id", "gsmktg.EXHIBITOR");
            DropIndex("gsmktg.BOOKINGREQUEST", new[] { "Exhibitor_Id" });
            DropColumn("gsmktg.BOOKINGREQUEST", "Exhibitor_Id");
        }
    }
}
