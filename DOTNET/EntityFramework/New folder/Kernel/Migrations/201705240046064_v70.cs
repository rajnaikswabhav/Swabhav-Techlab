namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v70 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.EXHIBITORSTATUS",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Status = c.String(),
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
            DropTable("gsmktg.EXHIBITORSTATUS");
        }
    }
}
