namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v48 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.EXHIBITORFEEDBACK",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Satisfaction = c.Int(nullable: false),
                        Objective = c.Int(nullable: false),
                        TargetAudience = c.Boolean(nullable: false),
                        QualityOfVisitor = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Event_Id = c.Guid(),
                        Exhibitor_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.EVENT", t => t.Event_Id)
                .ForeignKey("gsmktg.EXHIBITOR", t => t.Exhibitor_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Exhibitor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.EXHIBITORFEEDBACK", "Exhibitor_Id", "gsmktg.EXHIBITOR");
            DropForeignKey("gsmktg.EXHIBITORFEEDBACK", "Event_Id", "gsmktg.EVENT");
            DropIndex("gsmktg.EXHIBITORFEEDBACK", new[] { "Exhibitor_Id" });
            DropIndex("gsmktg.EXHIBITORFEEDBACK", new[] { "Event_Id" });
            DropTable("gsmktg.EXHIBITORFEEDBACK");
        }
    }
}
