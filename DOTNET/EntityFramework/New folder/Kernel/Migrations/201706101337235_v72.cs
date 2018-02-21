namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v72 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.ExhibitorRegistrationTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RegistrationType = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("gsmktg.EXHIBITOR", "Comment", c => c.String());
            AddColumn("gsmktg.EXHIBITOR", "ExhibitorRegistrationType_Id", c => c.Guid());
            CreateIndex("gsmktg.EXHIBITOR", "ExhibitorRegistrationType_Id");
            AddForeignKey("gsmktg.EXHIBITOR", "ExhibitorRegistrationType_Id", "gsmktg.ExhibitorRegistrationTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.EXHIBITOR", "ExhibitorRegistrationType_Id", "gsmktg.ExhibitorRegistrationTypes");
            DropIndex("gsmktg.EXHIBITOR", new[] { "ExhibitorRegistrationType_Id" });
            DropColumn("gsmktg.EXHIBITOR", "ExhibitorRegistrationType_Id");
            DropColumn("gsmktg.EXHIBITOR", "Comment");
            DropTable("gsmktg.ExhibitorRegistrationTypes");
        }
    }
}
