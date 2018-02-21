namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v57 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.LOGIN", "ParentLogin_Id", c => c.Guid());
            CreateIndex("gsmktg.LOGIN", "ParentLogin_Id");
            AddForeignKey("gsmktg.LOGIN", "ParentLogin_Id", "gsmktg.LOGIN", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.LOGIN", "ParentLogin_Id", "gsmktg.LOGIN");
            DropIndex("gsmktg.LOGIN", new[] { "ParentLogin_Id" });
            DropColumn("gsmktg.LOGIN", "ParentLogin_Id");
        }
    }
}
