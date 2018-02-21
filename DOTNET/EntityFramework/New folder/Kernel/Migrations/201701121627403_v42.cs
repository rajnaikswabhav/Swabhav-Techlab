namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v42 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.STALL", "Event_Id", c => c.Guid());
            CreateIndex("gsmktg.STALL", "Event_Id");
            AddForeignKey("gsmktg.STALL", "Event_Id", "gsmktg.EVENT", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.STALL", "Event_Id", "gsmktg.EVENT");
            DropIndex("gsmktg.STALL", new[] { "Event_Id" });
            DropColumn("gsmktg.STALL", "Event_Id");
        }
    }
}
