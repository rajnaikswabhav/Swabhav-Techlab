namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v60 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.STALL", "Login_Id", c => c.Guid());
            AddColumn("gsmktg.LOGIN", "Color", c => c.String());
            CreateIndex("gsmktg.STALL", "Login_Id");
            AddForeignKey("gsmktg.STALL", "Login_Id", "gsmktg.LOGIN", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.STALL", "Login_Id", "gsmktg.LOGIN");
            DropIndex("gsmktg.STALL", new[] { "Login_Id" });
            DropColumn("gsmktg.LOGIN", "Color");
            DropColumn("gsmktg.STALL", "Login_Id");
        }
    }
}
