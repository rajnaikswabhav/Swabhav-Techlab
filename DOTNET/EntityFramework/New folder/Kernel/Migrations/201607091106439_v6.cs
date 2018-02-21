namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.EXHIBITION", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("gsmktg.EXHIBITION", "Address");
        }
    }
}
