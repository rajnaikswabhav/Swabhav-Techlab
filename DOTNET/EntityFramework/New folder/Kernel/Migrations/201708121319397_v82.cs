namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v82 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.VENUE", "ExhibitorFeedback_Id", c => c.Guid());
            AddColumn("gsmktg.EXHIBITORFEEDBACK", "ExpectedBusiness", c => c.Boolean(nullable: false));
            AddColumn("gsmktg.EXHIBITORFEEDBACK", "IIMTFSatisfaction", c => c.Int(nullable: false));
            AddColumn("gsmktg.EXHIBITORFEEDBACK", "IIMTFTeam", c => c.Int(nullable: false));
            AddColumn("gsmktg.EXHIBITORFEEDBACK", "IIMTFFacility", c => c.Int(nullable: false));
            AddColumn("gsmktg.EXHIBITORFEEDBACK", "Comment", c => c.String());
            CreateIndex("gsmktg.VENUE", "ExhibitorFeedback_Id");
            AddForeignKey("gsmktg.VENUE", "ExhibitorFeedback_Id", "gsmktg.EXHIBITORFEEDBACK", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.VENUE", "ExhibitorFeedback_Id", "gsmktg.EXHIBITORFEEDBACK");
            DropIndex("gsmktg.VENUE", new[] { "ExhibitorFeedback_Id" });
            DropColumn("gsmktg.EXHIBITORFEEDBACK", "Comment");
            DropColumn("gsmktg.EXHIBITORFEEDBACK", "IIMTFFacility");
            DropColumn("gsmktg.EXHIBITORFEEDBACK", "IIMTFTeam");
            DropColumn("gsmktg.EXHIBITORFEEDBACK", "IIMTFSatisfaction");
            DropColumn("gsmktg.EXHIBITORFEEDBACK", "ExpectedBusiness");
            DropColumn("gsmktg.VENUE", "ExhibitorFeedback_Id");
        }
    }
}
