namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.TRANSACTION", "Ticket_Id", c => c.Guid());
            CreateIndex("gsmktg.TRANSACTION", "Ticket_Id");
            AddForeignKey("gsmktg.TRANSACTION", "Ticket_Id", "gsmktg.TICKET", "Id");
            DropColumn("gsmktg.TRANSACTION", "TicketId");
        }
        
        public override void Down()
        {
            AddColumn("gsmktg.TRANSACTION", "TicketId", c => c.String());
            DropForeignKey("gsmktg.TRANSACTION", "Ticket_Id", "gsmktg.TICKET");
            DropIndex("gsmktg.TRANSACTION", new[] { "Ticket_Id" });
            DropColumn("gsmktg.TRANSACTION", "Ticket_Id");
        }
    }
}
