namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v67 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.STALL", "ExhibitorIndustryType_Id", c => c.Guid());
            AddColumn("gsmktg.SALESTARGET", "TargetAchieved", c => c.Int(nullable: false));
            AddColumn("gsmktg.SALESTARGET", "Country_Id", c => c.Guid());
            AddColumn("gsmktg.SALESTARGET", "ExhibitorType_Id", c => c.Guid());
            AddColumn("gsmktg.SALESTARGET", "State_Id", c => c.Guid());
            CreateIndex("gsmktg.STALL", "ExhibitorIndustryType_Id");
            CreateIndex("gsmktg.SALESTARGET", "Country_Id");
            CreateIndex("gsmktg.SALESTARGET", "ExhibitorType_Id");
            CreateIndex("gsmktg.SALESTARGET", "State_Id");
            AddForeignKey("gsmktg.STALL", "ExhibitorIndustryType_Id", "gsmktg.EXHIBITORINDUSTRYTYPE", "Id");
            AddForeignKey("gsmktg.SALESTARGET", "Country_Id", "gsmktg.COUNTRY", "Id");
            AddForeignKey("gsmktg.SALESTARGET", "ExhibitorType_Id", "gsmktg.EXHIBITORTYPE", "Id");
            AddForeignKey("gsmktg.SALESTARGET", "State_Id", "gsmktg.STATE", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.SALESTARGET", "State_Id", "gsmktg.STATE");
            DropForeignKey("gsmktg.SALESTARGET", "ExhibitorType_Id", "gsmktg.EXHIBITORTYPE");
            DropForeignKey("gsmktg.SALESTARGET", "Country_Id", "gsmktg.COUNTRY");
            DropForeignKey("gsmktg.STALL", "ExhibitorIndustryType_Id", "gsmktg.EXHIBITORINDUSTRYTYPE");
            DropIndex("gsmktg.SALESTARGET", new[] { "State_Id" });
            DropIndex("gsmktg.SALESTARGET", new[] { "ExhibitorType_Id" });
            DropIndex("gsmktg.SALESTARGET", new[] { "Country_Id" });
            DropIndex("gsmktg.STALL", new[] { "ExhibitorIndustryType_Id" });
            DropColumn("gsmktg.SALESTARGET", "State_Id");
            DropColumn("gsmktg.SALESTARGET", "ExhibitorType_Id");
            DropColumn("gsmktg.SALESTARGET", "Country_Id");
            DropColumn("gsmktg.SALESTARGET", "TargetAchieved");
            DropColumn("gsmktg.STALL", "ExhibitorIndustryType_Id");
        }
    }
}
