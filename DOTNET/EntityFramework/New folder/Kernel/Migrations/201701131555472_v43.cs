namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v43 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.BOOKING", "LoginId_Id", c => c.Guid());
            CreateIndex("gsmktg.BOOKING", "LoginId_Id");
            AddForeignKey("gsmktg.BOOKING", "LoginId_Id", "gsmktg.LOGIN", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.BOOKING", "LoginId_Id", "gsmktg.LOGIN");
            DropIndex("gsmktg.BOOKING", new[] { "LoginId_Id" });
            DropColumn("gsmktg.BOOKING", "LoginId_Id");
        }
    }
}
