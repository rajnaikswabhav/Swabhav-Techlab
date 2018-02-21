namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v73 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.EXHIBITORENQUIRY",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        EmailId = c.String(),
                        PhoneNo = c.String(),
                        CompanyName = c.String(),
                        Designation = c.String(),
                        CompanyDescription = c.String(),
                        Address = c.String(),
                        PinCode = c.Int(nullable: false),
                        Comment = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Country_Id = c.Guid(),
                        Event_Id = c.Guid(),
                        ExhibitorIndustryType_Id = c.Guid(),
                        ExhibitorRegistrationType_Id = c.Guid(),
                        ExhibitorStatus_Id = c.Guid(),
                        ExhibitorType_Id = c.Guid(),
                        State_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.COUNTRY", t => t.Country_Id)
                .ForeignKey("gsmktg.EVENT", t => t.Event_Id)
                .ForeignKey("gsmktg.EXHIBITORINDUSTRYTYPE", t => t.ExhibitorIndustryType_Id)
                .ForeignKey("gsmktg.ExhibitorRegistrationTypes", t => t.ExhibitorRegistrationType_Id)
                .ForeignKey("gsmktg.EXHIBITORSTATUS", t => t.ExhibitorStatus_Id)
                .ForeignKey("gsmktg.EXHIBITORTYPE", t => t.ExhibitorType_Id)
                .ForeignKey("gsmktg.STATE", t => t.State_Id)
                .Index(t => t.Country_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.ExhibitorIndustryType_Id)
                .Index(t => t.ExhibitorRegistrationType_Id)
                .Index(t => t.ExhibitorStatus_Id)
                .Index(t => t.ExhibitorType_Id)
                .Index(t => t.State_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.EXHIBITORENQUIRY", "State_Id", "gsmktg.STATE");
            DropForeignKey("gsmktg.EXHIBITORENQUIRY", "ExhibitorType_Id", "gsmktg.EXHIBITORTYPE");
            DropForeignKey("gsmktg.EXHIBITORENQUIRY", "ExhibitorStatus_Id", "gsmktg.EXHIBITORSTATUS");
            DropForeignKey("gsmktg.EXHIBITORENQUIRY", "ExhibitorRegistrationType_Id", "gsmktg.ExhibitorRegistrationTypes");
            DropForeignKey("gsmktg.EXHIBITORENQUIRY", "ExhibitorIndustryType_Id", "gsmktg.EXHIBITORINDUSTRYTYPE");
            DropForeignKey("gsmktg.EXHIBITORENQUIRY", "Event_Id", "gsmktg.EVENT");
            DropForeignKey("gsmktg.EXHIBITORENQUIRY", "Country_Id", "gsmktg.COUNTRY");
            DropIndex("gsmktg.EXHIBITORENQUIRY", new[] { "State_Id" });
            DropIndex("gsmktg.EXHIBITORENQUIRY", new[] { "ExhibitorType_Id" });
            DropIndex("gsmktg.EXHIBITORENQUIRY", new[] { "ExhibitorStatus_Id" });
            DropIndex("gsmktg.EXHIBITORENQUIRY", new[] { "ExhibitorRegistrationType_Id" });
            DropIndex("gsmktg.EXHIBITORENQUIRY", new[] { "ExhibitorIndustryType_Id" });
            DropIndex("gsmktg.EXHIBITORENQUIRY", new[] { "Event_Id" });
            DropIndex("gsmktg.EXHIBITORENQUIRY", new[] { "Country_Id" });
            DropTable("gsmktg.EXHIBITORENQUIRY");
        }
    }
}
