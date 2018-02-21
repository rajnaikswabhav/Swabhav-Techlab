namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v83 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("gsmktg.VENUE", "ExhibitorFeedback_Id", "gsmktg.EXHIBITORFEEDBACK");
            DropIndex("gsmktg.VENUE", new[] { "ExhibitorFeedback_Id" });
            CreateTable(
                "gsmktg.ExhibitorFeedbackVenues",
                c => new
                    {
                        ExhibitorFeedback_Id = c.Guid(nullable: false),
                        Venue_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ExhibitorFeedback_Id, t.Venue_Id })
                .ForeignKey("gsmktg.EXHIBITORFEEDBACK", t => t.ExhibitorFeedback_Id, cascadeDelete: true)
                .ForeignKey("gsmktg.VENUE", t => t.Venue_Id, cascadeDelete: true)
                .Index(t => t.ExhibitorFeedback_Id)
                .Index(t => t.Venue_Id);
            
            AddColumn("gsmktg.EMAILTEMPLATE", "Name", c => c.String());
            AddColumn("gsmktg.EMAILTEMPLATE", "EmailType_Id", c => c.Guid());
            CreateIndex("gsmktg.EMAILTEMPLATE", "EmailType_Id");
            AddForeignKey("gsmktg.EMAILTEMPLATE", "EmailType_Id", "gsmktg.EMAILTYPE", "Id");
            DropColumn("gsmktg.VENUE", "ExhibitorFeedback_Id");
        }
        
        public override void Down()
        {
            AddColumn("gsmktg.VENUE", "ExhibitorFeedback_Id", c => c.Guid());
            DropForeignKey("gsmktg.EMAILTEMPLATE", "EmailType_Id", "gsmktg.EMAILTYPE");
            DropForeignKey("gsmktg.ExhibitorFeedbackVenues", "Venue_Id", "gsmktg.VENUE");
            DropForeignKey("gsmktg.ExhibitorFeedbackVenues", "ExhibitorFeedback_Id", "gsmktg.EXHIBITORFEEDBACK");
            DropIndex("gsmktg.ExhibitorFeedbackVenues", new[] { "Venue_Id" });
            DropIndex("gsmktg.ExhibitorFeedbackVenues", new[] { "ExhibitorFeedback_Id" });
            DropIndex("gsmktg.EMAILTEMPLATE", new[] { "EmailType_Id" });
            DropColumn("gsmktg.EMAILTEMPLATE", "EmailType_Id");
            DropColumn("gsmktg.EMAILTEMPLATE", "Name");
            DropTable("gsmktg.ExhibitorFeedbackVenues");
            CreateIndex("gsmktg.VENUE", "ExhibitorFeedback_Id");
            AddForeignKey("gsmktg.VENUE", "ExhibitorFeedback_Id", "gsmktg.EXHIBITORFEEDBACK", "Id");
        }
    }
}
