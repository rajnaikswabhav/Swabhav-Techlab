namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v71 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.EXHIBITOR", "ExhibitorStatus_Id", c => c.Guid());
            CreateIndex("gsmktg.EXHIBITOR", "ExhibitorStatus_Id");
            AddForeignKey("gsmktg.EXHIBITOR", "ExhibitorStatus_Id", "gsmktg.EXHIBITORSTATUS", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.EXHIBITOR", "ExhibitorStatus_Id", "gsmktg.EXHIBITORSTATUS");
            DropIndex("gsmktg.EXHIBITOR", new[] { "ExhibitorStatus_Id" });
            DropColumn("gsmktg.EXHIBITOR", "ExhibitorStatus_Id");
        }
    }
}
