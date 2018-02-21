namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v81 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("gsmktg.EXHIBITORTYPE", "VisitorFeedback_Id", "gsmktg.VISITORFEEDBACK");
            DropIndex("gsmktg.EXHIBITORTYPE", new[] { "VisitorFeedback_Id" });
            CreateTable(
                "gsmktg.ExhibitorTypeVisitorFeedbacks",
                c => new
                    {
                        ExhibitorType_Id = c.Guid(nullable: false),
                        VisitorFeedback_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ExhibitorType_Id, t.VisitorFeedback_Id })
                .ForeignKey("gsmktg.EXHIBITORTYPE", t => t.ExhibitorType_Id, cascadeDelete: true)
                .ForeignKey("gsmktg.VISITORFEEDBACK", t => t.VisitorFeedback_Id, cascadeDelete: true)
                .Index(t => t.ExhibitorType_Id)
                .Index(t => t.VisitorFeedback_Id);
            
            DropColumn("gsmktg.EXHIBITORTYPE", "VisitorFeedback_Id");
        }
        
        public override void Down()
        {
            AddColumn("gsmktg.EXHIBITORTYPE", "VisitorFeedback_Id", c => c.Guid());
            DropForeignKey("gsmktg.ExhibitorTypeVisitorFeedbacks", "VisitorFeedback_Id", "gsmktg.VISITORFEEDBACK");
            DropForeignKey("gsmktg.ExhibitorTypeVisitorFeedbacks", "ExhibitorType_Id", "gsmktg.EXHIBITORTYPE");
            DropIndex("gsmktg.ExhibitorTypeVisitorFeedbacks", new[] { "VisitorFeedback_Id" });
            DropIndex("gsmktg.ExhibitorTypeVisitorFeedbacks", new[] { "ExhibitorType_Id" });
            DropTable("gsmktg.ExhibitorTypeVisitorFeedbacks");
            CreateIndex("gsmktg.EXHIBITORTYPE", "VisitorFeedback_Id");
            AddForeignKey("gsmktg.EXHIBITORTYPE", "VisitorFeedback_Id", "gsmktg.VISITORFEEDBACK", "Id");
        }
    }
}
