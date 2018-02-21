namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v35 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.VISITOR", "FacebookId", c => c.String());
            AddColumn("gsmktg.EVENTTICKET", "PhysicalTicketSerialNo", c => c.String());
            AlterColumn("gsmktg.VISITOR", "Gender", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("gsmktg.VISITOR", "Gender", c => c.Int(nullable: false));
            DropColumn("gsmktg.EVENTTICKET", "PhysicalTicketSerialNo");
            DropColumn("gsmktg.VISITOR", "FacebookId");
        }
    }
}
