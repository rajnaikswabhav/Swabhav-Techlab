namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v64 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("gsmktg.STALL", "Login_Id", "gsmktg.LOGIN");
            DropIndex("gsmktg.STALL", new[] { "Login_Id" });
            AddColumn("gsmktg.STALL", "Partner_Id", c => c.Guid());
            CreateIndex("gsmktg.STALL", "Partner_Id");
            AddForeignKey("gsmktg.STALL", "Partner_Id", "gsmktg.PARTNER", "Id");
            DropColumn("gsmktg.STALL", "Login_Id");
        }
        
        public override void Down()
        {
            AddColumn("gsmktg.STALL", "Login_Id", c => c.Guid());
            DropForeignKey("gsmktg.STALL", "Partner_Id", "gsmktg.PARTNER");
            DropIndex("gsmktg.STALL", new[] { "Partner_Id" });
            DropColumn("gsmktg.STALL", "Partner_Id");
            CreateIndex("gsmktg.STALL", "Login_Id");
            AddForeignKey("gsmktg.STALL", "Login_Id", "gsmktg.LOGIN", "Id");
        }
    }
}
