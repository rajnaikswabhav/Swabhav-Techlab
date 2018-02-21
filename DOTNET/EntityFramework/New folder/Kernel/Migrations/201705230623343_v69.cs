namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v69 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.COUNTRY", "Color", c => c.String());
            AddColumn("gsmktg.STALL", "Country_Id", c => c.Guid());
            AddColumn("gsmktg.STALL", "State_Id", c => c.Guid());
            AddColumn("gsmktg.STATE", "Color", c => c.String());
            CreateIndex("gsmktg.STALL", "Country_Id");
            CreateIndex("gsmktg.STALL", "State_Id");
            AddForeignKey("gsmktg.STALL", "Country_Id", "gsmktg.COUNTRY", "Id");
            AddForeignKey("gsmktg.STALL", "State_Id", "gsmktg.STATE", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.STALL", "State_Id", "gsmktg.STATE");
            DropForeignKey("gsmktg.STALL", "Country_Id", "gsmktg.COUNTRY");
            DropIndex("gsmktg.STALL", new[] { "State_Id" });
            DropIndex("gsmktg.STALL", new[] { "Country_Id" });
            DropColumn("gsmktg.STATE", "Color");
            DropColumn("gsmktg.STALL", "State_Id");
            DropColumn("gsmktg.STALL", "Country_Id");
            DropColumn("gsmktg.COUNTRY", "Color");
        }
    }
}
