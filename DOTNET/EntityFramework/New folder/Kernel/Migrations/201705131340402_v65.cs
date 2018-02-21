namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v65 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.SALESTARGET",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Target = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Event_Id = c.Guid(),
                        ExhibitorIndustryType_Id = c.Guid(),
                        Login_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.EVENT", t => t.Event_Id)
                .ForeignKey("gsmktg.EXHIBITORINDUSTRYTYPE", t => t.ExhibitorIndustryType_Id)
                .ForeignKey("gsmktg.LOGIN", t => t.Login_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.ExhibitorIndustryType_Id)
                .Index(t => t.Login_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.SALESTARGET", "Login_Id", "gsmktg.LOGIN");
            DropForeignKey("gsmktg.SALESTARGET", "ExhibitorIndustryType_Id", "gsmktg.EXHIBITORINDUSTRYTYPE");
            DropForeignKey("gsmktg.SALESTARGET", "Event_Id", "gsmktg.EVENT");
            DropIndex("gsmktg.SALESTARGET", new[] { "Login_Id" });
            DropIndex("gsmktg.SALESTARGET", new[] { "ExhibitorIndustryType_Id" });
            DropIndex("gsmktg.SALESTARGET", new[] { "Event_Id" });
            DropTable("gsmktg.SALESTARGET");
        }
    }
}
