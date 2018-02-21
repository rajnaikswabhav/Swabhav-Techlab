namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("gsmktg.VISITOR", "DateOfBirth", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("gsmktg.VISITOR", "DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
