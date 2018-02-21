namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v37 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("gsmktg.LOGINSESSION", "StartTime", c => c.DateTime());
            AlterColumn("gsmktg.LOGINSESSION", "EndTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("gsmktg.LOGINSESSION", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("gsmktg.LOGINSESSION", "StartTime", c => c.DateTime(nullable: false));
        }
    }
}
