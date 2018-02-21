namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v16 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.LayoutPlans",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VersionNo = c.String(),
                        isLocked = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Event_Id = c.Guid(),
                        PreviousLayoutPlan_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.EVENT", t => t.Event_Id)
                .ForeignKey("gsmktg.LayoutPlans", t => t.PreviousLayoutPlan_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.PreviousLayoutPlan_Id);
            
            AddColumn("gsmktg.SECTION", "Exhibition_Id", c => c.Guid());
            AddColumn("gsmktg.SECTION", "LayoutPlan_Id", c => c.Guid());
            CreateIndex("gsmktg.SECTION", "Exhibition_Id");
            CreateIndex("gsmktg.SECTION", "LayoutPlan_Id");
            AddForeignKey("gsmktg.SECTION", "Exhibition_Id", "gsmktg.EXHIBITION", "Id");
            AddForeignKey("gsmktg.SECTION", "LayoutPlan_Id", "gsmktg.LayoutPlans", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.SECTION", "LayoutPlan_Id", "gsmktg.LayoutPlans");
            DropForeignKey("gsmktg.LayoutPlans", "PreviousLayoutPlan_Id", "gsmktg.LayoutPlans");
            DropForeignKey("gsmktg.LayoutPlans", "Event_Id", "gsmktg.EVENT");
            DropForeignKey("gsmktg.SECTION", "Exhibition_Id", "gsmktg.EXHIBITION");
            DropIndex("gsmktg.LayoutPlans", new[] { "PreviousLayoutPlan_Id" });
            DropIndex("gsmktg.LayoutPlans", new[] { "Event_Id" });
            DropIndex("gsmktg.SECTION", new[] { "LayoutPlan_Id" });
            DropIndex("gsmktg.SECTION", new[] { "Exhibition_Id" });
            DropColumn("gsmktg.SECTION", "LayoutPlan_Id");
            DropColumn("gsmktg.SECTION", "Exhibition_Id");
            DropTable("gsmktg.LayoutPlans");
        }
    }
}
