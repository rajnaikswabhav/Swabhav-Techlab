namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v45 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.VISITORCARDPAYMENT",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        MobileNo = c.String(),
                        ExhibitorName = c.String(),
                        TransactionId = c.String(),
                        Invoice = c.String(),
                        Amount = c.Int(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Event_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.EVENT", t => t.Event_Id)
                .Index(t => t.Event_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.VISITORCARDPAYMENT", "Event_Id", "gsmktg.EVENT");
            DropIndex("gsmktg.VISITORCARDPAYMENT", new[] { "Event_Id" });
            DropTable("gsmktg.VISITORCARDPAYMENT");
        }
    }
}
