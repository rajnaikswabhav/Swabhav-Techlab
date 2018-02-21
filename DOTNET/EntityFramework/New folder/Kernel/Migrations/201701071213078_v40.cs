namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v40 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "gsmktg.CategoryExhibitors", newName: "ExhibitorCategories");
            DropPrimaryKey("gsmktg.ExhibitorCategories");
            CreateTable(
                "gsmktg.VISITORFEEDBACK",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SpendRange = c.Int(nullable: false),
                        EventRating = c.Int(nullable: false),
                        RecommendToOther = c.Boolean(nullable: false),
                        ReasonForVisiting = c.Int(nullable: false),
                        KnowAboutUs = c.Int(nullable: false),
                        Comment = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Event_Id = c.Guid(),
                        Visitor_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.EVENT", t => t.Event_Id)
                .ForeignKey("gsmktg.VISITOR", t => t.Visitor_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Visitor_Id);
            
            CreateTable(
                "gsmktg.CategoryVisitorFeedbacks",
                c => new
                    {
                        Category_Id = c.Guid(nullable: false),
                        VisitorFeedback_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.VisitorFeedback_Id })
                .ForeignKey("gsmktg.CATEGORY", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("gsmktg.VISITORFEEDBACK", t => t.VisitorFeedback_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.VisitorFeedback_Id);
            
            CreateTable(
                "gsmktg.VisitorFeedbackCountries",
                c => new
                    {
                        VisitorFeedback_Id = c.Guid(nullable: false),
                        Country_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.VisitorFeedback_Id, t.Country_Id })
                .ForeignKey("gsmktg.VISITORFEEDBACK", t => t.VisitorFeedback_Id, cascadeDelete: true)
                .ForeignKey("gsmktg.COUNTRY", t => t.Country_Id, cascadeDelete: true)
                .Index(t => t.VisitorFeedback_Id)
                .Index(t => t.Country_Id);
            
            AddPrimaryKey("gsmktg.ExhibitorCategories", new[] { "Exhibitor_Id", "Category_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.VISITORFEEDBACK", "Visitor_Id", "gsmktg.VISITOR");
            DropForeignKey("gsmktg.VISITORFEEDBACK", "Event_Id", "gsmktg.EVENT");
            DropForeignKey("gsmktg.VisitorFeedbackCountries", "Country_Id", "gsmktg.COUNTRY");
            DropForeignKey("gsmktg.VisitorFeedbackCountries", "VisitorFeedback_Id", "gsmktg.VISITORFEEDBACK");
            DropForeignKey("gsmktg.CategoryVisitorFeedbacks", "VisitorFeedback_Id", "gsmktg.VISITORFEEDBACK");
            DropForeignKey("gsmktg.CategoryVisitorFeedbacks", "Category_Id", "gsmktg.CATEGORY");
            DropIndex("gsmktg.VisitorFeedbackCountries", new[] { "Country_Id" });
            DropIndex("gsmktg.VisitorFeedbackCountries", new[] { "VisitorFeedback_Id" });
            DropIndex("gsmktg.CategoryVisitorFeedbacks", new[] { "VisitorFeedback_Id" });
            DropIndex("gsmktg.CategoryVisitorFeedbacks", new[] { "Category_Id" });
            DropIndex("gsmktg.VISITORFEEDBACK", new[] { "Visitor_Id" });
            DropIndex("gsmktg.VISITORFEEDBACK", new[] { "Event_Id" });
            DropPrimaryKey("gsmktg.ExhibitorCategories");
            DropTable("gsmktg.VisitorFeedbackCountries");
            DropTable("gsmktg.CategoryVisitorFeedbacks");
            DropTable("gsmktg.VISITORFEEDBACK");
            AddPrimaryKey("gsmktg.ExhibitorCategories", new[] { "Category_Id", "Exhibitor_Id" });
            RenameTable(name: "gsmktg.ExhibitorCategories", newName: "CategoryExhibitors");
        }
    }
}
