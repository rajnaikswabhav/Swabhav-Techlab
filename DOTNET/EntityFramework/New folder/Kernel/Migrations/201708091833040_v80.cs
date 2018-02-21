namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v80 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.EMAILTEMPLATE",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Subject = c.String(),
                        EmailTemplateText = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "gsmktg.EMAILTYPE",
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
            
        }
        
        public override void Down()
        {
            DropTable("gsmktg.EMAILTYPE");
            DropTable("gsmktg.EMAILTEMPLATE");
        }
    }
}
