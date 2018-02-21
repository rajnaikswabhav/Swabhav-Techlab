namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v54 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.LOGIN", "EmailId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("gsmktg.LOGIN", "EmailId");
        }
    }
}
