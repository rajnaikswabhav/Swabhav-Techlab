namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v47 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.EXHIBITORINDUSTRYTYPE",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IndustryType = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "gsmktg.EXHIBITORTYPE",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("gsmktg.EXHIBITOR", "ExhibitorIndustryType_Id", c => c.Guid());
            AddColumn("gsmktg.EXHIBITOR", "ExhibitorType_Id", c => c.Guid());
            CreateIndex("gsmktg.EXHIBITOR", "ExhibitorIndustryType_Id");
            CreateIndex("gsmktg.EXHIBITOR", "ExhibitorType_Id");
            AddForeignKey("gsmktg.EXHIBITOR", "ExhibitorIndustryType_Id", "gsmktg.EXHIBITORINDUSTRYTYPE", "Id");
            AddForeignKey("gsmktg.EXHIBITOR", "ExhibitorType_Id", "gsmktg.EXHIBITORTYPE", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.EXHIBITOR", "ExhibitorType_Id", "gsmktg.EXHIBITORTYPE");
            DropForeignKey("gsmktg.EXHIBITOR", "ExhibitorIndustryType_Id", "gsmktg.EXHIBITORINDUSTRYTYPE");
            DropIndex("gsmktg.EXHIBITOR", new[] { "ExhibitorType_Id" });
            DropIndex("gsmktg.EXHIBITOR", new[] { "ExhibitorIndustryType_Id" });
            DropColumn("gsmktg.EXHIBITOR", "ExhibitorType_Id");
            DropColumn("gsmktg.EXHIBITOR", "ExhibitorIndustryType_Id");
            DropTable("gsmktg.EXHIBITORTYPE");
            DropTable("gsmktg.EXHIBITORINDUSTRYTYPE");
        }
    }
}
