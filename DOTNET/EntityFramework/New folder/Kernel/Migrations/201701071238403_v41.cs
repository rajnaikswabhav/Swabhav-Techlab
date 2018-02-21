namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v41 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.GENERALMASTER",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.String(),
                        Value = c.String(),
                        Key = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
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
            DropTable("gsmktg.GENERALMASTER");
        }
    }
}
