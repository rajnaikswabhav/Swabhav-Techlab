namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v34 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.EVENTTICKET",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TokenNumber = c.String(),
                        TicketDate = c.DateTime(nullable: false),
                        NumberOfTicket = c.Int(nullable: false),
                        TotalPriceOfTicket = c.Double(nullable: false),
                        TxnIdForPaymentGateWay = c.String(),
                        PaymentCompleted = c.Boolean(nullable: false),
                        IsPayOnLocation = c.Boolean(nullable: false),
                        ValidityDayCount = c.Int(nullable: false),
                        Status = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        EventTicketType_Id = c.Guid(),
                        Visitor_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.EVENTTICKETTYPE", t => t.EventTicketType_Id)
                .ForeignKey("gsmktg.VISITOR", t => t.Visitor_Id)
                .Index(t => t.EventTicketType_Id)
                .Index(t => t.Visitor_Id);
            
            AddColumn("gsmktg.TICKET", "EventTicketType_Id", c => c.Guid());
            AddColumn("gsmktg.EVENT", "Time", c => c.String());
            CreateIndex("gsmktg.TICKET", "EventTicketType_Id");
            AddForeignKey("gsmktg.TICKET", "EventTicketType_Id", "gsmktg.EVENTTICKETTYPE", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.EVENTTICKET", "Visitor_Id", "gsmktg.VISITOR");
            DropForeignKey("gsmktg.EVENTTICKET", "EventTicketType_Id", "gsmktg.EVENTTICKETTYPE");
            DropForeignKey("gsmktg.TICKET", "EventTicketType_Id", "gsmktg.EVENTTICKETTYPE");
            DropIndex("gsmktg.EVENTTICKET", new[] { "Visitor_Id" });
            DropIndex("gsmktg.EVENTTICKET", new[] { "EventTicketType_Id" });
            DropIndex("gsmktg.TICKET", new[] { "EventTicketType_Id" });
            DropColumn("gsmktg.EVENT", "Time");
            DropColumn("gsmktg.TICKET", "EventTicketType_Id");
            DropTable("gsmktg.EVENTTICKET");
        }
    }
}
